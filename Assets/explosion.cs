using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 5.0f; // explosion radius
    public float Damage; // damage amount

    public List<float> ExplosionDamage = new List<float>() { 100, 110, 120, 130, 140, 150, 160, 170, 180, 190, 200 };
    public int ExplosionLevel;


    void Start()
    {
        // Get all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), radius);

        
        

        // Apply damage to all objects within the explosion radius
        foreach (Collider2D hit in colliders)
        {
            //print("better expolsion: " + hit.name);
            if (hit.gameObject.tag == "Enemy")
            {
                //print("better expolsion part 2: " + hit.name);
                //print($"{Damage} explosion");
                EnemyScript enemyScript = hit.gameObject.GetComponent<EnemyScript>();
                enemyScript.TakeDamage(ExplosionDamage[ExplosionLevel]);
            }
            
            
           
        }

        // Destroy the explosion after applying damage
        Invoke("diedelay", 2f);
    }

    // Update is called once per frame
    void diedelay()
    {
        Destroy(gameObject);
    }
}
