using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public float height = 11.0f;
    public float distance = 5.0f;

    private Vector3 offDir = new Vector3(1f,1f, -1f);


    private void Start()
    {
        
        
    }
    private void LateUpdate()
    {
        Vector3 offset = offDir.normalized * distance;
        offset.y += height;

        transform.position = player.position + offset;
        transform.LookAt(player);
        
    }
}
