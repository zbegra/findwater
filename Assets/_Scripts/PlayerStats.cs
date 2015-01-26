using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerStats : MonoBehaviour {

	public float maxHealth = 10f;
	public float currHealth;
	public float maxHeat = 10f;
	public float currHeat;
	public float maxHydration = 10f;
	public float currHydration;

	public float heatRate = 2f;
	public float dehydrationRate = 2f;
	public float cooling = 1f;

	public Slider healthSlider;                                 // Reference to the UI's health bar.
	public Image healthFlash;                                   // Reference to an image to flash on the screen on being hurt.
	//public AudioClip deathClip;                                 // The audio clip to play when the player dies.
	public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
	public Color flashColor = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.
	public Slider hydrationSlider;
	public Slider heatSlider;
	public bool inShade = false;

	public GameObject panelOfTheDead;

	//Animator anim;                                              // Reference to the Animator component.
	PlayerMovement playerMovement;                              // Reference to the player's movement.
	bool isDead = false;                                                // Whether the player is dead.
	bool damaged;                                               // True when the player gets damaged.

	[HideInInspector]
	public bool hasWon = false;

	void OnEnable()
	{
		GameManager.OnExitSpawned += ExitSpawned;
	}

	void OnDisable()
	{
		Exit.OnWaterFound -= FoundWater;
		GameManager.OnExitSpawned -= ExitSpawned;
	}

	void ExitSpawned()
	{
		Exit.OnWaterFound += FoundWater;
	}

	void FoundWater()
	{
		hasWon = true;
	}

	// Use this for initialization
	void Awake () {
	
		//anim = GetComponent <Animator> ();
		//playerAudio = GetComponent <AudioSource> ();
		playerMovement = GetComponent <PlayerMovement> ();
		
		currHealth = maxHealth;
		currHeat = 0f;
		currHydration = maxHydration;
	}
	
	// Update is called once per frame
	void Update () {
		if(hasWon)
			return;

		if(damaged)
		{
			healthFlash.color = flashColor;
		}
		else
		{
			healthFlash.color = Color.Lerp (healthFlash.color, Color.clear, flashSpeed * Time.deltaTime);
		}

		damaged = false;
		
		if(!inShade)
		{
		currHeat = Mathf.MoveTowards (currHeat, maxHeat, heatRate * Time.deltaTime);
		}
		currHydration = Mathf.MoveTowards (currHydration, 0f, dehydrationRate * Time.deltaTime);

		currHeat = Mathf.Clamp (currHeat, 0f, maxHeat);
		currHydration = Mathf.Clamp (currHydration, 0f, maxHydration);

		hydrationSlider.value = currHydration;
		heatSlider.value = currHeat;

		if (currHeat == 10)
		{
			TakeDamage (1f * Time.deltaTime);
		}
	
		if (currHydration == 0)
		{
			TakeDamage (0.5f * Time.deltaTime);
		}
	}

	public void TakeDamage (float amount) {
		if(hasWon)
		{
			return;
		}
		if(isDead)
			// ... no need to take damage so exit the function.
			return;

		damaged = true;

		currHealth -= amount;

		healthSlider.value = currHealth;

		//hurtAudio.Play ();

		if(currHealth <= 0 && !isDead)
		{
			// ... the player is dead.
			Death ();
		}

	}

	public void Shade () {
		if(hasWon)
		{
			return;
		}
		if (isDead)
		{
			return;
		}
		if (currHeat > 0)
		{
		currHeat -= cooling;
		}

	}

	void Death ()
	{
		if(hasWon)
		{
			return;
		}

		isDead = true;
		panelOfTheDead.SetActive(true);
		//anim.SetTrigger ("Die");
		
		// Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
		//playerAudio.clip = deathClip;
		//playerAudio.Play ();
		
		// Turn off the movement and shooting scripts.

		playerMovement.canMove = false;
	}
}
