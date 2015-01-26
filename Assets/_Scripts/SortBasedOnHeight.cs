using UnityEngine;
using System.Collections;

public class SortBasedOnHeight : MonoBehaviour {

	public bool isRealtime = false;

	private int initialOrder;

	// Use this for initialization
	void Start () {

		initialOrder = this.gameObject.renderer.sortingOrder;
		this.gameObject.renderer.sortingOrder = Mathf.RoundToInt(-transform.position.y) + initialOrder;
	}
	
	// Update is called once per frame
	void Update () {
	
		if(isRealtime)
		{
			this.gameObject.renderer.sortingOrder = Mathf.RoundToInt(-transform.position.y)+ initialOrder;
		}

	}
}
