using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

	public Sprite[] dmgSprites = new Sprite[2];
	private int index_d = 0;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "bullet_pistola") {          
			if (index_d > 1) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_d];
				index_d++;
			}
			Destroy (collision.gameObject);
		}

        if (collision.gameObject.tag == "bullet_mitra") 
		{
			if (index_d > 1) {
				Destroy (this.gameObject);
			} else {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [index_d];
				index_d++;
			}
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "bullet_bazooka") 
		{
			Destroy (this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
