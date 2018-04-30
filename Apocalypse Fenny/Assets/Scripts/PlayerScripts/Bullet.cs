using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	float speed;
	//Direzione del player;
	public int indice;

	void Start()
	{
		if (PlayerScript.direction == true ) {
			indice = 0;

		} else if (PlayerScript.direction == false ) {
			indice = 1;
		}
		speed = 0.2f;
	}

	void Update()
	{
		//Posizione corrente del proiettile
		Vector2 position = transform.position;

		//Calcolo nuova posizione
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		//Aggiorno nuova posizione 
		transform.position = position;

        //Il proiettile segue la direzione in cui è stato sparato
		if (indice == 0 ) {
			transform.Translate (0.2f, 0f, 0f);
            transform.GetComponent<SpriteRenderer>().flipX = true;
		} else if (indice == 1 ) {
			transform.Translate (-0.2f, 0f, 0f);
            transform.GetComponent<SpriteRenderer>().flipX = false;
        }


		//Punti massimi(bordi) del campo di gioco
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//Elimina il proiettile se si troverà fuori dallo schermo 
		if (transform.position.x > max.x)
		{
			Destroy(gameObject);
		}
	}

}





