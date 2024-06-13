using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{


    // Start is called before the first frame update
    public float speed = 1.0f;
    public float Damage;
    //the damage is in a list so that i can easily change the damage whenever the wand levels up 
    public List <float> WandDamage = new List<float>() { 40,45,50,55,60,65,70,75,80,85,};
    public int Wandlevel;

    

    public Vector3 Direction;
    // Update is called once per frame
    private void Update()
    {
        //this is for the speed of the projectile

        transform.position += Direction * Time.deltaTime * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player" && collision.gameObject.tag == "Enemy")
        {
            
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(WandDamage[Wandlevel]);
            print("wand damage" + WandDamage[Wandlevel]);
            Destroy(gameObject);
        }
        

    }

   
    

    
    
}
