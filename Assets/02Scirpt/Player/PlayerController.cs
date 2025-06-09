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

    [SerializeField] GameObject mouseClickEffect;


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
                    ShowMoveClickEffect(hit.point,hit.normal);

                }

            }
           
        }
    }

    private void ShowMoveClickEffect(Vector3 postion, Vector3 normal)
    {
        if (mouseClickEffect == null) return;


        Vector3 effectPos = new Vector3(postion.x, postion.y + 0.5f, postion.z);
        Quaternion effect = Quaternion.FromToRotation(Vector3.up, normal);

        GameObject Effect = Instantiate(mouseClickEffect, effectPos, Quaternion.Euler(90,0,0));

        Destroy(Effect, 0.5f);
    }

    
}
