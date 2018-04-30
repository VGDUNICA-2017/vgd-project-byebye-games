using UnityEngine;
using System.Collections;

public class BalconyPool : MonoBehaviour
{
    //Questo gameObject prende il prefab del balcone mezzo distrutto
    public GameObject balcony_1Prefab;
    //Dimensione dell'"insieme" di balconi che verranno creati una sola volta fuori dallo schermo
    public int balcony_1PoolSize = 8;
    //Velocità di spawn
    public float spawnRate = 3f;
    //Coordinate massime e minime di spawn
    public float yMin = -4f;
    public float yMax = -3f;

    //Collezione di gameObject di balconi
    private GameObject[] balcony_1;
    //Indice di gameObject corrente
    private int currentBalcony_1 = 0;
    //Una posizione usata per i balconi non usati
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float spawnXPosition = 10f;
    //Tempo passato dall'ultimo spawn
    private float timeSinceLastSpawned;
    //Offset tra un balcone e l'altro
    private float offset;

    public BackgroundScrolling backgroundScrolling;


    void Start()
    {
        timeSinceLastSpawned = 0f;

        //Inizializza il vettore
        balcony_1 = new GameObject[balcony_1PoolSize];

        for (int i = 0; i < balcony_1PoolSize; i++)
        {
            //Instanziamo uno per volta i balconi
            balcony_1[i] = (GameObject)Instantiate(balcony_1Prefab, objectPoolPosition, Quaternion.identity);
            balcony_1[i].transform.parent = gameObject.transform;
        }

        backgroundScrolling = GetComponent<BackgroundScrolling>();

    }



    void FixedUpdate()
    {
        offset = Random.Range(3.0f, 5.0f);
        timeSinceLastSpawned += Time.deltaTime;
        //Se il tempo passato dall'ultimo spawn è maggiore o uguale al tempo massimo di spawn entra nel ramo if
        if (timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            float spawnYPosition = Random.Range(yMin, yMax);
            //Se il background sta scorrendo da destra verso sinistra instanziamo i prefab a destra della camera altrimenti vicerversa
            if (backgroundScrolling.mSpeed < 0)
            {
                if(balcony_1[currentBalcony_1] != null)
                {
                    balcony_1[currentBalcony_1].transform.position = new Vector2(spawnXPosition + offset, spawnYPosition);
                }
                else
                {
                    balcony_1[currentBalcony_1] = (GameObject)Instantiate(balcony_1Prefab, objectPoolPosition, Quaternion.identity);
                    balcony_1[currentBalcony_1].transform.parent = gameObject.transform;
                }
                
            }
            else
            {
                if (balcony_1[currentBalcony_1] != null)
                {
                    balcony_1[currentBalcony_1].transform.position = new Vector2(-spawnXPosition - offset, spawnYPosition);
                }
                else
                {
                    balcony_1[currentBalcony_1] = (GameObject)Instantiate(balcony_1Prefab, objectPoolPosition, Quaternion.identity);
                    balcony_1[currentBalcony_1].transform.parent = gameObject.transform;
                }
            }

            currentBalcony_1++;

            if (currentBalcony_1 >= balcony_1PoolSize)
            {
                currentBalcony_1 = 0;
            }

        }
    }
}