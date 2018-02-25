using UnityEngine;
using System.Collections.Generic;

public static class ReviewTexts
{
	private static readonly List<ReviewText> reviewTexts = new List<ReviewText>() {
		new ReviewText ("I was not satisfied with the menu at all! Did not order anything!", 0),
		new ReviewText ("Bad, bad, bad! Really disappointed.", 1),
		new ReviewText ("The staff was incompetent and the food wasn't delicious.", 2),
		new ReviewText ("Some of the meals happened to have taste but overall a bad place.", 3),
		new ReviewText ("Pretty avarage place.", 4),
		new ReviewText ("I've been to better places. Nevertheless, the food was good.", 5),
		new ReviewText ("Pretty neat decorations and good place overall.", 6),
		new ReviewText ("The staff did its job and the food was tasty!", 7),
		new ReviewText ("One of the best restaurants I have visited, hands down!", 8),
		new ReviewText ("Incredibly delicious and adequate pricing. One of the best!", 9),
		new ReviewText ("My favourite one so far! Food - great, prices - great, staff - even greater!", 10)};

	public static string GetMessageForRating ( int rating )
	{
		var options = reviewTexts.FindAll(r => r.ForRating == rating);
		return options[Random.Range(0, options.Count)].Message;
	}
}

public class ReviewText
{
	private string message;
	private int forRating;

	public ReviewText ( string message, int forRating )
	{
		Message = message;
		ForRating = forRating;
	}

	public string Message
	{
		get
		{
			return message;
		}

		set
		{
			message = value;
		}
	}

	public int ForRating
	{
		get
		{
			return forRating;
		}

		set
		{
			forRating = value;
		}
	}
}
