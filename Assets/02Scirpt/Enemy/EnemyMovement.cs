using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float patrolRadius = 10f;   
    public float waitTime = 2f;        

    private NavMeshAgent agent;
    private Vector3 patrolCenter;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolCenter = transform.position;  
        SetNewDestination();
    }

    void Update()
    {
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            waitTimer += Time.deltaTime;

            if (waitTimer >= waitTime)
            {
                SetNewDestination();
                waitTimer = 0f;
            }
        }
    }

    void SetNewDestination()
    {
        Vector3 randomPos = RandomNavSphere(patrolCenter, patrolRadius, -1);
        agent.SetDestination(randomPos);
    }

    
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
