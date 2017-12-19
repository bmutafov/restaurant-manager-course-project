using System.Collections;
using TMPro;
using UnityEngine;

public class ReviewInfo : MonoBehaviour
{
	#region variables
	public int duration = 5;
	[HideInInspector]
	public int rating;
	[HideInInspector]
	public string customerName;

	[SerializeField]
	private TextMeshProUGUI textName;
	[SerializeField]
	private TextMeshProUGUI textRating;
	[SerializeField]
	private TextMeshProUGUI text;
	[SerializeField]
	private Transform starContainer;
	[SerializeField]
	private GameObject starPrefab;

	private Animator animator;
	#endregion

	#region unity_methods
	private void OnEnable ()
	{
		animator = GetComponent<Animator>();
		animator.SetFloat("speed", DayCycle.Instance.daySpeed);
	}
	#endregion

	#region public_methods
	public void SetInfo ( int rating, string name )
	{
		this.rating = rating;
		customerName = name;

		textName.text = customerName;
		textRating.text = rating + "/10";

		for ( int i = 0 ; i < rating ; i++ )
		{
			Instantiate(starPrefab, starContainer);
		}

		text.text = ReviewTexts.GetMessageForRating(rating);

		StartCoroutine(DeleteReview());
	}
	#endregion

	#region private_methods
	private IEnumerator DeleteReview ()
	{
		yield return new WaitForSecondsRealtime(duration);

		animator.SetBool("fadeOut", true);

		yield return new WaitForSecondsRealtime(1);

		Destroy(gameObject);
	}
	#endregion
}
