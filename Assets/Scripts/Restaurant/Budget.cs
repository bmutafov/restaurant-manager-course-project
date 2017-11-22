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
		try
		{
			funds = Load.BudgetFunds();
		}
		catch ( System.Exception )
		{
			funds = startingMoney;
		}
		InvokeBudgetChange();
	}
	#endregion

	#region transactions
	public void AddFunds ( float amount )
	{
		funds += ( float ) System.Math.Round(amount, 2);
		InvokeBudgetChange();
	}

	public void WithdrawFunds ( float amount )
	{
		funds -= ( float ) System.Math.Round(amount, 2);
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
