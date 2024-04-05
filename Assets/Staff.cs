using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Staff : MonoBehaviour
{

    public Projectile Projectile;
    public Transform LaunchOffset;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AutoShoot",1,2);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(Projectile, LaunchOffset.position, transform.rotation);
        }
    }

    void AutoShoot()
    {
        
        List<float> Distances = new List<float>();
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 5f, 8);

        foreach (Collider2D hit in hits)
        {
            Distances.Add(Vector2.Distance(transform.position, hit.transform.position));
        }
        float min = Distances.Min();
        print(hits[Distances.IndexOf(min)]);

        Vector3 ShootLocation = LaunchOffset.position - transform.position;
        Instantiate(Projectile, LaunchOffset.position,);



        // Calculating the rotation of the enemy from the player

        //Instantiate(Projectile, LaunchOffset.position, transform.rotation);

    }
}
