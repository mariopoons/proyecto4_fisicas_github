using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 30f;
    public bool hasPowerup;
    private float forwardInput;
    private Rigidbody _rigidbody;
    private GameObject focalPoint;
    private float powerupForce = 15f;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }
    private void Update()
    {
        forwardInput = Input.GetAxis("Vertical");
        _rigidbody.AddForce(focalPoint.transform.forward * speed * forwardInput);
    }
  
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup")) ;
        {
            hasPowerup = true;
            Destroy(other.gameObject);
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
        yield return new WaitForSeconds(6);
        hasPowerup = false;
    }
}
