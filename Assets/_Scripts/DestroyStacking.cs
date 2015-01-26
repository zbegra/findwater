using UnityEngine;
using System.Collections;

public class DestroyStacking : MonoBehaviour {

	Vector2 startPos;
	bool isSafe = false;


	void SetSafe()
	{
		isSafe = true;
		rigidbody2D.isKinematic = true;
	}


	void Start()
	{
		startPos = transform.position;
		Invoke("SetSafe", 2.0f);

	}

	void OnCollisionStay2D(Collision2D col)
	{
		if(col.gameObject.tag == "Stone" || col.gameObject.tag == "Tree")
		{
			Debug.Log("HEy");
			Destroy(col.gameObject);	
		}
	}

	void Update()
	{
		if(!isSafe)
		{
		rigidbody2D.velocity = Vector2.zero;
		rigidbody2D.angularVelocity = 0f;
		transform.position = startPos;
		}

	}
}
