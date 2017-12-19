using UnityEngine;

public class ExitGameAction : MonoBehaviour, IAction
{
	public void Action ()
	{
		Application.Quit();
	}
}
