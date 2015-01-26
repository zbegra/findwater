using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Zone : MonoBehaviour {

	[Header("Editor Related Stuff")]
	[Tooltip("Color of the gizmo shown in the scene view")]
	public Color32 GizmoColor = Color.red;

	[Tooltip("Setting this to true will hide the gizmo")]
	public bool HideGizmos = false;
	[Space(1.0f)]

	[Header("Zone Parameters")]
	[Tooltip("The inner radius, objects will start spawning outside this")]
	public float innerRadius = 10.0f;

	[Tooltip("The outer radius, objects will spawn inside this")]
	public float outerRadius = 10.0f;

	[Tooltip("This will adjust the density of grass spawned")]
	public int grassAreas = 10;

	[Tooltip("If you want more than one goal you can adjust this variable - Does nothing if prefab is not assigned")]
	public int numberOfWaterSources = 1;

	public int keyFrequency = 20;

	[Space(1.0f)]

	[Header("Detail Objects")]
	[Tooltip("Grass prefabs")]
	public List<GameObject> grassPrefabs = new List<GameObject>();

	[Space(1.0f)]

	[Header("Key Objects")]
	[Tooltip("Water prefab - aka the goal - water will only spawn in zones that has this prefab assigned")]
	public GameObject waterPrefab;

	
	[Space(1.0f)]

	[Header("Shadow Objects")]
	
	public List<Biome> biomes = new List<Biome>();

	public void OnDrawGizmos()
	{
		if(!HideGizmos)
		{
		Gizmos.color = GizmoColor;


		//Find the minimum distance from the center that we will start spawning objects
		float minD = Vector3.Distance(transform.position, transform.position + (Vector3.right * innerRadius));

		//Find the maxium distance from the center that we will spawn objects.
		float maxD = Vector3.Distance(transform.position, transform.position + (Vector3.right * outerRadius));

		//Draw Spheres in the scene showing the radius of the spawn area.
		Gizmos.DrawWireSphere(transform.position,innerRadius);
		Gizmos.DrawWireSphere(transform.position,outerRadius);

		//Display a cube at min and max distance in editor only.
		Gizmos.color = Color.blue;
		Gizmos.DrawCube(Vector3.right * (minD),Vector3.one);
		Gizmos.color = Color.red;
		Gizmos.DrawCube(Vector3.right * (maxD), Vector3.one);


		}

	}

	/// <summary>
	/// Gets a random point whitin circle.
	/// </summary>
	/// <returns>The random point whitin circle.</returns>
	/// <param name="index">Index to add to seed for random number generator</param>
	private Vector3 GetRandomPointWhitinCircle()
	{

		Vector3 returnValue = Vector3.zero;
		float r1 = Random.value;
		float r2 = Random.value;

		float rX = r2 * outerRadius * Mathf.Cos((Mathf.PI*2)*r1/r2);
		float rY = r2 * outerRadius *Mathf.Sin((Mathf.PI*2)*r1/r2);

		returnValue.x = rX;
		returnValue.y = rY;


		return returnValue;
	}

	private void SpawnGrass()
	{
		//Find the minimum distance from the center that we will start spawning objects
		float minDistance = Vector3.Distance(transform.position, transform.position + (Vector3.right * innerRadius));
		
		//Find the maxium distance from the center that we will spawn objects.
		float maxDistance = Vector3.Distance(transform.position, transform.position + (Vector3.right * outerRadius));


		for(int i = 0; i < grassAreas; i++)
		{
			Vector3 point = GetRandomPointWhitinCircle();
			float d = Mathf.Abs(Vector3.Distance(point,transform.position));

			if(d > minDistance && d < maxDistance)
			{
				GameObject grass = Instantiate(grassPrefabs[Random.Range(0, grassPrefabs.Count)],point,Quaternion.identity) as GameObject;
			}
			
		}
	}

	private void PopulateKeyObjects()
	{
		//Find the minimum distance from the center that we will start spawning objects
		float minDistance = Vector3.Distance(transform.position, transform.position + (Vector3.right * innerRadius));
		
		//Find the maxium distance from the center that we will spawn objects.
		float maxDistance = Vector3.Distance(transform.position, transform.position + (Vector3.right * outerRadius));
		
		
		for(int i = 0; i < keyFrequency; i++)
		{
			Vector3 point = GetRandomPointWhitinCircle();
			float d = Mathf.Abs(Vector3.Distance(point,transform.position));
			
			if(d > minDistance && d < maxDistance)
			{
				float r = Random.Range(0,100);

				foreach (Biome b in biomes)
				{
					if(r <= b.spawnMaxRange && r >= b.spawnMinRange)
					{
						GameObject obj = Instantiate(b.objectVariations[Random.Range(0,b.objectVariations.Count)],point,Quaternion.identity) as GameObject;
						continue;
					}
				}


			}
			
		}
	}

	IEnumerator SpawnWater()
	{
		Debug.Log ("Attempting to spawn water");
		//Find the minimum distance from the center that we will start spawning objects
		float minDistance = Vector3.Distance(transform.position, transform.position + (Vector3.right * innerRadius));
		
		//Find the maxium distance from the center that we will spawn objects.
		float maxDistance = Vector3.Distance(transform.position, transform.position + (Vector3.right * outerRadius));

		bool hasSpawned = false;

		Vector3 pos = GetRandomPointWhitinCircle();
		float d = Mathf.Abs (Vector3.Distance(pos,transform.position));
		Debug.Log ("Distance is: " + d + "(min: " + minDistance + " - max: " + maxDistance);

		if(d >= minDistance && d <= maxDistance)
		{
			GameObject water = Instantiate(waterPrefab, pos,Quaternion.identity) as GameObject;
		}

		else
		{
		
		float m = maxDistance-minDistance;

		float randomDistanceOffset = Random.Range (0f,m);
		
		
		while( d < minDistance + randomDistanceOffset)
		{
			pos = Vector3.MoveTowards(pos,pos * randomDistanceOffset,5.0f);
			d = Mathf.Abs (Vector3.Distance(pos,transform.position));
			Debug.Log (pos);
			yield return null;
		}

		while(d > maxDistance + randomDistanceOffset)
		{
			pos = Vector3.MoveTowards(pos,pos * randomDistanceOffset,5.0f);
			d = Mathf.Abs (Vector3.Distance(pos,transform.position));
			Debug.Log (pos);
			yield return null;
		}
			GameObject water = Instantiate(waterPrefab, pos,Quaternion.identity) as GameObject;
		}

		Debug.Log ("Water Reached position");


	}



	void Start()
	{
		SpawnGrass();
		PopulateKeyObjects();

		for(int i = 0; i < numberOfWaterSources; i++)
		{
			if(waterPrefab != null)
			{
				StartCoroutine("SpawnWater");
			}
		}

	}


}

