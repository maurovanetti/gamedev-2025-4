using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float forceIntensity;
    public float minY;

    private GameObject player;    
    private Rigidbody body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 enemyToPlayer = player.transform.position - transform.position;
        Vector3 direction = enemyToPlayer.normalized;
        Vector3 force = direction * forceIntensity;
        force.y = 0;
        body.AddForce(force);

        if (transform.position.y < minY)
        {
            Destroy(gameObject);
        }
    }
}
