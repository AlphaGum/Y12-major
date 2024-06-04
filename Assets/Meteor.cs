using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public float speed = 1.0f;
    public float Damage;

    public List<float> MeteorDamage = new List<float>() {100,110,120,130,140,150,160,170,180,190,200};

    public GameObject Explosion;
    public GameObject Temp;
    public Vector3 Direction;
    public int MeteorLevel;
    public int ExplosionLevel;
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
            

            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(MeteorDamage[MeteorLevel]);

            GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
            explosion.GetComponent<explosion>().ExplosionLevel = ExplosionLevel;
            
            Destroy(gameObject);
        }


    }
    
}
