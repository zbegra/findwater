using UnityEngine;
using System.Collections;

public class Shadow : MonoBehaviour {

	GameObject player;
	PlayerStats playerStats;

	bool shade = false;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent <PlayerStats> ();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject == player)
		{
			shade = true;
			playerStats.inShade = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if(other.gameObject == player)
		{
			shade = false;
			playerStats.inShade = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (shade)
		{
			playerStats.Shade ();
		}
	
	}
}
