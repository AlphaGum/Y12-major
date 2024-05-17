using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> EnemyPrefabList;
    public List<GameObject> SpawnPoint;





    [SerializeField]
    private float MinimumSpawnTime;

    [SerializeField]
    private float MaximumSpawnTime;

    private float TimeUntillSpawn;

    public int waveCounter;
    public int Tier;
    public float WaveDelay;
    public int spawnQuantity = 10;

    public int EnemyTier;


    void Start()
    {
        
        InvokeRepeating("NextWave",1,2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  

    private void NextWave()
    {
        waveCounter++;
        print("waveCounter" + waveCounter);

        int tier = waveCounter / 5;

        System.Random rnd = new System.Random();



        for (int i = 0; i < spawnQuantity; i++)
        {
            float x = rnd.Next(0,5);
            float y = rnd.Next(0,5);
            Vector3 randVector = new Vector3(x, y, 0);

            Instantiate(EnemyPrefabList[tier].gameObject, SpawnPoint[tier].transform.position + randVector, Quaternion.identity);

        }
        spawnQuantity += 2;

        
        
       
      

    }

    


}
//every 30 second a new wave of predetermined enimes will spawn fom each corner of the screen
//wave 1: 10 t1 enemies, wave 2: 12 wave 3: 14, 16 18 wave 5 20t1 wave 6 22t1 and 2t2 24,4, 26,6 28,8 wave 10 30t1 10t2, wave 11 32,12,2
//link each spawner to the camera and alwsy keep then just out of reach so you cant see the enemies spawn in 
//wave counter, and teir counter that goes up every 5 waves