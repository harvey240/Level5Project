using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public FieldOfView fov;

    // Patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    public Enemy enemyScript;
    public float smoothBlend = 0.1f;
    [SerializeField] private AudioClip[] ambientSoundClips;

    Transform target;
    NavMeshAgent agent;
    private Animator animator = null;
    [HideInInspector] public bool isPursuing = false;
    private float defaultSpeed;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        enemyScript = GetComponent<Enemy>();
        Debug.Log(animator);
        defaultSpeed = agent.speed;

        StartCoroutine(PlayAmbientSoundsRandomly());
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyScript.dead)
        {
            enabled = false;
        }
        
        // Distance between the enemy and the player
        float distance = Vector3.Distance(target.position, transform.localPosition);

        if ((distance <= lookRadius && fov.canSeePlayer) || isPursuing)
        {
            isPursuing = true;
            Debug.Log("Pursuing");

            agent.speed = defaultSpeed;
            Debug.Log(agent.speed);
            walkToDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                //Face the target
                FaceTarget();
                animator.SetFloat("Speed", 0f, smoothBlend, Time.smoothDeltaTime);
            }

            if (distance > lookRadius)
            {
                agent.speed = 2 * defaultSpeed;
                animator.SetFloat("Speed", 1f, smoothBlend, Time.smoothDeltaTime);
                Debug.Log(agent.speed);
            }
            
        }

        if (!fov.canSeePlayer && isPursuing)
        {
            StartCoroutine(endPursuit());
        }


        
        else if (agent.remainingDistance <= agent.stoppingDistance && !isPursuing)
        {
            Patrol();
        }



    }
    private IEnumerator endPursuit()
    {
        yield return new WaitForSeconds(5f);
        isPursuing = false;
        Debug.Log("Not Pursuing");
    }

    private void walkToDestination(Vector3 destination)
    {
        agent.SetDestination(destination);
        animator.SetFloat("Speed", 0.5f, smoothBlend, Time.smoothDeltaTime);
    }

    private void Patrol()
    {
        Vector3 point;
        if (RandomWalkPoint(agent.transform.position, walkPointRange, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            walkToDestination(point);
        }

        // if (!walkPointSet) SearchWalkPoint();

        // if (walkPointSet) agent.SetDestination(walkPoint);

        // Vector3 distanceToWalk = transform.position - walkPoint;

        // if (distanceToWalk.magnitude < 1f) walkPointSet = false;
    }

    bool RandomWalkPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; 
        NavMeshHit hit;

        // 1.0f is distance from randomPoint to a point on the navMesh and can be increased if I want range to be bigger
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private IEnumerator PlayAmbientSoundsRandomly()
    {
        while (true)
        {
            if (!isPursuing)
            {
                float randomInterval = Random.Range(5f,15f);
                yield return new WaitForSeconds(randomInterval);
                SoundFXManager.instance.PlayRandomSoundFXClip(ambientSoundClips, transform, 1f);
            }
            yield return null;
        }
    }

    // private void SearchWalkPoint()
    // {
    //     float randomZ = Random.Range(-walkPointRange, walkPointRange);
    //     float randomX = Random.Range(-walkPointRange, walkPointRange);

    //     walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
    //     walkPointSet = true;
    // }

    void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

    }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.localPosition, lookRadius);
    }
}
