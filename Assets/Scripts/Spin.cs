using UnityEngine;

public class Spin : MonoBehaviour
{
    public float rotationSpeed;
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
