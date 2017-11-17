using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMono : MonoBehaviour
{

    public Customer customer;
    public Transform customerInfoUI;

    private void OnMouseEnter ()
    {
        customerInfoUI.gameObject.SetActive(true);
        UI.UpdateChildTextMeshText(customerInfoUI, 0, customer.Name);
        UI.MoveUIToGameObjectPosition(customerInfoUI.gameObject, transform.position, 0, 50);
    }

    private void OnMouseExit ()
    {
        customerInfoUI.gameObject.SetActive(false);
    }
}
