using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public float patrolRadius = 10f;   // 몬스터가 돌아다닐 반경
    public float waitTime = 2f;        // 목적지 도착 후 대기 시간

    private NavMeshAgent agent;
    private Vector3 patrolCenter;
    private float waitTimer;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolCenter = transform.position;  // 처음 위치를 중심으로 patrol
        SetNewDestination();
    }

    void Update()
    {
        // 목적지에 거의 도착했으면
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

    // 반경 내 랜덤 위치 계산 함수
    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;

        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}
