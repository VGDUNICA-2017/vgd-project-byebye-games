using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour
{

    private float backgroundSize;

    private void Start()
    {
        backgroundSize = transform.localScale.x;
    }

    private void FixedUpdate()
    {
        if(transform.position.x < -backgroundSize)
        {
            RepeatBackground();
        }
    }

    void RepeatBackground()
    {
        Vector2 offset;
        if(transform.position.y >= 80)
        {
            offset = new Vector2(backgroundSize * -2f + 5, 0);
        }
        else
        {
           offset = new Vector2(backgroundSize * 2f -5, 0);
        }
        transform.position = (Vector2)transform.position + offset;

    }

}