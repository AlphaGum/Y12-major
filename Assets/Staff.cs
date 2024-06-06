using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Staff : MonoBehaviour
{

    public GameObject Projectile;
    public UnityEngine.Transform LaunchOffset;
    public GameObject Enemy;
    public GameObject Bomb;

    public List<float> WandCD = new List<float>() {1,1,0.9f,0.9f,0.8f,0.8f,0.7f,0.7f,0.6f,0.6f,0.5f};
    public List<float> MeteorCD = new List<float>() {5,4.75f,4.5f,4.25f,4,3.75f,3.5f,3.25f,3,2.75f,2.5f};
    public List<float> WandAmount = new List<float>() {1,1,1,2,2,2,3,3,3,4,5};
    public List<float> MeteorAmount = new List<float>() {1,1,1,1,1,2,2,2,2,2,3};
    public float lastShootTime = 0f;

    public CharacterScript characterScript;
    public int wandlevel;
    public int Meteorlevel;
    public int ExplosionLevel;

    public TextMeshProUGUI Meteortext;
    public TextMeshProUGUI WandText;


    void Start()
    {
        //InvokeRepeating("AutoShoot", 1, WandCD[wandlevel]);
        //StartCoroutine(AutoShoot());
        InvokeRepeating("MeteorSpell", 1, MeteorCD[Meteorlevel]);
        DisplayLevel();
    }

    // Update is called once per frame
    void Update()
    {
        AutoShoot(); 
    }

    public void LevelUpWand()
    {
        // Increase the level of the wand
        wandlevel++;

        // Cancel the current invocation of AutoShoot
        //CancelInvoke("AutoShoot");

        //StartCoroutine(AutoShoot());

        // Start a new invocation with the new cooldown
        //InvokeRepeating("AutoShoot", 1, WandCD[wandlevel]);
        DisplayLevel();
        
    }
    public void LevelUpMeteor()
    {
        // Increase the level of the wand
        Meteorlevel++;
        ExplosionLevel++;

        // Cancel the current invocation of AutoShoot
        CancelInvoke("AutoShoot");

        // Start a new invocation with the new cooldown
        InvokeRepeating("AutoShoot", 1, MeteorCD[Meteorlevel]);
        DisplayLevel();

        print("meteor level" + Meteorlevel);
    }

    //void AutoShoot()
    //{
    //    float numProjectilesW = WandAmount[wandlevel];

    //    for (int i = 0; i < numProjectilesW; i++)
    //    {
    //        List<float> Distances = new List<float>();
    //        //This line uses the Physics2D.OverlapCircleAll() method to detect all colliders within a circle of radius 11 units around the current object's position. The detected colliders are stored in an array called hits.
    //        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 11f, 8);
    //        //This loop goes through each collider in the hits array and calculates the distance from the current object to the collider. It then adds this distance to the Distances list.
    //        foreach (Collider2D hit in hits)
    //        {
    //            Distances.Add(Vector2.Distance(transform.position, hit.transform.position));
    //        }
    //        //This line finds the smallest value in the Distances list, which represents the closest detected object.
    //        float min = Distances.Min();
    //        //This line assigns the closest collider to a variable called Target.
    //        Collider2D Target = (hits[Distances.IndexOf(min)]);
    //        //This line creates a new instance of the Projectile game object at the position of LaunchOffset.
    //        GameObject Bullet = Instantiate(Projectile, LaunchOffset.position, Quaternion.identity);
    //        //These lines calculate the direction from the current object to the Target object. The direction is then normalized, which means it's converted to a unit vector (a vector of length 1).
    //        Vector3 MoveDirection = Target.gameObject.transform.position - transform.position;
    //        MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
    //        MoveDirection = Vector3.Normalize(MoveDirection);
    //        //Finally, this line sets the Direction property of the Projectile component of the Bullet object to the calculated direction. This will likely be used to move the bullet towards the target.
    //        Bullet.GetComponent<Projectile>().Direction = MoveDirection;
    //        Bullet.GetComponent<Projectile>().Wandlevel = wandlevel;
    //    }
    //}

    private void AutoShoot()
    {
        if (Time.time > lastShootTime + WandCD[wandlevel])
        {
            lastShootTime = Time.time;
            float numProjectilesW = WandAmount[wandlevel]; // Number of projectiles to shoot


            // Try a time.time based method to delay these ones
            for (int i = 0; i < numProjectilesW; i++)
            {
                List<float> Distances = new List<float>();

                // Detect all colliders within a circle of radius 11 units around the current object's position
                Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 11f, 8);

                // Calculate the distance to each detected collider and store it in the Distances list
                foreach (Collider2D hit in hits)
                {
                    Distances.Add(Vector2.Distance(transform.position, hit.transform.position));
                }

                // Find the closest detected object
                float min = Distances.Min();
                Collider2D Target = hits[Distances.IndexOf(min)];

                // Create a new instance of the Projectile game object at the position of LaunchOffset
                GameObject Bullet = Instantiate(Projectile, LaunchOffset.position, Quaternion.identity);

                // Calculate the direction to the target and normalize it
                Vector3 MoveDirection = Target.gameObject.transform.position - transform.position;
                MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
                MoveDirection = Vector3.Normalize(MoveDirection);

                // Set the direction and wand level of the projectile
                Bullet.GetComponent<Projectile>().Direction = MoveDirection;
                Bullet.GetComponent<Projectile>().Wandlevel = wandlevel;

                // Introduce a slight delay before spawning the next projectile
                //yield return new WaitForSeconds(0.1f); // Adjust the delay time as needed
            }

            // Wait for the cooldown period before the next round of shooting
            //yield return new WaitForSeconds(WandCD[wandlevel]);
        }
    }

    void MeteorSpell()
    {
        float numProjectilesM = MeteorAmount[Meteorlevel];

        for (int i = 0; i < numProjectilesM; i++)
        {
            //creates a list to f=sore the hits
            List<GameObject> MitHit = new List<GameObject>();
            //uses an overlap circle to find every enmy within my range
            Collider2D[] Meteorhits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 11f, 8);
            //each enemyhit by the overlap circle gets added to the mithit list
            foreach (Collider2D hit in Meteorhits)
            {

                if (hit.gameObject.tag == "Enemy")
                {
                    MitHit.Add(hit.gameObject);
                }
            }

            //if there is an enemy in the list it chooses a random one to target
            if (MitHit.Count > 0)
            {
                int randomIndex = Random.Range(0, MitHit.Count);
                GameObject RandomHit = MitHit[randomIndex];
                print(RandomHit.name + " randomHit");

                //converts the enemy position inot a world position
                Vector3 EnemyPosition = RandomHit.transform.position;
                //instantiates teh prefab at the enemy position
                GameObject meteor = Instantiate(Bomb, new Vector3(transform.position.x, transform.position.y + 8, 0), Quaternion.identity);

                //this code dicates how fast the meteor moves towards the target
                Vector3 MoveDirection = EnemyPosition - meteor.transform.position;
                MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
                MoveDirection = Vector3.Normalize(MoveDirection);

                meteor.GetComponent<Meteor>().Direction = MoveDirection;
                meteor.GetComponent<Meteor>().MeteorLevel = Meteorlevel;
                meteor.GetComponent<Meteor>().ExplosionLevel = ExplosionLevel;

            }
        }

    }

    public void DisplayLevel()
    {
        WandText.text = wandlevel.ToString();
        Meteortext.text = Meteorlevel.ToString();


    }
}


////This line creates a new list of floats called Distances. This list will be used to store the distances from the current object to other objects detected by the Physics2D.OverlapCircleAll() method.
//List<float> Distances = new List<float>();
////This line uses the Physics2D.OverlapCircleAll() method to detect all colliders within a circle of radius 11 units around the current object's position. The detected colliders are stored in an array called hits.
//Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 11f, 8);
////This loop goes through each collider in the hits array and calculates the distance from the current object to the collider. It then adds this distance to the Distances list.
//foreach (Collider2D hit in hits)
//{
//    Distances.Add(Vector2.Distance(transform.position, hit.transform.position));
//}
////This line finds the smallest value in the Distances list, which represents the closest detected object.
//float min = Distances.Min();
////This line assigns the closest collider to a variable called Target.
//Collider2D Target = (hits[Distances.IndexOf(min)]);
////This line creates a new instance of the Projectile game object at the position of LaunchOffset.
//GameObject Bullet = Instantiate(Projectile, LaunchOffset.position, Quaternion.identity);
////These lines calculate the direction from the current object to the Target object. The direction is then normalized, which means it's converted to a unit vector (a vector of length 1).
//Vector3 MoveDirection = Target.gameObject.transform.position - transform.position;
//MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
//MoveDirection = Vector3.Normalize(MoveDirection);
////Finally, this line sets the Direction property of the Projectile component of the Bullet object to the calculated direction. This will likely be used to move the bullet towards the target.
//Bullet.GetComponent<Projectile>().Direction = MoveDirection;






//    //creates a list to f=sore the hits
//    List<GameObject>MitHit = new List<GameObject>();
////uses an overlap circle to find every enmy within my range
//Collider2D[] Meteorhits = Physics2D.OverlapCircleAll(new Vector2(transform.position.x, transform.position.y), 11f, 8);
////each enemyhit by the overlap circle gets added to the mithit list
//foreach(Collider2D hit in Meteorhits)
//{

//    if (hit.gameObject.tag == "Enemy")
//    {
//        MitHit.Add(hit.gameObject);
//    }
//}

////if there is an enemy in the list it chooses a random one to target
//if (MitHit.Count > 0)
//{
//    int randomIndex = Random.Range(0, MitHit.Count);
//    GameObject RandomHit = MitHit[randomIndex];
//    print(RandomHit.name + " randomHit");

//    //converts the enemy position inot a world position
//    Vector3 EnemyPosition = RandomHit.transform.position;
//    //instantiates teh prefab at the enemy position
//    GameObject meteor = Instantiate(Bomb, new Vector3(transform.position.x, transform.position.y + 8, 0), Quaternion.identity);

//    //this code dicates how fast the meteor moves towards the target
//    Vector3 MoveDirection = EnemyPosition - meteor.transform.position;
//    MoveDirection = new Vector3(MoveDirection.x, MoveDirection.y, 0);
//    MoveDirection = Vector3.Normalize(MoveDirection);

//    meteor.GetComponent<Meteor>().Direction = MoveDirection;
//}

