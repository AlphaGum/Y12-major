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


    void Start()
    {
        PlayerLocation = GameObject.Find("player").GetComponent<Transform>();
        characterScript = PlayerLocation.GetComponent<CharacterScript>();
    }

    void Update()
    {
        Vector3 MoveDirection = PlayerLocation.position - transform.position;
        MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
        MoveDirection = Vector3.Normalize(MoveDirection);
        
        transform.Translate(MoveDirection * MoveSpeed * Time.deltaTime);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            characterScript.takedamage(damage);
        }
    }

    public void TakeDamage(float damage1)
    {
        Hp -= damage1;
        if (Hp <= 0)
        {
            Destroy(gameObject);
        }

    }
}
