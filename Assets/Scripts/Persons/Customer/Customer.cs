using System;
using UnityEngine;

[System.Serializable]
public class Customer : Person
{

    #region variables
    [SerializeField] private Wealthiness wealth;
    [SerializeField] private float visitPercentage;

    public Wealthiness Wealth
    {
        get
        {
            return wealth;
        }

        protected set
        {
            wealth = value;
        }
    }

    public float VisitPercentage
    {
        get
        {
            return visitPercentage;
        }

        set
        {
            visitPercentage = value;
        }
    }
    #endregion

    public Customer (string name, Wealthiness wealth, float visitPercentage) : base(name)
    {
        Wealth = wealth;
        VisitPercentage = visitPercentage;
    }
}
