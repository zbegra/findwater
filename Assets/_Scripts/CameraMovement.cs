using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

	Transform target;

	public float smoothT = 0.2f;

	private Transform thisTransform;
	private Vector3 velocity;


	// Use this for initialization
	void Start () {
	
		target = GameObject.Find ("Player").transform;
		thisTransform = transform;
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float newPosX = Mathf.SmoothDamp(thisTransform.position.x, target.position.x, ref velocity.x, smoothT);
		float newPosY = Mathf.SmoothDamp(thisTransform.position.y, target.position.y, ref velocity.y, smoothT);
		transform.position = new Vector3 (newPosX, newPosY, transform.position.z);
	


//		Vector3 defPos = new Vector3 (0,56,-32);
//		
//		transform.position = target.position + defPos;
	}
}
