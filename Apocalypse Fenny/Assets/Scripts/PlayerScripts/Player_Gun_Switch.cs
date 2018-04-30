using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Gun_Switch : MonoBehaviour {

	public Sprite[] dmgSprites = new Sprite[3];

    void Update()
	{
        //Se il player si trova sopra ad un'arma
		if (PlayerScript.hover == true) {

			if (PlayerScript.weap_ == 1) {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [0];

			} else if (PlayerScript.weap_ == 2) {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [1];

            } else if (PlayerScript.weap_ == 3) {
				transform.GetComponent<SpriteRenderer> ().sprite = dmgSprites [2];
            }
		}
        else
        {
            transform.GetComponent<SpriteRenderer>().sprite = null;
        }

	}
}
