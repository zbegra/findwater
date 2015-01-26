using UnityEngine;
using System.Collections;

public class Snake : MonoBehaviour {

	GameObject player;
	PlayerStats playerStats;
	float timer;
	bool attack = false;
	
	public float attackDamage = 2;
	public float timeBetweenAttacks = 0.5f;

	public AudioClip snakeBite;
	SpriteRenderer spriteR;
	AudioSource snakeAudio;
	
	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");
		playerStats = player.GetComponent <PlayerStats> ();
		spriteR = GetComponent <SpriteRenderer> ();
		snakeAudio = GetComponent <AudioSource>();


	}
	
	void OnTriggerEnter2D (Collider2D other) {
		if(other.gameObject == player)
		{
			spriteR.enabled = true;
			attack = true;
		}
	}
	
	void OnTriggerExit2D (Collider2D other) {
		if(other.gameObject == player)
		{
			attack = false;
		}
	}
	
	void Update () {
		timer += Time.deltaTime;
		
		if (attack && timer >= timeBetweenAttacks)
		{
			audio.PlayOneShot (snakeBite);
			Attack ();
		}
	}
	
	
	void Attack ()
	{
		// Reset the timer.
		timer = 0f;
		
		// If the player has health to lose...
		if(playerStats.currHealth > 0)
		{
			// ... damage the player.
			playerStats.TakeDamage (attackDamage);
		}
	}
}
