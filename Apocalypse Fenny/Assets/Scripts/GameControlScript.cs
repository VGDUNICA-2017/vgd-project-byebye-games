using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlScript : MonoBehaviour {

    //Entrambi statici per poter accedere da qualsiasi script
    public static float scrollSpeed = -1.5f;
    public static bool gameOver = false;

    public Transform meteor;
    private int rand;// Le meteore inizieranno ad arrivare con un delay minimo dall'inizio del gioco compreso tra 100 e 500 frames
    private int n_meteor;// Saranno generate massimo 3 meteore alla volta
    public int xMax = 6;
    public int xMin = -6;
    public int yMax = 16;
    public int yMin = 7;

    // Use this for initialization
    void Start()
    {
        rand = Random.Range(100, 500);
        n_meteor = Random.Range(0, 3);
    }

	// Update is called once per frame
	void FixedUpdate ()
    {      
        if (rand < 0)
        {
            rand = Random.Range(100, 500);
        }

        for (int i = 0; i < n_meteor; i++)
        {
            if (rand == 0)
            {
                Instantiate(meteor, new Vector3(Random.Range(xMin, xMax), Random.Range(yMin, yMax), 0), Quaternion.identity);
            }
        }

        rand--;
    }
}
