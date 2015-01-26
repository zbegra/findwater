using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	public delegate void onWaterFound();
	public static event onWaterFound OnWaterFound;

	// Use this for initialization
	void Start () {
		GameManager.Instance.exitSpawned = true;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			if(OnWaterFound != null)
			{
				OnWaterFound();
				this.gameObject.collider2D.enabled = false;
			}
		}
	}

}
