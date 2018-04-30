using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour {

    public static bool playerIsDead = false;
    public PlayerHealth playerHealth;
    public float restartDelay = 5f;

    Animator anim;                          // Reference to the animator component.
    float restartTimer;                     // Timer to count up to restarting the level


    void Awake()
    {
        // Set up the reference.
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        // If the player has run out of health...
        if (playerHealth.currentHealth <= 0)
        {
            //disattivo la sprite dell'arma una volta che il player è morto
            GameObject.FindGameObjectWithTag("Weapon").GetComponent<SpriteRenderer>().enabled = false;
            //setto la variabile bool che indica la morte del player
            playerIsDead = true;
            // ... tell the animator the game is over.
            anim.SetTrigger("GameOver");

            // .. increment a timer to count up to restarting.
            restartTimer += Time.deltaTime;

            // .. if it reaches the restart delay...
            if (restartTimer >= restartDelay)
            {
                playerIsDead = false;
                // .. then reload the currently loaded level.
                SceneManager.LoadScene(0);
            }
        }
    }
}
