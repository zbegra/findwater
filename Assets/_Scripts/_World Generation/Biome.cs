using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Biome
{
	public string name = "";
	public List<GameObject> objectVariations;
	public float spawnMinRange = 1;
	public float spawnMaxRange = 5;
}
