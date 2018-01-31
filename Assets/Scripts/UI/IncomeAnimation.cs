using System.Collections;
using TMPro;
using UnityEngine;

public class IncomeAnimation : MonoBehaviour
{

	#region variables
	private Animator animator;
	private TextMeshProUGUI text;
	private bool inMotion = false;
	#endregion

	#region unity_methods
	private void OnEnable ()
	{
		text = GetComponent<TextMeshProUGUI>();
		animator = GetComponent<Animator>();
	}
	#endregion

	#region public_methods
	public void AnimateIncome (float text, string sign)
	{
		if ( inMotion ) return;
		this.text.text = sign + text + "$";
		ChangeMotion(true);
		StartCoroutine(WaitForAnimation());
	}
	#endregion

	#region private_methods
	private IEnumerator WaitForAnimation ()
	{
		yield return new WaitForSeconds(1);
		ChangeMotion(false);
	}

	private void ChangeMotion ( bool motion )
	{
		inMotion = motion;
		animator.SetBool("inMotion", inMotion);
	}
	#endregion
}
