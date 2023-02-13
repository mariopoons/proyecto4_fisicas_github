using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;

    private float yMin = -10;
    private Rigidbody _rigidbody;
    private GameObject player;

    // se ejetuta antes que el start
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        player = GameObject.Find("Player");
    }
    private void Update()
    {
        // Direccion enemigo hacia player
        Vector3 direction = (player.transform.position - transform.position).normalized;
        //aplicar la fuerza para que el enemigo siempre se mueva hacia el player
        _rigidbody.AddForce(direction * speed);

        // comprueba si el enemigo se ha caido de la plataforma
        if(transform.position.y < -yMin)
        {
            Destroy(gameObject);
        }
    }
}
