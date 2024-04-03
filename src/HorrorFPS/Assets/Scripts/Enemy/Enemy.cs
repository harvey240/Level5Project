using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float health = 50f;
    public PlayerTest playerTest;
    public bool isPassive = false;
    private bool isAttacking = false;
    public Animator animator = null;
    public EnemyController enemyController;
    [SerializeField] private AudioClip[] damageSoundClips;
    [HideInInspector]
    public bool dead = false;
    private UnityEngine.Object explosionRef;
    SpriteRenderer explosion;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        explosionRef = Resources.Load("EnemyExplode");
    }

    public void TakeDamage(float amount)
    {
        if (!isPassive)
        {
            SoundFXManager.instance.PlayRandomSoundFXClip(damageSoundClips, transform, 1f);
            enemyController.isPursuing = true;
        }
        
        health -= amount;
        if (health <= 0f)
        {
            Die();
        }
    }

    void OnTriggerStay(Collider collider)
    {
        if (collider.CompareTag("Player") && !isAttacking && !isPassive && !dead && !playerTest.isDead)
        {
            // playerTest.TakeDamage(10);
            StartCoroutine(attack());
        }
    }

    IEnumerator attack()
    {
        isAttacking = true;
        animator.SetTrigger("Attack");
        playerTest.TakeDamage(20);
        yield return new WaitForSeconds(2.4f);
        isAttacking = false;
    }

    void Die()
    {
        dead = true;
        if (!isPassive)
        {
            GameObject explosion = (GameObject) Instantiate(explosionRef);
            explosion.transform.position = new Vector3(transform.position.x, transform.position.y + .3f, transform.position.z);
            animator.SetTrigger("Dead");
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(WaitToDestroy());
        }
        else
        {
            Destroy(gameObject);
        }

        
        
    }

    private IEnumerator WaitToDestroy()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }
}
