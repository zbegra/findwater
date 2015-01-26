using UnityEngine;
using System.Collections;

public class Waterplant : MonoBehaviour {

	GameObject player;
	PlayerStats playerStats;

	
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent <PlayerStats> ();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject == player)
		{
			playerStats.currHydration = playerStats.currHydration + 3f;
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
