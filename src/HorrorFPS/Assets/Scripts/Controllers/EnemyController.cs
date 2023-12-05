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
    bool walkPointSet=false;
    public float walkPointRange;

    Transform target;
    NavMeshAgent agent;
    public Animator anim = null;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.transform;
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.localPosition);

        if (distance <= lookRadius && fov.canSeePlayer)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                // TODO: Attack the target

                //Face the target
                FaceTarget();
            }
        }
        
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        Vector3 point;
        if (RandomWalkPoint(agent.transform.position, walkPointRange, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            agent.SetDestination(point);
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
