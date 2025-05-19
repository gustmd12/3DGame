using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetProjectile : MonoBehaviour
{
    private Transform target;

    private Vector3 direction;
    private float speed;
    public void Init(Vector3 dir, float spd, Transform _target)
    {
        direction = dir;
        speed = spd; 
        target = _target;

    }
    
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < 0.3f)
        {
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Q ´êÀ½");
        }
    }
}
