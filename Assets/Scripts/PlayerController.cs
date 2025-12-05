using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;
    public GameObject focalPoint;
    public GameObject indicator;
    private Rigidbody body;
    public bool hasPowerup;
    public float powerupStrength;

    void SetPowerup(bool active)
    {
        hasPowerup = active;
        indicator.SetActive(active);
    }

    void Start()
    {
        body = GetComponent<Rigidbody>();
        SetPowerup(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            SetPowerup(true);
            Destroy(other.gameObject);

            StopAllCoroutines();
            StartCoroutine(PowerupCountDown());
        }
    }

    IEnumerator PowerupCountDown()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Powerup almost depleted");
        yield return new WaitForSeconds(2);
        Debug.Log("Powerup DEPLETED");
        SetPowerup(false);
    }

    void Update()
    {
        indicator.transform.position = transform.position 
            + (Vector3.down * 0.55f);

        float verticalInput = Input.GetAxis("Vertical");
        body.AddForce(focalPoint.transform.forward * verticalInput);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (hasPowerup && collision.collider.CompareTag("Enemy"))
        {
            Debug.Log("Collision with " 
                + collision.collider.gameObject.name 
                + " while having a powerup");
            
            Rigidbody enemyBody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyBody.AddForce(powerupStrength * awayFromPlayer, ForceMode.Impulse);
        }
    }
}
