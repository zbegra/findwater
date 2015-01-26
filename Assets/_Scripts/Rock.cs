using UnityEngine;
using System.Collections;

public class Rock : MonoBehaviour {

	GameObject player;
	PlayerMovement playerMovement;
	bool canDestroy = false;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerMovement = player.GetComponent <PlayerMovement> ();
	}

	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject == player)
		{
			canDestroy = true;
			playerMovement.canCollect = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if(other.gameObject == player)
		{
			canDestroy = false;
			playerMovement.canCollect = false;
		}
	}
	
	// Update is called once per frame
	void Update () {

		transform.rotation = Quaternion.identity;
		
		if (playerMovement.collectRock && canDestroy)
		{
			playerMovement.canCollect = false;
			Invoke ("DestroyRock", 0.15f);
		}

	}

	void DestroyRock () {

		Destroy(gameObject);
	}
}