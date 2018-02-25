using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.IO;

public class StartGameAction : MonoBehaviour, IAction
{
	public GameObject loadingCanvas;
	public bool load = false;

	private void Start ()
	{
		if(load)
		{
			if(!File.Exists("allSave.json"))
			{
				GetComponent<Text3DAction>().Disable();
			}
		}
	}

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
