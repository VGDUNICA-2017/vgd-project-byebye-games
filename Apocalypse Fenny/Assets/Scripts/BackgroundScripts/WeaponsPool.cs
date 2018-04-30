using UnityEngine;
using System.Collections;

public class WeaponsPool : MonoBehaviour
{
    //Questi due gameObject prenderanno i prefab delle armi
    public GameObject weapon_1Prefab;
    public GameObject weapon_2Prefab;
    public GameObject weapon_3Prefab;
    //Dimensione dell'"insieme" di armi che verranno create una volta fuori dallo schermo
    public int weapon_1PoolSize = 1;
    public int weapon_2PoolSize = 1;
    public int weapon_3PoolSize = 1;
    //Velocità di spawn
    public float spawnRate = 15f;
    //yPosition di spawn
    private float spawnYPosition = -3.5f;


    //Collezione di gameObject di armi
    private GameObject[] weapon_1;
    private GameObject[] weapon_2;
    private GameObject[] weapon_3;
    //Indice di gameObject corrente
    private int currentWeapon_1 = 0;
    private int currentWeapon_2 = 0;
    private int currentWeapon_3 = 0;
    //Una posizione usata per le armi non usate
    private Vector2 objectPoolPosition = new Vector2(-15, -25);
    private float spawnXPosition = 10f;
    //Tempo passato dall'ultimo spawn
    private float timeSinceLastSpawned;

    public BackgroundScrolling backgroundScrolling;
    private int rand = 0;


    void Start()
    {
        timeSinceLastSpawned = 0f;

        //Inizializza il vettore
        weapon_1 = new GameObject[weapon_1PoolSize];
        weapon_2 = new GameObject[weapon_2PoolSize];
        weapon_3 = new GameObject[weapon_3PoolSize];

        for (int i = 0; i < weapon_1PoolSize; i++)
        {
            //Instanziamo l'oggetto weapon_1 e lo attacchiamo al parent (Background)
            weapon_1[i] = (GameObject)Instantiate(weapon_1Prefab, objectPoolPosition, Quaternion.identity);
            weapon_1[i].transform.parent = gameObject.transform;
            
        }
        for (int i = 0; i < weapon_2PoolSize; i++)
        {
            //Instanziamo l'oggetto weapon_2 e lo attacchiamo al parent (Background)
            weapon_2[i] = (GameObject)Instantiate(weapon_2Prefab, objectPoolPosition, Quaternion.identity);
            weapon_2[i].transform.parent = gameObject.transform;
        }
        for (int i = 0; i < weapon_3PoolSize; i++)
        {
            //Instanziamo l'oggetto weapon_3 e lo attacchiamo al parent (Background)
            weapon_3[i] = (GameObject)Instantiate(weapon_3Prefab, objectPoolPosition, Quaternion.identity);
            weapon_3[i].transform.parent = gameObject.transform;
        }

        backgroundScrolling = GetComponent<BackgroundScrolling>();  

    }



    void FixedUpdate()
    {
        timeSinceLastSpawned += Time.deltaTime;

        if (timeSinceLastSpawned >= spawnRate)
        {
            timeSinceLastSpawned = 0f;

            if (backgroundScrolling.mSpeed < 0)
            {
                //In base al numero random verrà scelto il tipo di arma da spawnare
                rand = Random.Range(0, 100);
                if (rand <= 33)
                {
                    //Se l'arma non è null e quindi non è stata raccolta la riposizioniamo a destra della camera... 
                    if (weapon_1[currentWeapon_1] != null)
                        weapon_1[currentWeapon_1].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                    //...altrimenti la ricreiamo come nella funzione Start()
                    else
                    {
                        weapon_1[currentWeapon_1] = (GameObject)Instantiate(weapon_1Prefab, objectPoolPosition, Quaternion.identity);
                        weapon_1[currentWeapon_1].transform.parent = gameObject.transform;
                    }
                }
                else if (rand <= 66)
                {
                    if (weapon_2[currentWeapon_2] != null)
                        weapon_2[currentWeapon_2].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                    else
                    {
                        weapon_2[currentWeapon_2] = (GameObject)Instantiate(weapon_2Prefab, objectPoolPosition, Quaternion.identity);
                        weapon_2[currentWeapon_2].transform.parent = gameObject.transform;
                    }
                }
                else
                {
                    if (weapon_3[currentWeapon_3] != null)
                        weapon_3[currentWeapon_3].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                    else
                    {
                        weapon_3[currentWeapon_3] = (GameObject)Instantiate(weapon_3Prefab, objectPoolPosition, Quaternion.identity);
                        weapon_3[currentWeapon_3].transform.parent = gameObject.transform;
                    }
                }
            }
            else
            {
                if (Random.Range(0, 100) <= 33)
                {
                    if (weapon_1[currentWeapon_1] != null)
                        weapon_1[currentWeapon_1].transform.position = new Vector2(-spawnXPosition, spawnYPosition);
                    else
                    {
                        weapon_1[currentWeapon_1] = (GameObject)Instantiate(weapon_1Prefab, objectPoolPosition, Quaternion.identity);
                        weapon_1[currentWeapon_1].transform.parent = gameObject.transform;
                    }
                }
                else if (Random.Range(0, 100) <= 66)
                {
                    if (weapon_2[currentWeapon_2] != null)
                        weapon_2[currentWeapon_2].transform.position = new Vector2(-spawnXPosition, spawnYPosition);
                    else
                    {
                        weapon_2[currentWeapon_2] = (GameObject)Instantiate(weapon_2Prefab, objectPoolPosition, Quaternion.identity);
                        weapon_2[currentWeapon_2].transform.parent = gameObject.transform;
                    }
                }
                else
                {
                    if (weapon_3[currentWeapon_3] != null)
                        weapon_3[currentWeapon_3].transform.position = new Vector2(-spawnXPosition, spawnYPosition);
                    else
                    {
                        weapon_3[currentWeapon_3] = (GameObject)Instantiate(weapon_3Prefab, objectPoolPosition, Quaternion.identity);
                        weapon_3[currentWeapon_3].transform.parent = gameObject.transform;
                    }
                }
            }

            currentWeapon_1++;
            if (currentWeapon_1 >= weapon_1PoolSize)
            {
                currentWeapon_1 = 0;
            }

            currentWeapon_2++;
            if (currentWeapon_2 >= weapon_2PoolSize)
            {
                currentWeapon_2 = 0;
            }

            currentWeapon_3++;
            if (currentWeapon_3 >= weapon_3PoolSize)
            {
                currentWeapon_3 = 0;
            }
        }
    }
}