using UnityEngine;

public class CharacterMainMenu : MonoBehaviour
{

	#region variables
	private Animator animator;
	#endregion

	#region unity_methods
	private void Start ()
	{
		animator = GetComponent<Animator>();
		animator.SetBool("isSitting", true);
	}
	#endregion

	#region public_methods

	#endregion

	#region private_methods

	#endregion
}
