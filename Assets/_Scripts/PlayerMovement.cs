using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	// Handling & Stats
	public float moveSpeed;
	public float throwSpeed;

	public Rigidbody2D rockPrefab;

	// System
	[HideInInspector]
	public bool canMove = true;
	public bool canThrow = true;
	public bool canCollect = false;
	
	private float moveDeadzone = 0.25f;
	private float aimDeadzone = 0.25f;

	float walkingSoundTimer = 0f;

	private bool throwRock = false;
	public bool collectRock = false;

	Vector2 throwDirection;

	//float horizontalDirection;
	//float verticalDirection;

	public AudioClip walkSound;

	// Components
	public Transform throwPoint;
	GameObject rockIcon;
	Animator anim;
	PlayerStats stats;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		throwPoint = gameObject.transform.FindChild("throwPoint");
		rockIcon = GameObject.Find("RockIcon");
		stats = GetComponent<PlayerStats>();
		//horizontalDirection = 0f;
		//verticalDirection = -1f;

		throwDirection = new Vector2 (0f, 1f);
	}

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
		canMove = false;
	}

	void Update () {

		if (canThrow && Input.GetButtonDown("Fire1"))
		{
			throwRock = true;
		}
		if (!canThrow && Input.GetButtonDown("Fire1") && canCollect)
		{
			collectRock = true;
		}


		if (canThrow)
		{
			rockIcon.SetActive(true);
		}
		else
			rockIcon.SetActive(false);

	}


	// Update is called once per frame
	void FixedUpdate () {

		GamepadControls();

	}

	void GamepadControls() {

		if(!canMove)
		{
			anim.SetInteger("Horizontal", 0);
			anim.SetInteger("Vertical", 0);
			return;
		}

		if(stats.hasWon)
		{
			anim.SetInteger("Horizontal", 0);
			anim.SetInteger("Vertical", 0);
			return;
		}

		Vector2 moveDirection = new Vector3 (Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical"));
		//Vector2 aimDirection = new Vector2 (horizontalDirection, verticalDirection);


		if (moveDirection != Vector2.zero)
		{
			throwDirection = moveDirection;
		}

		if (throwRock)
		{
			Rigidbody2D rockInstance;
			rockInstance = Instantiate(rockPrefab, throwPoint.position, throwPoint.rotation) as Rigidbody2D;
			rockInstance.AddForce(throwDirection * throwSpeed, ForceMode2D.Impulse);

			canThrow = false;
			throwRock = false;
		}

		if (collectRock)
		{
			canThrow = true;
			collectRock = false;
		}


		if(moveDirection.sqrMagnitude < moveDeadzone)
		{
			moveDirection = Vector3.zero;
		}
//		if(aimDirection.sqrMagnitude < aimDeadzone)
//		{
//			aimDirection = Vector3.zero;
//		}

		if (moveDirection != Vector2.zero)
			throwPoint.rotation = Quaternion.LookRotation(moveDirection);

		if (canMove)
		{
			rigidbody2D.MovePosition(rigidbody2D.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
		}


		if (Input.GetAxisRaw ("Horizontal") < 0)
		{
			anim.SetInteger("Horizontal", -1);

			if(!gameObject.audio.isPlaying)
			{
				audio.clip = walkSound;
				audio.Play();
			}
		}
		else if (Input.GetAxisRaw ("Horizontal") > 0)
		{
			anim.SetInteger("Horizontal", 1);

			if(!gameObject.audio.isPlaying)
			{
				audio.clip = walkSound;
				audio.Play();
			}
		}
		else
		{
			anim.SetInteger("Horizontal", 0);

		}

		if (Input.GetAxisRaw ("Vertical") < 0)
		{
			anim.SetInteger("Vertical", -1);

			if(!gameObject.audio.isPlaying)
			{
				audio.clip = walkSound;
				audio.Play();
			}
		}
		else if (Input.GetAxisRaw ("Vertical") > 0)
		{
			anim.SetInteger("Vertical", 1);

			if(!gameObject.audio.isPlaying)
			{
				audio.clip = walkSound;
				audio.Play();
			}
		}
		else
		{
			anim.SetInteger("Vertical", 0);
	
		}
		float isMovingHor = Input.GetAxisRaw ("Vertical");
		float isMovingVert =Input.GetAxisRaw ("Horizontal");


		if(isMovingHor == 0 && isMovingVert == 0)
		{
			audio.Stop();
		}


	}




}
