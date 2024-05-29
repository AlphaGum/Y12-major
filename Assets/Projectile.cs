using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    // Start is called before the first frame update
    public float speed = 1.0f;
    public float Damage;
    public List <float> DamageList = new List<float>() { 50,60,70,80,90,100,110,120,130,140,150};
    public int Wandlevel;

    public Vector3 Direction;
    // Update is called once per frame
    private void Update()
    {

        transform.position += Direction * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag == "Enemy")
        {
            
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(Damage);
            Destroy(gameObject);
        }
        

    }

    void ProjectileDamage()
    {
        
    }
    

    
    
}
