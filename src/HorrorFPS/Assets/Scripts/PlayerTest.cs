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

    public HUDManager hudManager;

    public HealthBar healthBar;
    public HeartHealthManager heartHealthManager;
    public HealthNumber healthNumber;
    public HealthVignette healthVignette;
    public HitFlash hitFlash;


    public AudioSource damageSound;
    public bool HasKey = false;

    // Start is called before the first frame update
    void Start()
    {
        hudManager.currentHealthUpdater.SetHealth(maxHealth, currentHealth);
        // healthNumber.SetHealth(maxHealth, currentHealth);
        // healthVignette.SetHealth(maxHealth,currentHealth);
        
        // healthBar.SetHealth(maxHealth);
        // heartHealthManager.createHearts(maxHealth, currentHealth);
    }

    void Awake(){
        currentHealth = maxHealth;

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

        hudManager.currentHealthUpdater.SetHealth(maxHealth, currentHealth);

        // healthBar.SetHealth(currentHealth);
        // heartHealthManager.createHearts(maxHealth, currentHealth);

        // healthNumber.SetHealth(maxHealth, currentHealth);
        // healthVignette.SetHealth(maxHealth,currentHealth);

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

        hudManager.currentHealthUpdater.SetHealth(maxHealth, currentHealth);

        // healthNumber.SetHealth(maxHealth, currentHealth);
        // healthVignette.SetHealth(maxHealth,currentHealth);

        // healthBar.SetHealth(currentHealth);
        // heartHealthManager.createHearts(maxHealth, currentHealth);
    }

}
