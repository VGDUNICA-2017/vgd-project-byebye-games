using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    //Inizializzo il vettore velocità
    //Variabile pubblica per poter cambiare la velocità dall'editor Unity
    public Vector2 speed = new Vector2(2, 2);

    //Vettore per il movimento
    private Vector2 movement;
    //Ci prendiamo il component del player (in questo caso il rigidbody)
    private Rigidbody2D rigidbodyComponent;

    void Update()
    {
        //Prendiamo le informazioni riguardanti gli spostamenti in questo caso solo sull'asse X
        float inputX = Input.GetAxis("Horizontal");
        //float inputY = Input.GetAxis("Vertical");

        //Salviamo il movimento del player
        movement = new Vector2(
          speed.x * inputX,
          speed.y * 0);

    }

    void FixedUpdate()
    {
        //Salviamo la reference del component nella variabile dichiarata sopra
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        //Diamo effettivamente movimento al player
        rigidbodyComponent.velocity = movement;
    }
}

