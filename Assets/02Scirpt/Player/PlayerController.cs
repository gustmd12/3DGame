using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
public class PlayerController : MonoBehaviour
{
    public Camera mainCamera;
    private NavMeshAgent agent;

    private Transform currentTarget;

    private PlayerMovement playerMovement;
    private PlayerAttack playerAttack;
    private void Awake()
    {
        TryGetComponent<NavMeshAgent>(out agent);
        mainCamera = Camera.main;
        TryGetComponent<PlayerMovement>(out playerMovement);
        TryGetComponent<PlayerAttack>(out playerAttack);    
    }
    
    void Update()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    playerAttack.SetTarget(hit.collider.transform);
                    currentTarget = hit.collider.transform;
                    agent.SetDestination(currentTarget.transform.position);

                }
                else
                {
                    agent.SetDestination(hit.point);
                }

            }
           
        }
    }

    
}
