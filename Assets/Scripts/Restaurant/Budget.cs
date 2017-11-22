using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Budget : GenericSingletonClass<Budget>
{
	public float startingMoney = 5000;

	private float funds;

	public float Funds
	{
		get
		{
			return funds;
		}
	}

	public delegate void OnBudgetChanged ();
	public OnBudgetChanged onBudgetChangedCallback;

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

	private void InvokeBudgetChange ()
	{
		if ( onBudgetChangedCallback != null ) onBudgetChangedCallback.Invoke();
	}
}
