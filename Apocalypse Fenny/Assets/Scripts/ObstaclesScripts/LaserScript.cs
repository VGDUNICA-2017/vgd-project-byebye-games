using UnityEngine;
using System.Collections;

public class LaserScript : MonoBehaviour
{
    //Le tre parti che comporranno la sprite laser
    public GameObject laserStart;
    public GameObject laserMiddle;
    public GameObject laserEnd;

    //Variabili usate internamente
    private GameObject start;
    private GameObject middle;
    private GameObject end;

    public PlayerHealth playerHealth;


    void FixedUpdate()
    {
        //Creaimo la parte start del laser dal prefab
        if (start == null)
        {
            start = Instantiate(laserStart) as GameObject;
            start.transform.parent = this.transform;
            start.transform.localPosition = Vector2.zero;
        }

        //Stessa cosa della parte start
        if (middle == null)
        {
            middle = Instantiate(laserMiddle) as GameObject;
            middle.transform.parent = this.transform;
            middle.transform.localPosition = Vector2.zero;
        }

        //Definiamo una misura che vada come minimo fuori dallo schermo se non dovesse toccare nessun oggetto
        float maxLaserSize = 50f;
        float currentLaserSize = maxLaserSize;

        //Facciamo il raycast a destra per vedere se la sprite ha toccato qualcosa
        Vector2 laserDirection = this.transform.right;
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, laserDirection, maxLaserSize);

        //Se il laser tocca il player togliamo vita
        if (hit.collider != null)
        {
            if(hit.collider.name == "Player")
                playerHealth.TakeDamage(0.3f);
            //Ci prendiamo la lunghezza attuale della sprite
            currentLaserSize = Vector2.Distance(hit.point, this.transform.position);

            //E creiamo quindi la sprite finale
            if (end == null)
            {
                end = Instantiate(laserEnd) as GameObject;
                end.transform.parent = this.transform;
                end.transform.localPosition = Vector2.zero;
            }
        }
        else
        {
            if (end != null) Destroy(end);
        }

        
        float startSpriteWidth = start.GetComponent<Renderer>().bounds.size.x;

        //Aggiustiamo la lunghezza di middle in base alla lunghezza che aveva al frame precedente (attuale) e togliamo la larghezza della sprite iniziale
        middle.transform.localScale = new Vector3(currentLaserSize - startSpriteWidth, middle.transform.localScale.y, middle.transform.localScale.z);
        middle.transform.localPosition = new Vector2((currentLaserSize / 2f), 0f);

        //Se quindi stiamo toccando qualche oggetto aggiungiamo la sprite finale
        if (end != null)
        {
            end.transform.localPosition = new Vector2(currentLaserSize, 0f);
        }
    }
}