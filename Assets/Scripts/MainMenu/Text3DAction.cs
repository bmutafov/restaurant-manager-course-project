using TMPro;
using UnityEngine;

[RequireComponent(typeof(IAction))]
public class Text3DAction : MonoBehaviour {

	#region variables
	private Animator animator;
	private IAction action;
	private bool isDisabled = false;
	#endregion
	
	#region unity_methods
	private void Start () {
		animator = GetComponent<Animator>();
		action = GetComponent<IAction>();
	}

	private void OnMouseEnter ()
	{
		if ( isDisabled ) return;
		animator.SetBool("enter", true);
		animator.SetBool("exit", false);
	}

	private void OnMouseExit ()
	{
		if ( isDisabled ) return;
		animator.SetBool("enter", false);
		animator.SetBool("exit", true);
	}

	private void OnMouseDown ()
	{
		if ( isDisabled ) return;
		action.Action();
	}
	#endregion

	public void Disable()
	{
		GetComponent<TextMeshPro>().color = Color.grey;
		isDisabled = true;
	}
}
