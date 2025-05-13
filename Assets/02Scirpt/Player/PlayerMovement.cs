using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private float rotSpeed = 360f;
    private float rotationSpeed = 5f;


    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
    }
    void Update()
    {
        RotationInput();
    }

    void RotationInput()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
    
}
