using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	public static GameManager Instance;

	public int seed = 1;
	public List<GameObject> shadowObjects = new List<GameObject>();
	public GameObject birdflockPrefab;

	public float distanceBetweenNodes = 20f;

	public float birdSpawnInterval = 15f;

	[HideInInspector]
	public GameObject exit;

	[HideInInspector]
	public GameObject player;

	[HideInInspector]
	public bool exitSpawned = false;

	public delegate void onExitSpawned();
	public static event onExitSpawned OnExitSpawned;

	void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}

		else 
		{
			Destroy(this);
		}

	}

	void Start()
	{
		//Random.seed = seed;
		 player = GameObject.FindGameObjectWithTag("Player");
//		 exit = GameObject.FindGameObjectWithTag("exit");

		if(player != null)
		{
			StartCoroutine(WaitForExitToSpawn());
		}

		else if(player == null)
		{
			Debug.LogError("There's no player controller in the scene");
		}
	
	}

    IEnumerator WaitForExitToSpawn()
	{
		while(!exitSpawned)
		{
			yield return null;
		}

		if(OnExitSpawned != null)
		{
		OnExitSpawned();
		}
		exit = GameObject.FindGameObjectWithTag("exit");
		StartCoroutine(GeneratePathToExit(player.transform.position,exit.transform.position));
		Invoke("SpawnBirdFlock",6f);
	}


	IEnumerator GeneratePathToExit(Vector3 startPoint, Vector3 endPoint)
	{
		
		Vector3 nodePosition;
		Vector3 previousPosition = startPoint;
		nodePosition = startPoint;

		float curDistance = Mathf.Abs(Vector3.Distance(nodePosition,endPoint));
		float distanceFromPrevious = 0f;

		while(curDistance > 0f)
		{
			nodePosition = Vector3.MoveTowards(nodePosition,endPoint,2.0f);
			curDistance = Mathf.Abs(Vector3.Distance(nodePosition,endPoint));
			distanceFromPrevious = Mathf.Abs (Vector3.Distance(nodePosition,previousPosition));

			if(distanceFromPrevious > distanceBetweenNodes)
			{
				GameObject node = Instantiate(shadowObjects[Random.Range(0,shadowObjects.Count)],nodePosition + GetRandomPointWhitinCircle(30f), Quaternion.identity) as GameObject;
				previousPosition = nodePosition;
				distanceFromPrevious = 0f;
			}
			yield return null;                 
		}

	}

	private Vector3 GetRandomPointWhitinCircle(float radius)
	{
		Vector3 returnValue = Vector3.zero;
		float r1 = Random.value;
		float r2 = Random.value;
		
		float rX = r2 * radius * Mathf.Cos((Mathf.PI*2)*r1/r2);
		float rY = r2 * radius *Mathf.Sin((Mathf.PI*2)*r1/r2);
		
		returnValue.x = rX;
		returnValue.y = rY;
		
		
		return returnValue;
	}

	public void SpawnBirdFlock()
	{
		Vector3 spawnPos = (player.transform.position - exit.transform.position).normalized;

		GameObject birdFlocker = Instantiate(birdflockPrefab, player.transform.position + spawnPos,Quaternion.identity) as GameObject;
		Debug.Log ("Spawning bird flock: Vector: " + spawnPos);

		Invoke("SpawnBirdFlock",birdSpawnInterval);
	}


}
