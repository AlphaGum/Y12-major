using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    // Start is called before the first frame update
    public float speed = 1.0f;
    public float Damage;
    // Update is called once per frame
    private void Update()
    {

        transform.position += transform.right * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag == "Enemy")
        {
            print("damage");
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(Damage);
            Destroy(gameObject);
        }
        

    }
    
}
