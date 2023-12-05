using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public PlayerTest playerTest;
    public bool isPassive = false;
    private bool isAttacking = false;


    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && !isAttacking && !isPassive)
        {
            // playerTest.TakeDamage(10);
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        isAttacking = true;
        playerTest.TakeDamage(10);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
