using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
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
        frontXpBarNumber = currentXP / RequiredXP;
        backXpBarNumber = currentXP / RequiredXP;
        RequiredXP = CalculateRequiredXp();

        Vector3 StartVector = new Vector3(0,1,1);
        healthSlider.transform.localScale = StartVector;

    }

    void Update()
    {
        movement();
        

        if(Input.GetKeyDown(KeyCode.Return))
        {
            takedamage(20);
        }

        UpdateXpUI();
        if (Input.GetKeyDown(KeyCode.Equals))
            GainExperinceFlatRate(20);

        if (currentXP > RequiredXP)
        {
            LevelUp();
        }

    }

    public void movement()
    {
        moveinput.x = Input.GetAxisRaw("Horizontal");
        moveinput.y = Input.GetAxisRaw("Vertical");

        moveinput.Normalize();

        rb2d.velocity = moveinput * moveSpeed;
    }
    private void healthbarupdate()
    {
        float NewScale = 1 - healthBarNumber;
        Vector3 NewScaleVector = new Vector3(NewScale, 1, 1);
        healthSlider.transform.localScale = NewScaleVector;
    }
    public void takedamage(float damage)
    {
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
        HealthAmount += healingAmount;
        HealthAmount = Mathf.Clamp(HealthAmount, 0, 100);
        healthBarNumber = HealthAmount / 100;
        healthbarupdate();
    }
    public void UpdateXpUI()
    {
        float xpFraction = currentXP / RequiredXP;
        float FXP = frontXpBarNumber;
        if(FXP < xpFraction)
        {
            delaytimer += Time.deltaTime;
            backXpBarNumber = xpFraction;
            if(delaytimer > 1.5)
            {
                lerptimer += Time.deltaTime;
                float percentComplete = lerptimer / 4;
                // TODO: change fill amount to scale, and invert the number
                frontXpBarNumber = Mathf.Lerp(FXP,backXpBarNumber, percentComplete);

            }

        }
        float NewScale = 1 - frontXpBarNumber;
        Vector3 NewScaleVector = new Vector3(NewScale,1,1);
        XpSlider.transform.localScale = NewScaleVector;
    }
    public void GainExperinceFlatRate(float xpGained)
    {
        currentXP += xpGained;
        lerptimer = 0f;
    }
    public void LevelUp()
    {
        level++;
        frontXpBarNumber = 0f;
        backXpBarNumber = 0f;
        currentXP = Mathf.RoundToInt(currentXP - RequiredXP);
        RequiredXP = CalculateRequiredXp();
        heal(100);
       
       
    }
    private int CalculateRequiredXp()
    {
        int solveforRequiredXp = 0;
        for (int LevelCycle = 1; LevelCycle <= level; LevelCycle++)
        {
            solveforRequiredXp += (int)Mathf.Floor(LevelCycle + AdditionMultiplyer * Mathf.Pow(PowerMultiplyer, LevelCycle / DivisionMultiplyer));
        }
        return solveforRequiredXp / 4;
    }

    
}