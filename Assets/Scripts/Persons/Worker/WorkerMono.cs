using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerMono : MonoBehaviour
{

    public Worker worker;
    public bool isCook = false;

    private void Start ()
    {
        if ( isCook )
            worker = new Cook("Petar", 6);
        else
            worker = new Waiter("Stanio", 1);
        worker.CalculateWorkloadFromSkill();
    }

    private void Update ()
    {
        if ( Input.GetKeyDown("o") )
        {
            var order = new Order(RecipeManager.Instance.GetRandomRecipe(), DayCycle.Instance.GameTime);
            Debug.Log(order.recipe.name);
            OrderStack.Instance.allOrders.Add(order);
        }
        worker.DoWork();
    }
}
