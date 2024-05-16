using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject EnemyPrefab1;
    private GameObject EnemyPrefab2;
    private GameObject EnemyPrefab3;
    private GameObject EnemyPrefab4;
    private GameObject EnemyPrefab5;


    [SerializeField]
    private float MinimumSpawnTime;

    [SerializeField]
    private float MaximumSpawnTime;

    private float TimeUntillSpawn;

    public int waveCounter;
    public int Tier;
    public float WaveDelay;

    public int EnemyTier;


    void Start()
    {
        //SetTimeUntillSpawn();
        InvokeRepeating("NextWave",1,2);
    }

    // Update is called once per frame
    void Update()
    {
        TimeUntillSpawn -= Time.deltaTime;

        if (TimeUntillSpawn <= 0)
        {
            //Instantiate(EnemyPrefab, transform.position, Quaternion.identity);
            //SetTimeUntillSpawn();
        }
    }

    private void SetTimeUntillSpawn()
    {
        //TimeUntillSpawn = Random.Range(MinimumSpawnTime, MaximumSpawnTime);
        //TimeUntillSpawn = 4;
    }

    private void NextWave()
    {
        waveCounter++;
        print("waveCounter" + waveCounter);

        int tier = waveCounter / 5;
        print("teir " + tier);
        //Instantiate(EnemyPrefab, transform.position, Quaternion.identity);

        if (tier == 0)
        {
            Instantiate(EnemyPrefab1, transform.position, Quaternion.identity);
        }
        else if (tier == 1)
        {
            Instantiate(EnemyPrefab2, transform.position, Quaternion.identity);
        }
        else if (tier == 2)
        {
            Instantiate(EnemyPrefab3, transform.position, Quaternion.identity);
        }
        else if (tier == 3)
        {
            Instantiate(EnemyPrefab4, transform.position, Quaternion.identity);
        }
        else if (tier == 4)
        {
            Instantiate(EnemyPrefab5, transform.position, Quaternion.identity);
        }
        






    }


}
//every 30 second a new wave of predetermined enimes will spawn fom each corner of the screen
//wave 1: 10 t1 enemies, wave 2: 12 wave 3: 14, 16 18 wave 5 20t1 wave 6 22t1 and 2t2 24,4, 26,6 28,8 wave 10 30t1 10t2, wave 11 32,12,2
//link each spawner to the camera and alwsy keep then just out of reach so you cant see the enemies spawn in 
//wave counter, and teir counter that goes up every 5 waves