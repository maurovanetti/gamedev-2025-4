using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private float turboSpeed = 1500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public GameObject powerupIndicator;
    public int powerUpDuration = 5;

    public GameObject smoke;

    private float normalStrength = 10; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        smoke = GameObject.Find("Smoke_Particle");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {            
            smoke.GetComponent<ParticleSystem>().Play();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
             smoke.GetComponent<ParticleSystem>().Stop();
        }

        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        if (Input.GetKey(KeyCode.Space))
        {
            smoke.transform.position = transform.position;
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * turboSpeed * Time.deltaTime); 
        }
        else
        {
            playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime);
        }
            
        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {        
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (hasPowerup)
            {
                PushEnemy(other.gameObject, powerupStrength);
            }
            else
            {
                PushEnemy(other.gameObject, normalStrength);
            }           
        }
    }

    void PushEnemy(GameObject enemy, float strength)
    {
        Rigidbody enemyRigidbody = enemy.GetComponent<Rigidbody>();
        Vector3 awayFromPlayer = enemy.transform.position - transform.position;
        enemyRigidbody.AddForce(awayFromPlayer * strength, ForceMode.Impulse);
    }
}
