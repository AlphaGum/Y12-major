using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiScript : MonoBehaviour
{
    public GameObject LevelupScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Staff playerStats; // A reference to the PlayerStats script

    // This method should be called when the Wand button is clicked
    public void OnClickWandButton()
    {
        playerStats.LevelUpWand(); // Call the method to level up the Wand
        LevelupScreen.SetActive(false);
        Time.timeScale = 1f;


    }

    // This method should be called when the Meteor button is clicked
    public void OnClickMeteorButton()
    {
        playerStats.LevelUpMeteor(); // Call the method to level up the Meteor
        LevelupScreen.SetActive(false);
        Time.timeScale = 1f;


    }
}
