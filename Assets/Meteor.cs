using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 1.0f;
    public float Damage;

    public GameObject Explosion;
    public GameObject Temp;
    public Vector3 Direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Direction * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag == "Enemy")
        {
            print($"{Damage} Meteor");

            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(Damage);

            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            print("explosion");
            Destroy(gameObject);
        }


    }
    
}
