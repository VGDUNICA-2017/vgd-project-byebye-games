using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public Sprite[] dmgSprites = new Sprite[2];
	private int index_d = 0;

	private SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bullet") {          
			if (index_d > 1) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_d];
				index_d++;
			}

			Destroy (collision.gameObject);

		} else if (collision.gameObject.tag == "Bullet2") 
		{
			if (index_d > 0) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_d];
				index_d++;
			}
		}
		else if (collision.gameObject.tag == "Bullet3") 
		{
			if (index_d > 0) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_d];
				index_d++;
			}
		}

	}
}
