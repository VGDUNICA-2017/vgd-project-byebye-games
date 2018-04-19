using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	float speed;

	void Start()
	{
		speed = 0.2f;
	}

	void Update()
	{
		//posizione corrente del proiettile
		Vector2 position = transform.position;

		//calcolo nuova posizione
		position = new Vector2(position.x, position.y + speed * Time.deltaTime);

		//aggiorno nuova posizione 
		transform.position = position;
		transform.Translate(0.2f, 0f, 0f);

		//punti massimi(bordi) del campo di gioco
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		//elimina il proiettile se si troverà fuori dallo schermo 
		if (transform.position.x > max.x)
		{
			Destroy(gameObject);
		}
	}

}





