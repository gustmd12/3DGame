using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    private NavMeshAgent agent;

    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
        mainCamera = Camera.main;
    }

    void Update()
    {
        if(Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
