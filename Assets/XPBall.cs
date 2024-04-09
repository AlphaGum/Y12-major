using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPBall : MonoBehaviour
{

    public CharacterScript characterScript;
    public int XPAmount;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.tag == "Player")
        {
            CharacterScript characterScript = collision.gameObject.GetComponent<CharacterScript>();
            characterScript.GainExperinceFlatRate(XPAmount);
            Destroy(gameObject);
        }
    }

    
}
