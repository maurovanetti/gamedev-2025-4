using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRange;

    void Start()
    {
        Instantiate(
            enemyPrefab, 
            GetSpawnPoint(),
            enemyPrefab.transform.rotation 
        );
    }

    private Vector3 GetSpawnPoint()
    {
        float x = Random.Range(-spawnRange, spawnRange);
        float z = Random.Range(-spawnRange, spawnRange);
        return new Vector3(x, 0, z);
    }

    void Update()
    {

    }
}
