using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGameAction : MonoBehaviour, IAction
{
	public GameObject loadingCanvas;

	public void Action ()
	{
		StartCoroutine(LoadingScreen());
	}

	private IEnumerator LoadingScreen()
	{
		AsyncOperation async = SceneManager.LoadSceneAsync(1);
		loadingCanvas.SetActive(true);

		yield return async;
	}
}
