using UnityEngine;
using System.Collections;

public class BirdFlock : MonoBehaviour {


	public GameObject birdPrefab;

	public float minSpawnTime = 0.2f;
	public float maxSpawnTime = 0.5f;

	public int BirdsToSpawn = 5;

	public float birdSpawnRadius = 5.0f;

	private int birdsSpawned = 0;

	void OnEnable()
	{
		birdsSpawned = 0;
		Invoke("SpawnBird", Random.Range(minSpawnTime,maxSpawnTime));
	}

	void SpawnBird()
	{
	  GameObject bird = Instantiate(birdPrefab,transform.position + GetRandomPointWhitinCircle(birdSpawnRadius),Quaternion.identity) as GameObject;
	  birdsSpawned++;
	
	if(birdsSpawned < BirdsToSpawn)
	{
		Invoke("SpawnBird", Random.Range(minSpawnTime,maxSpawnTime));
	}
	
	else
		{
			this.gameObject.SetActive(false);
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
}
