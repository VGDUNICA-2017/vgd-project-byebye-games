using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    public static float mSpeed;
    [SerializeField] private bool mStopScrolling;

    private int randScroll;
    private int randVerso;


    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = new Vector2(-2.0f, 0);
        randScroll = 1000;
        randVerso = 1000;
        mSpeed = -2.0f;
    }

    private void FixedUpdate()
    {
        if (randScroll <= 0)
        {
            if(mSpeed < 0)
            {
                mSpeed = mSpeed - 1.0f;
            }
            else
            {
                mSpeed = mSpeed + 1.0f;
            }
            randScroll = 1000;
        }
        else
        {
            randScroll--;
        }

        //cambia il verso di scorrimento
        if (randVerso <= 0 && transform.position.x > -10 )
        {
            mSpeed = -mSpeed;
            randVerso = 1000;
        }
        else
        {
            randVerso--;
        }

        //sezione per scorrimento vero e proprio del background
        if (mStopScrolling)
        {
            rigidbody2d.velocity = Vector2.zero;
        }
        else
        {
            rigidbody2d.velocity = new Vector2(mSpeed, 0);
        }
    }

}