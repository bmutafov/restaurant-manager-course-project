using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {

	#region variables
	
	#endregion
	
	#region unity_methods
	private void Start () {
		
	}
	
	private void Update () {
		
	}
	#endregion

	#region public_methods
	public void QuitGame()
	{
		SaveManager.Instance.Save();
		Application.Quit();
	}

	public void ChangeSceneTo(string sceneName)
	{
		SceneManager.LoadScene(sceneName);
	}
	#endregion

	#region private_methods

	#endregion
}
