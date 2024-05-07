using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;

public class Staff : MonoBehaviour
{

    public GameObject Projectile;
    public Transform LaunchOffset;
    public GameObject Enemy;
    public GameObject Bomb;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("AutoShoot",1,1);
    }

    // Update is called once per frame
    void Update()
    {


        MeteorSpell();

    }

    void AutoShoot()
    {
        //This line creates a new list of floats called Distances. This list will be used to store the distances from the current object to other objects detected by the Physics2D.OverlapCircleAll() method.
        List<float> Distances = new List<float>();
        //This line uses the Physics2D.OverlapCircleAll() method to detect all colliders within a circle of radius 11 units around the current object's position. The detected colliders are stored in an array called hits.
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 11f, 8);
        //This loop goes through each collider in the hits array and calculates the distance from the current object to the collider. It then adds this distance to the Distances list.
        foreach (Collider2D hit in hits)
        {
            Distances.Add(Vector2.Distance(transform.position, hit.transform.position));
        }
        //This line finds the smallest value in the Distances list, which represents the closest detected object.
        float min = Distances.Min();
        //This line assigns the closest collider to a variable called Target.
        Collider2D Target = (hits[Distances.IndexOf(min)]);
        //This line creates a new instance of the Projectile game object at the position of LaunchOffset.
        GameObject Bullet = Instantiate(Projectile, LaunchOffset.position, Quaternion.identity);
        //These lines calculate the direction from the current object to the Target object. The direction is then normalized, which means it's converted to a unit vector (a vector of length 1).
        Vector3 MoveDirection = Target.gameObject.transform.position - transform.position;
        MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
        MoveDirection = Vector3.Normalize(MoveDirection);
        //Finally, this line sets the Direction property of the Projectile component of the Bullet object to the calculated direction. This will likely be used to move the bullet towards the target.
        Bullet.GetComponent<Projectile>().Direction = MoveDirection;
        


    }

    void MeteorSpell()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Convert mouse position to world position
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            // Instantiate the prefab at the mouse position
             GameObject meteor = Instantiate(Bomb, new Vector3(transform.position.x,8,0), Quaternion.identity);

            Vector3 MoveDirection = mousePosition - meteor.transform.position;
            MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
            MoveDirection = Vector3.Normalize(MoveDirection);

            meteor.GetComponent<Meteor>().Direction = MoveDirection;

        }

    }
}
