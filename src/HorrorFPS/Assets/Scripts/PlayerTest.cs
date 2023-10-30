using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public HeartHealthManager heartHealthManager;
    public HealthNumber healthNumber;
    public HitFlash hitFlash;
    public AudioSource damageSound;
    public bool HasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        healthNumber.SetHealth(maxHealth, currentHealth);
    }

    void Awake(){
        currentHealth = maxHealth;

        // healthBar.SetHealth(maxHealth);
        // heartHealthManager.createHearts(maxHealth, currentHealth);
        // healthNumber.SetHealth(maxHealth, currentHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K)){
            print("K key pressed");
            TakeDamage(10);
        }
    }

    public void TakeDamage(int damage){
        if(currentHealth - damage >= 0)
        {
            currentHealth -= damage;
        }

        hitFlash.DisplayHitFlash();
        damageSound.PlayOneShot(damageSound.clip);

        // healthBar.SetHealth(currentHealth);
        // heartHealthManager.createHearts(maxHealth, currentHealth);
        healthNumber.SetHealth(maxHealth, currentHealth);
    }

    public void Heal(int health)
    {
        if (currentHealth < maxHealth)
        {
            if (currentHealth + health >maxHealth)
                currentHealth=maxHealth;
            else
                currentHealth += health;
        }

        healthNumber.SetHealth(maxHealth, currentHealth);
    }

}
