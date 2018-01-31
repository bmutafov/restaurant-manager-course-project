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
		animator.SetBool("enter", true);
		animator.SetBool("exit", false);
	}

	private void OnMouseExit ()
	{
		animator.SetBool("enter", false);
		animator.SetBool("exit", true);
	}

	private void OnMouseDown ()
	{
		action.Action();
	}
	#endregion
}
