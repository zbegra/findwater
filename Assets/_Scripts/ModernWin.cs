using UnityEngine;
using System.Collections;

public class ModernWin : MonoBehaviour {

	public GameObject winScreen;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "Player")
		{
			winScreen.SetActive(true);
		}
	}
}
