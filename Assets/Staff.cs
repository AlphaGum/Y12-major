using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class Staff : MonoBehaviour
{

    public GameObject Projectile;
    public Transform LaunchOffset;
    public GameObject Enemy;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AutoShoot",1,1);
    }

    // Update is called once per frame
    void Update()
    {

       
    }

    void AutoShoot()
    {
        
        List<float> Distances = new List<float>();
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 11f, 8);

        foreach (Collider2D hit in hits)
        {
            Distances.Add(Vector2.Distance(transform.position, hit.transform.position));
        }
        float min = Distances.Min();
        print(hits[Distances.IndexOf(min)]);

        Collider2D Target = (hits[Distances.IndexOf(min)]);

        GameObject Bullet = Instantiate(Projectile, LaunchOffset.position, Quaternion.identity);

        Vector3 MoveDirection = Target.gameObject.transform.position - transform.position;
        MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
        MoveDirection = Vector3.Normalize(MoveDirection);

        Bullet.GetComponent<Projectile>().Direction = MoveDirection;
        


    }

    void MeteorSpell()
    {
        OnMouseDown(Instantiate(Projectile));
    }
}
