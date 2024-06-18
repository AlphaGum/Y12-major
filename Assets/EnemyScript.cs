using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

// movmemt hp dmg tier and xp of the enemy

public class EnemyScript : MonoBehaviour
{
    public CharacterScript characterScript;
    public int damage = 10;

    public Transform PlayerLocation;
    public float MoveSpeed;
    public float Hp;
    public int TakeDmg;

    public GameObject XPball;
    public Transform Enemy;


    void Start()
    {
        //finds the player on the map
        PlayerLocation = GameObject.Find("player").GetComponent<Transform>();
        characterScript = PlayerLocation.GetComponent<CharacterScript>();
    }

    void Update()
    {
        //moves towards the player constantly
        if(PlayerLocation != null)
        {
            Vector3 MoveDirection = PlayerLocation.position - transform.position;
            MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
            MoveDirection = Vector3.Normalize(MoveDirection);
            transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //damages the player on collions
        if (collision.gameObject.tag == "Player")
        {
            characterScript.takedamage(damage);
        }
    }

    public void TakeDamage(float damage1)
    {
        //takes damage on on 0 hp dies
        Hp -= damage1;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }

    public void DropXP()
    {
        //spawns xp on death
        Instantiate(XPball, transform.position,Quaternion.identity);
    }

    private void OnDestroy()
    {
        DropXP();
    }


}
