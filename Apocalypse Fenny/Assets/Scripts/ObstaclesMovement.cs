using UnityEngine;
using System.Collections;

public class ObstaclesMovement : MonoBehaviour {

    private static float speed = -0.025F;
    public Vector3 rotation_vector;
    
	// Update is called once per frame
	void Update () {
		transform.Translate (new Vector2(speed,0));
        transform.Rotate(rotation_vector);
	}
}
