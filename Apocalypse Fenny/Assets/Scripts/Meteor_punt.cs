using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor_punt : MonoBehaviour {

	private int index_m = 0;

	private SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bullet3") {          
			if (index_m > 0) {
				Destroy (this.gameObject);
			} else {
				
				index_m++;
			}

			Destroy (collision.gameObject);


	}
}
}