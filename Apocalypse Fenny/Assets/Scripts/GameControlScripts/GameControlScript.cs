using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class GameControlScript : MonoBehaviour {

    //Entrambi statici per poter accedere da qualsiasi script
    public static float scrollSpeed = -1.5f;

    public Transform meteor1;
    public Transform meteor2;


    //Delay rispettivamente per meteor_1 e meteor_2
    public int rand1;
    public int rand2;

    //Numero meteore che verranno generate
    private int n_meteor;

    //Coordinate x e y, massime e minime in cui verranno istanziate le meteore
    public int xMax = 15;
    public int xMin = -6;
    public int yMax = 16;
    public int yMin = 7;

    public GameObject laser;
    public int count;


    void Start()
    {
        rand1 = Random.Range(500, 1000);
        rand2 = Random.Range(500, 1000);
        n_meteor = 1;
    }

	void FixedUpdate ()
    {
        count--;
        if (Time.frameCount % 500 == 0)
        {
            laser.SetActive(true);
        }
        if (Time.frameCount % 200 == 0)
        {
            laser.SetActive(false);
        }

        if (rand1 < 0)
        {
            //Rinizializziamo la variabile
            rand1 = Random.Range(300, 500);
            //Incrementiamo il numero di meteore da instanziare al prossimo giro
            n_meteor++;
            n_meteor %= 4;
            //Se dopo il module n_meteor vale zero lo portiamo a 1
            if(n_meteor == 0)
                n_meteor += 1;
        }
        if (rand2 < 0)
        {
            rand2 = Random.Range(300, 500);
            n_meteor++;
            n_meteor %= 4;
            if(n_meteor == 0)
                n_meteor += 1;
        }

        //For per il numero di meteore da istanziare dato n_meteor
        for (int i = 0; i < n_meteor; i++)
        {
            if (rand1 == 0)
            {
                Instantiate(meteor1, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
            }

            if (rand2 == 0)
            {
                Instantiate(meteor2, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
            }
        }
        rand1--;
        rand2--;

        
    }

}
