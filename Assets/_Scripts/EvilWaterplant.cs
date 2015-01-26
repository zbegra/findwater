using UnityEngine;
using System.Collections;

public class EvilWaterplant : MonoBehaviour {

	GameObject player;
	PlayerStats playerStats;
	
	float attackDamage = 3f;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent <PlayerStats> ();
	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject == player)
		{
			playerStats.currHydration = playerStats.currHydration + 6f;
			playerStats.TakeDamage(attackDamage);
			Destroy (gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
