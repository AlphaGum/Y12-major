using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosion : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 5.0f; // explosion radius
    public float Damage; // damage amount

    void Start()
    {
        // Get all colliders within the explosion radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.z), radius);
        print(colliders.Length);

        // Apply damage to all objects within the explosion radius
        foreach (Collider2D hit in colliders)
        {
            print("better expolsion: " + hit.name);
            if(hit.gameObject.tag == "Enemy")
            {
                EnemyScript enemyScript = hit.gameObject.GetComponent<EnemyScript>();
                enemyScript.TakeDamage(Damage);
            }
            
            
            // Here you would access the health system of your game objects
            // and apply damage. This will depend on how you've structured your game.
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
