using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : GenericSingletonClass<CustomerManager>
{
	#region variables_generation
	public AllCustomers allCustomers;
	public int customerPoolSize = 50;

	[Range(0, 1)]
	public float startingVisitPercentage = 0.5f;
	#endregion

	#region variables_visit
	public GameObject spawnNodes;
	public List<CustomerGroup> customerGroups;
	#endregion

	#region variables_customers
	public GameObject customerPrefab;
	public Transform customerInfoUI;

	public GameObject reviewPrefab;
	public GameObject reviewContainer;
	#endregion


	private void Start ()
	{
		DayCycle.Instance.onDayChangedCallback += KickLeftCustomers;
		
		if ( !SaveManager.Instance.loadOnStart )
		{
			allCustomers = new AllCustomers();
			allCustomers.list.Clear();
			GenerateCustomersPool();
		}

		customerGroups = new List<CustomerGroup>();

		DayCycle.Instance.onDayStartedCallback += CustomersNewDay;
	}

	#region customer_generation
	private void GenerateCustomersPool ()
	{
		for ( int i = 0 ; i < customerPoolSize ; i++ )
		{
			Customer customer = new Customer(Generate.Name(), Generate.Wealth(), startingVisitPercentage);
			allCustomers.list.Add(customer);
			Debug.Log("Customer " + customer.Name + " created!");
		}
	}
	#endregion

	#region customer_visit
	private void CustomersNewDay ()
	{
		List<Customer> visitingCustomers = GenerateVisitingCustomersForTheDay();
		GenerateGroupsFromCustomers(visitingCustomers);
	}

	/// <summary>
	/// Generates a list for the amount of visiting customers for the day
	/// </summary>
	/// <returns>List</returns>
	private List<Customer> GenerateVisitingCustomersForTheDay ()
	{
		// Count current seats count
		int tablesCount = spawnNodes.transform.childCount;
		int seatsCount = 0;
		for ( int i = 0 ; i < tablesCount ; i++ )
		{
			seatsCount += spawnNodes.transform.GetChild(i).childCount;
		}
		
		// Formula to determine the max number of visiting customers
		int customerMaxCount = seatsCount  * DayCycle.Instance.WorkingHours;

		// Get random customers with that count
		List<Customer> customerPool = allCustomers.GetRandomCustomers(customerMaxCount);
		List<Customer> visitingCustomers = new List<Customer>();

		// Filter the customers
		for ( int i = 0 ; i < customerPool.Count ; i++ )
		{
			// Random number between 0.00 and 1.00
			float chance = Random.Range(0f, 1f);
			// if the number is less than the visit percentage of the customer
			// they visit the restaurant; else they do not
			if ( chance < customerPool[i].VisitPercentage )
			{
				visitingCustomers.Add(customerPool[i]);
			}
		}

		// returns the list to be used in other functions
		return visitingCustomers;
	}

	/// <summary>
	/// Splits a customer list into groups of 2-4 people
	/// </summary>
	/// <param name="customers">List of customers to be seperated by groups</param>
	private void GenerateGroupsFromCustomers ( List<Customer> customers )
	{
		List<Customer> customerCopy = new List<Customer>(customers);
		while ( customerCopy.Count > 0 )
		{
			int personCount = Random.Range(2, 4);
			if ( personCount > customerCopy.Count )
			{
				personCount = customerCopy.Count;
			}

			List<Customer> cust = new List<Customer>();
			cust.AddRange(customerCopy.GetRange(0, personCount));
			customerCopy.RemoveRange(0, personCount);

			CustomerGroup cg = new CustomerGroup(cust)
			{
				visitTime = GenerateVisitTime()
			};
			Debug.Log("A group of " + cg.customers.Count + " will visit at: " + cg.visitTime);
			customerGroups.Add(cg);
		}
	}

	/// <summary>
	/// Generates a visit time; relies on percentages
	/// </summary>
	/// <returns></returns>
	private System.DateTime GenerateVisitTime ()
	{
		System.DateTime result = new System.DateTime();
		result = result
			.AddYears(2017)
			.AddDays(DayCycle.daysPassedSinceStart)
			.AddHours(GetVisitingHour())
			.AddMinutes(Random.Range(0, 55));
		return result;
	}

	/// <summary>
	/// Called automatically on day end. All customers left in the restaurant leave.
	/// </summary>
	private void KickLeftCustomers()
	{
		List<CustomerMono> customers = new List<CustomerMono>();
		customers.AddRange(FindObjectsOfType<CustomerMono>());
		foreach ( var customer in customers )
		{
			if ( customer.table != null )
				customer.LeaveRestaurant();
			else
				Destroy(customer.gameObject);
		}
	}

	/// <summary>
	/// Returns a visiting hour (12-22). Percentages included, for edit this function
	/// </summary>
	/// <returns></returns>
	private int GetVisitingHour ()
	{
		float p = Random.Range(0, 100);

		if ( (p -= 5) < 0 )  // 5%
			return 12;

		if ( (p -= 10) < 0 ) // 15%
			return 13;

		if ( (p -= 10) < 0 ) // 25%
			return 14;

		if ( (p -= 3) < 0 )  // 28%
			return 15;

		if ( (p -= 2) < 0 )  // 30%
			return 16;

		if ( (p -= 5) < 0 )  // 35%
			return 17;

		if ( (p -= 15) < 0 ) // 50%
			return 18;

		if ( (p -= 20) < 0 ) // 70%
			return 19;

		if ( (p -= 20) < 0 ) // 90%
			return 20;

		if ( (p -= 8) < 0 )  // 98%
			return 21;

		else                 //100%
			return 22;

	}
	#endregion

	#region public_functions
	public List<CustomerGroup> GetVisitingNow ()
	{
		System.DateTime currentGameTime = DayCycle.Instance.GameTime;

		System.Predicate<CustomerGroup> match = cg => cg.visitTime.Hour == currentGameTime.Hour && cg.visitTime.Minute == currentGameTime.Minute;
		List<CustomerGroup> visitingNow = customerGroups.FindAll(match);
		return visitingNow;
	}

	public GameObject SpawnCustomerModel ( Customer customer )
	{
		if ( GameObject.Find("Customer" + customer.Id) != null ) throw new System.Exception("Customer with this ID already exist in scene!");

		GameObject instance = Instantiate(customerPrefab);
		instance.name = "Customer" + customer.Id;
		CustomerMono customerMono = instance.AddComponent<CustomerMono>();
		customerMono.customer = customer;
		customerMono.customerInfoUI = customerInfoUI;
		customerMono.reviewPrefab = reviewPrefab;
		customerMono.reviewContainer = reviewContainer;
		return instance;
	}
	#endregion

}
