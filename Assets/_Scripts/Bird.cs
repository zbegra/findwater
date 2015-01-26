using UnityEngine;
using System.Collections;

public class Bird : MonoBehaviour {

	public float birdSpeed = 5.0f;
	private Vector3 destination;

	Color32 colore;

	public float fadeSpeed = 0.5f;
	public Color32 birdFadeColor;

	SpriteRenderer render;

	void Start()
	{
		render = GetComponentInChildren<SpriteRenderer>();
		colore = render.color;
		destination = GameObject.FindGameObjectWithTag("exit").transform.position;

	}

	void Update()
	{
		Vector3 moveVector = Vector3.MoveTowards(transform.position,destination,birdSpeed * Time.deltaTime);
		transform.LookAt(destination);
		transform.position = moveVector;

		colore = Color.Lerp(colore,birdFadeColor, fadeSpeed * Time.deltaTime);
		render.color = colore;

		if(render.color == birdFadeColor)
		{
			Destroy(transform.gameObject);
		}
	}

}
