using UnityEngine;

[RequireComponent(typeof(IAction))]
public class Text3DAction : MonoBehaviour {

	#region variables
	private Animator animator;
	private IAction action;
	#endregion
	
	#region unity_methods
	private void Start () {
		animator = GetComponent<Animator>();
		action = GetComponent<IAction>();
	}

	private void OnMouseEnter ()
	{
		Debug.Log("Enter");
		animator.SetBool("enter", true);
		animator.SetBool("exit", false);
	}

	private void OnMouseExit ()
	{
		Debug.Log("Exit");
		animator.SetBool("enter", false);
		animator.SetBool("exit", true);
	}

	private void OnMouseDown ()
	{
		action.Action();
		Debug.Log("Click.");
	}
	#endregion

	#region public_methods
	#endregion

	#region private_methods

	#endregion
}
