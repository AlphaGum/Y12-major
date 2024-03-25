using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab;

    [SerializeField]
    private float MinimumSpawnTime;

    [SerializeField]
    private float MaximumSpawnTime;

    private float TimeUntillSpawn;
    void Start()
    {
        SetTimeUntillSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        TimeUntillSpawn -= Time.deltaTime;

        if (TimeUntillSpawn <= 0)
        {
            Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            SetTimeUntillSpawn();
        }
    }

    private void SetTimeUntillSpawn()
    {
        TimeUntillSpawn = Random.Range(MinimumSpawnTime, MaximumSpawnTime);
    }
}
