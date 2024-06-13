using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class CharacterScript : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveinput;
    public Image healthBar;
    public GameObject healthSlider;
    public float healthBarNumber;
    public float HealthAmount = 100f;
    public int level;
    public float currentXP;
    public float RequiredXP;

    private float lerptimer;
    public float delaytimer;
    public Image frontXpBar;
    public Image backXpBar;
    public float backXpBarNumber;
    public float frontXpBarNumber;
    public float AdditionMultiplyer = 150;
    public float PowerMultiplyer = 1.5f;
    public float DivisionMultiplyer = 15;
    public GameObject XpSlider;

    public float Damage;

    //class options
    public string ClassName;
    public GameObject WeaponS1;
    public GameObject WeaponS2;
    public GameObject Buff1;
    public GameObject Buff2;

    public TextMeshProUGUI LevelText;
    public Button Wand;
    public Button Meteor;

    public int StatPoints;
    public GameObject LevelupScreen;






    //handles the character colliding with the enmemy 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            EnemyScript enemyScript = collision.gameObject.GetComponent<EnemyScript>();
            enemyScript.TakeDamage(Damage);


        }
    }

    void Start()
    {
        //spawns the health and xp bars 
        frontXpBarNumber = currentXP / RequiredXP;
        backXpBarNumber = currentXP / RequiredXP;
        RequiredXP = CalculateRequiredXp();
        //changes the health on the slider 
        Vector3 StartVector = new Vector3(0,1,1);
        healthSlider.transform.localScale = StartVector;

        DisplayLevel();

    }

    void Update()
    {
        movement();
        //activates the levl up code 
        if (currentXP > RequiredXP)
        {
            LevelUp();
        }

        

    }
    public void movement()
    {
        // This method handles player movement

        moveinput.x = Input.GetAxisRaw("Horizontal");
        moveinput.y = Input.GetAxisRaw("Vertical");
        //ensures the speed to not vary
        moveinput.Normalize();

        rb2d.velocity = moveinput * moveSpeed;
    }
    private void healthbarupdate()
    {
        // This method updates the health bar UI
        float NewScale = 1 - healthBarNumber;
        Vector3 NewScaleVector = new Vector3(NewScale, 1, 1);
        healthSlider.transform.localScale = NewScaleVector;
    }
    public void takedamage(float damage)
    {
        // This method handles taking damage
        HealthAmount -= damage;
        healthBarNumber = HealthAmount / 100;
        healthbarupdate();
        if (HealthAmount <= 0)
        {
            Destroy(gameObject);
        }

    }
    public void heal(float healingAmount)
    {
        // This method handles healing
        HealthAmount += healingAmount;
        HealthAmount = Mathf.Clamp(HealthAmount, 0, 100);
        healthBarNumber = HealthAmount / 100;
        healthbarupdate();
    }
    public void UpdateXpUI()
    {
        // This method updates the XP UI
        // Calculate the fraction of current XP relative to the required XP
        float xpFraction = currentXP / RequiredXP;
        float FXP = frontXpBarNumber;
        // Check if the front XP bar needs to be updated
        if (FXP < xpFraction)
        {
            delaytimer += Time.deltaTime;
            backXpBarNumber = xpFraction;
            if(delaytimer > 1.5)
            {
                lerptimer += Time.deltaTime;
                // Calculate the percentage of completion for the lerp (linear interpolation)
                float percentComplete = lerptimer / 4;
                // Smoothly transition the front XP bar towards the back XP bar
                frontXpBarNumber = Mathf.Lerp(FXP,backXpBarNumber, percentComplete);

            }

        }
        // Calculate the new scale for the XP slider based on the front XP bar value
        float NewScale = 1 - frontXpBarNumber;
        Vector3 NewScaleVector = new Vector3(NewScale,1,1);
        XpSlider.transform.localScale = NewScaleVector;
    }
    public void GainExperinceFlatRate(float xpGained)
    {
        // This method handles gaining experience points
        currentXP += xpGained;
        lerptimer = 0f;
    }
    public void LevelUp()
    {
        // This method handles leveling up
        level++;
        frontXpBarNumber = 0f;
        backXpBarNumber = 0f;
        currentXP = Mathf.RoundToInt(currentXP - RequiredXP);
        RequiredXP = CalculateRequiredXp();
        heal(100);

        StatPoints++;
        DisplayLevel();
        LevelupScreen.SetActive(true);
        Time.timeScale =0f;


    }
    private int CalculateRequiredXp()
    {
        // This method calculates the required XP for the next level

        // Initialize a variable to store the calculated XP
        int solveforRequiredXp = 0;
        // Loop through each level from 1 to the current level
        for (int LevelCycle = 1; LevelCycle <= level; LevelCycle++)
        {
            // Calculate the XP required for the current level and add it to the total
            solveforRequiredXp += (int)Mathf.Floor(LevelCycle + AdditionMultiplyer * Mathf.Pow(PowerMultiplyer, LevelCycle / DivisionMultiplyer));
        }
        return solveforRequiredXp / 4;
    }

    public void DisplayLevel()
    {
        // This method displays the current level
        LevelText.text = level.ToString();
    }

    





    // create multiple options of about 5 classes to choosse from,
    // each class have its own main weapon which continously attacks and a activated abilty, that has a cooldown
    // each class has 2 buff abiltys that realate to the weapon and abilty, each weapon and abilty has 5 upgrades each and you get to choose one upgrade each time you level up
    // when your weapon and corrosponding abilty both become max level you can choose to fuse them craeting a much stronger weapon


    //on each level up i want to get a stat point that i can use to select any one of my abiltys (either the projectile or the meteor) and increase the stats of said abilty up to 10 times each
    //the buffs will inrease the damage and every few levels add another projectile eaither another metoer or bullet
    //on level up change scenes to a scene where the icons are located
    //stats is a boolean
    //when you leve up and getthe stat point it would pause the game and open up the level up menu, that menu would have all the icons on it,
    //each abilty will have a level counter and show the current stats of the abilty and the next stats of the abilty
    //once stat point is spend automaticly unpasue the game and upgrade the abilty 

    //the damage and amount changes and cooldown speed 

    // wand damage      0: 50   1: 60   2:70    3:80    4:90    ect    
    // Meteor damage    0: 100  1: 110  2: 120  3: 130  4: 150  ect
    // Explosion dmage  0: 100  1: 110  2: 120  3: 130  4: 150  ect
    // Wand CD          0: 1    1: 1    2: 0.9  3: 0.9  4: 0.8  ect
    // Meteor CD        0: 5    1: 4.75 2: 4.50 3: 4.25 4: 4.00 ect 
    // Wand Amount      0: 1    1: 1    2: 1    3: 2    4: 2    5: 2    6: 3    7: ect 
    // Meteor Amount    0: 1    1: 1    2: 1    3: 1    4: 1    5: 2    6: 2    7: 2    8: 2    9: 2    10: 3

    // wandDamage[characterScript.level]
    // 

}