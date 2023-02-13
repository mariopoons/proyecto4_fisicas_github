using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 30f;
    public bool hasPowerup; 
    public bool hasPowerup2;
    private float forwardInput;
    private Rigidbody _rigidbody;
    private GameObject focalPoint;
    private float powerupForce = 15f;
    public GameObject[] powerupIndicators;
    private float originalScale = 1.5f; //escala sin auntentar el powerup
    private float powerupScale = 2f; //escala aumentada por el powerup

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
    }
    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup")) 
        {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDown());
        }
        if (other.gameObject.CompareTag("Powerup2"))
        {
            hasPowerup2 = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountDown());
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRigidbody = other.gameObject.
            GetComponent<Rigidbody>();
            Vector3 awayFromPlayer =
            (other.gameObject.transform.position -
            transform.position).normalized;
            enemyRigidbody.AddForce(awayFromPlayer * powerupForce,
             ForceMode.Impulse);
        }
    }
    private IEnumerator PowerupCountDown()
    {
        if(hasPowerup2)
        {
            transform.localScale = powerupScale * Vector3.one;
        }
        for(int i=0; i<powerupIndicators.Length; i++)
        {
            powerupIndicators[i].SetActive(true);
            yield return new WaitForSeconds(2);
            powerupIndicators[i].SetActive(false);
        }
        if (hasPowerup2)
        {
            transform.localScale = originalScale * Vector3.one;
        }
        hasPowerup = false;
        hasPowerup2 = false;
    }
}
