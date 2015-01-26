using UnityEngine;
using System.Collections;

public class ActivateWinScreen : MonoBehaviour {

	public GameObject winScreen;

	void OnEnable()
	{
		GameManager.OnExitSpawned += GotExit;
	}

	void OnDisable()
	{
		GameManager.OnExitSpawned -= GotExit;
		Exit.OnWaterFound -= FoundWater;
	}

	void FoundWater ()
	{
		winScreen.SetActive(true);
	}

	void GotExit()
	{
		Exit.OnWaterFound += FoundWater;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
