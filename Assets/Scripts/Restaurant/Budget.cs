using System;
using UnityEngine;

public class Budget : GenericSingletonClass<Budget>
{
	#region variables
	[SerializeField]
	private IncomeAnimation incomeAnimator;
	[SerializeField]
	private IncomeAnimation withdrawAnimator;

	public float startingMoney = 5000;

	private float funds;

	public float Funds
	{
		get
		{
			return (float)System.Math.Round(funds, 2);
		}
	}
	#endregion

	#region delegates
	public delegate void OnBudgetChanged ();
	public OnBudgetChanged onBudgetChangedCallback;
	#endregion

	#region unity_methods
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
		AnimateWithdrawal(amount);
		InvokeBudgetChange();
	}


	/// <summary>
	/// Sets the Funds value equal to the amount (IMORTANT: USE ONLY WHEN LOADING SAVE)
	/// </summary>
	/// <param name="amount">Amount of funds to be set</param>
	public void LoadFunds ( float amount )
	{
		funds = amount;
		InvokeBudgetChange();
	}

	public void AnimateIncome ( float amount )
	{
		incomeAnimator.AnimateIncome(amount);
	}

	public void AnimateWithdrawal( float amount )
	{
		withdrawAnimator.AnimateIncome(amount);
	}
	#endregion

	#region private_methods
	private void InvokeBudgetChange ()
	{
		if ( onBudgetChangedCallback != null ) onBudgetChangedCallback.Invoke();
	}
	#endregion
}
