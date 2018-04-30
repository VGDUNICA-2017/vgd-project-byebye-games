using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private GameObject player;
    private ParticleSystem _psystem;
    private PlayerHealth playerHealth;
    private int meteorDamage = 30;
    private int index_m = 0;

    private void Awake()
    {
        //Inizializziamo i vari componenti
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        _psystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        //l'effetto particellare viene posto in pausa in attesa dell'attivazione per l'effetto di scena
        _psystem.Pause();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Se la meteora entra in contatto con il suolo o il player
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player" || collision.gameObject.tag == "bullet_bazooka")
        {
            //Mettiamo in azione il sistema che gestisce l'effetto particellare
            _psystem.Play();
        }

        //Se la meteora viene colpita da 3 proiettili del mitra
        if (collision.gameObject.tag == "bullet_mitra" && index_m == 3)
        {
            //Mettiamo in azione il sistema che gestisce l'effetto particellare
            _psystem.Play();
        }

        //Se la meteora viene colpita da 5 proiettili della pistola
        if (collision.gameObject.tag == "bullet_pistola" && index_m == 5)
        {
            //Mettiamo in azione il sistema che gestisce l'effetto particellare
            _psystem.Play();
        }

        //Nel caso particolare del player togliamo vita in base alla variabile "attackDamage"
        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //una munizione del bazooka distruggerà la meteora
        if (collision.gameObject.tag == "bullet_bazooka")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }

        //tre munizioni del mitra distruggerranno la meteora
        if (collision.gameObject.tag == "bullet_mitra")
        {
            if (index_m >= 3)
            {
                Destroy(this.gameObject);
            }
            else
            {
                index_m++;
            }
            Destroy(collision.gameObject);
        }

        //cinque munizioni della pistola distruggeranno la meteora
        if (collision.gameObject.tag == "bullet_pistola")
        {
            if (index_m >= 5)
            {
                Destroy(this.gameObject);
            }
            else
            {
                index_m++;
            }
            Destroy(collision.gameObject);
        }
    }

    void Attack()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(meteorDamage);
        }
    }
}