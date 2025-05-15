using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetProjectile : MonoBehaviour
{

    private Vector3 direction;
    private float speed;
    public void Init(Vector3 dir, float spd)
    {
        direction = dir;
        speed = spd;

    }

    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

    }
}
