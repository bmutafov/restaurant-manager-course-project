using System;
using UnityEngine;

public abstract class DeliverySource : ScriptableObject
{
    new public string name;
    public IngredientType[] ingredientTypes;

    [Range(0, 1)]
    public float averageQuality;

    public float dailyMinAmount;
    public float dailyMaxAmount;

    public abstract void GenerateDaily ();
    public abstract void DisplayOffer ();
}
