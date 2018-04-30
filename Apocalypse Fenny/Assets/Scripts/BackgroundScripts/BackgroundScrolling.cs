using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    //Variabile a cui verrà assegnata la velocità di scorrimento
    public float mSpeed;

    //Variabile per il cambio di velocità
    public int scroll;
    //Variabile per il cambio di verso
    public int verso;

    private void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        rigidbody2d.velocity = new Vector2(-2.0f, 0);
        //Dopo n frames verrà cambiata la velocità
        scroll = 1100;
        //Dopo n frames verrà cambiato il verso
        verso = 1000;
        mSpeed = -4.0f;
    }

    private void FixedUpdate()
    {
        //Non permettiamo una velocità maggiore di 15
        if (scroll <= 0 && Mathf.Abs(mSpeed)<=15 )
        {
            if (mSpeed < 0)
            {
                mSpeed = mSpeed - 1.5f;
            }
            else
            {
                mSpeed = mSpeed + 1.5f;
            }
            scroll = 2000;
        }
        else
        {
            scroll--;
        }

        //Cambia il verso di scorrimento
        if (verso == 0)
        {
            mSpeed *= (-1);
            verso = 1000;
        }
        else
        {
            verso--;
        }

        //Blocchiamo lo scorrimento quando il player muore
        if (GameOverManager.playerIsDead)
        {
            rigidbody2d.velocity = Vector2.zero;
        }
        else
        {
            rigidbody2d.velocity = new Vector2(mSpeed, 0);
        }
    }

}