using UnityEngine;
using System.Collections;

public class OpenSceneButton : MonoBehaviour {

	public string sceneToOpen = "";

	public void LoadScene()
	{
		Application.LoadLevel(sceneToOpen);
	}
}
