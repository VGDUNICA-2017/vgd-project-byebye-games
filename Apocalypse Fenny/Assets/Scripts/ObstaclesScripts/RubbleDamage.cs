using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubbleDamage : MonoBehaviour {

    private int index_m;

    private void OnTriggerExit2D(Collider2D collision)
    {
        //una munizione del bazooka distruggerà la maceria
        if (collision.gameObject.tag == "bullet_bazooka")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
