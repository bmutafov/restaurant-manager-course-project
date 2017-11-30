public class Budget : GenericSingletonClass<Budget>
{
	#region variables
	public float startingMoney = 5000;

	private float funds;

	public float Funds
	{
		get
		{
			return funds;
		}
	}
	#endregion

	#region delegates
	public delegate void OnBudgetChanged ();
	public OnBudgetChanged onBudgetChangedCallback;
	#endregion

	#region unity_methods
	private void Start ()
	{
	}
	#endregion

	#region transactions
	/// <summary>
	/// Adds an amount to the budget funds
	/// </summary>
	/// <param name="amount"></param>
	public void AddFunds ( float amount )
	{
		funds += ( float ) System.Math.Round(amount, 2);
		InvokeBudgetChange();
	}

	/// <summary>
	/// Takes funds from the budget
	/// </summary>
	/// <param name="amount"></param>
	public void WithdrawFunds ( float amount )
	{
		funds -= ( float ) System.Math.Round(amount, 2);
		InvokeBudgetChange();
	}


	/// <summary>
	/// Sets the Funds value equal to the amount (IMORTANT: USE ONLY WHEN LOADING SAVE)
	/// </summary>
	/// <param name="amount">Amount of funds to be set</param>
	public void LoadFunds( float amount )
	{
		funds = amount;
		InvokeBudgetChange();
	}
	#endregion

	#region private_methods
	private void InvokeBudgetChange ()
	{
		if ( onBudgetChangedCallback != null ) onBudgetChangedCallback.Invoke();
	}
	#endregion
}
