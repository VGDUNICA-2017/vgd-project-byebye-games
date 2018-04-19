using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balcony : MonoBehaviour {

	public Sprite[] dmgSprites = new Sprite[2];
	private int index_b = 0;

	private SpriteRenderer spriteRenderer;

	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Bullet") {          
			if (index_b > 1) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_b];
				index_b++;
			}

			Destroy (collision.gameObject);

		} else if (collision.gameObject.tag == "Bullet2") 
		{
			if (index_b > 0) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_b];
				index_b++;
			}
		}
		else if (collision.gameObject.tag == "Bullet3") 
		{
			if (index_b > 0) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_b];
				index_b++;
			}
		}

	}
}
