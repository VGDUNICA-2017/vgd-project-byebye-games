using UnityEngine;
using System.Collections;

public class RubblePool : MonoBehaviour
{
    //Questi due gameObject prenderanno i prefab delle macerie
    public GameObject rubble_1Prefab;
    public GameObject rubble_2Prefab;
    //Dimensione dell'"insieme" di macerie che verranno create una volta fuori dallo schermo
    public int rubble_1PoolSize = 8;
    public int rubble_2PoolSize = 8;
    //Velocità di spawn
    public float spawnRate = 3f;
    //Coordinate massime e minime di spawn
    public float yMin = -4f;
    public float yMax = -3f;

    //Collezione di gameObject di macerie
    private GameObject[] rubbles_1;
    private GameObject[] rubbles_2;
    //Indice di gameObject corrente
    private int currentRubble_1 = 0;
    private int currentRubble_2 = 0;
    //Una posizione usata per le macerie non usate
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float spawnXPosition = 10f;
    //Tempo passato dall'ultimo spawn
    private float timeSinceLastSpawned;
    //Offset tra un tipo di maceria e l'altro
    private float offset;

    public BackgroundScrolling backgroundScrolling;


    void Start()
    {
        timeSinceLastSpawned = 0f;

        //Inizializza il vettore
        rubbles_1 = new GameObject[rubble_1PoolSize];
        rubbles_2 = new GameObject[rubble_2PoolSize];

        for (int i = 0; i < rubble_1PoolSize; i++)
        {
            rubbles_1[i] = (GameObject)Instantiate(rubble_1Prefab, objectPoolPosition, Quaternion.identity);
            rubbles_1[i].transform.parent = gameObject.transform;
        }
        for (int i = 0; i < rubble_2PoolSize; i++)
        {
            rubbles_2[i] = (GameObject)Instantiate(rubble_2Prefab, objectPoolPosition, Quaternion.identity);
            rubbles_2[i].transform.parent = gameObject.transform;
        }

        backgroundScrolling = GetComponent<BackgroundScrolling>(); 

    }



    void FixedUpdate()
    {
        offset = Random.Range(5.0f, 10.0f);
        timeSinceLastSpawned += Time.deltaTime;
        //Se il tempo passato dall'ultimo spawn è maggiore o uguale al tempo massimo di spawn entra nel ramo if
        if (timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;


            float spawnYPosition = Random.Range(yMin, yMax);

            //Se il background sta scorrendo da destra verso sinistra instanziamo i prefab a destra della camera altrimenti vicerversa
            if (backgroundScrolling.mSpeed < 0)
            {
                if(rubbles_1[currentRubble_1] != null)
                {
                    rubbles_1[currentRubble_1].transform.position = new Vector2(spawnXPosition + offset, spawnYPosition);
                }
                else
                {
                    rubbles_1[currentRubble_1] = (GameObject)Instantiate(rubble_1Prefab, objectPoolPosition, Quaternion.identity);
                    rubbles_1[currentRubble_1].transform.parent = gameObject.transform;
                }

                if (rubbles_2[currentRubble_2] != null)
                {
                    rubbles_2[currentRubble_2].transform.position = new Vector2(spawnXPosition + offset, spawnYPosition);
                }
                else
                {
                    rubbles_2[currentRubble_2] = (GameObject)Instantiate(rubble_2Prefab, objectPoolPosition, Quaternion.identity);
                    rubbles_2[currentRubble_2].transform.parent = gameObject.transform;
                }
            }
            else
            {
                if (rubbles_1[currentRubble_1] != null)
                {
                    rubbles_1[currentRubble_1].transform.position = new Vector2(- spawnXPosition - offset, spawnYPosition);
                }
                else
                {
                    rubbles_1[currentRubble_1] = (GameObject)Instantiate(rubble_1Prefab, objectPoolPosition, Quaternion.identity);
                    rubbles_1[currentRubble_1].transform.parent = gameObject.transform;
                }

                if (rubbles_2[currentRubble_2] != null)
                {
                    rubbles_2[currentRubble_2].transform.position = new Vector2(- spawnXPosition - offset, spawnYPosition);
                }
                else
                {
                    rubbles_2[currentRubble_2] = (GameObject)Instantiate(rubble_2Prefab, objectPoolPosition, Quaternion.identity);
                    rubbles_2[currentRubble_2].transform.parent = gameObject.transform;
                }
            }

            currentRubble_1++;

            if (currentRubble_1 >= rubble_1PoolSize)
            {
                currentRubble_1 = 0;
            }

            currentRubble_2++;

            if (currentRubble_2 >= rubble_2PoolSize)
            {
                currentRubble_2 = 0;
            }
        }
    }
}