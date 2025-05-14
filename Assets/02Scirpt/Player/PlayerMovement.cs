using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    private float rotationSpeed = 720f;

    private PlayerAnimController animController;

    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
        TryGetComponent<PlayerAnimController>(out  animController);
        agent.updateRotation = false;
    }
    void Update()
    {
        RotationInput();
        AnimUpdate();
        RotationUpdate();
    }

    void RotationInput()
    {
        if (agent.velocity.sqrMagnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(agent.velocity.normalized);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void AnimUpdate()
    {
        float speed = agent.velocity.magnitude;

        animController.PlayerMove(speed > 0.1f);
    }
    
    void RotationUpdate()
    {
        RaycastHit hit;

        if(Physics.Raycast (transform.position + Vector3.up, Vector3.down, out hit,2f))
        {
            Vector3 GroundNomal1 = hit.normal;
            Quaternion slopeRotation = Quaternion.FromToRotation(transform.up,GroundNomal1) * transform.rotation;
            transform.rotation = Quaternion.Slerp(transform.rotation, slopeRotation, Time.deltaTime * 10f);
        }
    }
}
