using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundScript : MonoBehaviour {

    public float delay;

    //Script per la distruzione delle meteore quando entrano in contatto con la strada
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Meteor")
        {
            Destroy(collision.gameObject,delay);
        }
    }
}