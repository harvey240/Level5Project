using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake(){
        currentHealth = maxHealth;
        healthBar.SetHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            TakeDamage(20);
        }
    }

    void TakeDamage(int damage){
        if(currentHealth - damage >= 0)
        {
            currentHealth -= damage;
        }
        healthBar.SetHealth(currentHealth);
    }

}
