using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float force;
    public GameObject focalPoint;
    private Rigidbody body;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody>();      
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        body.AddForce(focalPoint.transform.forward * verticalInput);
    }
}
