using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        Instantiate(
            enemyPrefab, 
            new Vector3(0, 0, 6), 
            enemyPrefab.transform.rotation 
        );
    }

    void Update()
    {

    }
}
