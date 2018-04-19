using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private GameObject player;
    private ParticleSystem _psystem;
    private PlayerHealth playerHealth;
    private int attackDamage = 50;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        _psystem = GetComponentInChildren<ParticleSystem>();
    }

    private void Start()
    {
        _psystem.Pause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Ground" || collision.gameObject.tag == "Player")
        {
            _psystem.Play();
        }

        if (collision.gameObject.tag == "Player")
        {
            Attack();
        }
    }

    void Attack()
    {
        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }
}