using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorScript : MonoBehaviour
{
    private ParticleSystem _psystem;

    private void Awake()
    {
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
            GameControlScript.health -= 1;
        }
    }
}