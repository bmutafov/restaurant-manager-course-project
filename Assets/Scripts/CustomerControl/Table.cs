using System.Collections.Generic;
using UnityEngine;
using ExtensionMethods;

public class Table : MonoBehaviour
{
	private static int lastUsedId = 0;

	#region table_info_variables
	private int id;
	private int customersOnTable = 0;

	public int maxPeople = 4;
	public bool isTaken = false;
	public bool isRound = false;
	public List<CustomerGroup> customerGroup;
	public List<Order> tableOrders;
	#endregion

	public int Id
	{
		get
		{
			return id;
		}
	}

	public int CustomersOnTable
	{
		get
		{
			return customersOnTable;
		}

		set
		{
			customersOnTable = value;
			if ( customersOnTable == 0 )
			{
				for ( int i = 0 ; i < transform.childCount ; i++ )
				{
					Transform customerModel = transform.GetChild(i).FirstChild();
					if ( customerModel != null ) Destroy(customerModel.gameObject);
				}
				isTaken = false;
				Debug.Log("Table #" + id + " is now free!");
			}
		}
	}

	private void Start ()
	{
		id = lastUsedId++;
	}
}
