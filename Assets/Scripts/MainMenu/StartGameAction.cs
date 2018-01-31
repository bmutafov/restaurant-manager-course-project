using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartGameAction : MonoBehaviour, IAction
{
	public GameObject loadingCanvas;
	public bool load = false;

	public void Action ()
	{
		StartCoroutine(LoadingScreen());
	}

	private IEnumerator LoadingScreen ()
	{
		SaveManager.Instance.loadOnStart = load;

		AsyncOperation async = SceneManager.LoadSceneAsync(1);
		loadingCanvas.SetActive(true);

		yield return async;
	}
}
