using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrassSpawner : MonoBehaviour {

	public int minSize = 4;
	public int maxSize = 12;
	public float minRadius = 1f;
	public float maxRadius = 10f;
	public float spread = 0.4f;


	public List<GameObject> grassPrefabs = new List<GameObject>();

	public static int grassIndex = 0;

	private int size = 0;
	private float radius = 0f;

	void Awake()
	{

	}

	void Start()
	{
	 size =	Random.Range(minSize, maxSize);
	 radius = Random.Range(minRadius, maxRadius);

		for(int i = 0; i < size; i++)
		{
			GameObject grass = Instantiate(grassPrefabs[Random.Range(0,grassPrefabs.Count)], transform.position, Quaternion.identity) as GameObject;
			grass.transform.parent = this.transform;

			Vector3 newPos = GetRandomPointWhitinCircle() * spread;
			grass.transform.localPosition = newPos;

		}
	}

	private Vector3 GetRandomPointWhitinCircle()
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
