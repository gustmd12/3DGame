using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TargetProjectile : MonoBehaviour
{
    private Transform target;

    private Vector3 direction;
    private float speed;
    private GameObject hitef;

    public void Init(Vector3 dir, float spd, Transform _target, GameObject hit)
    {
        direction = dir;
        speed = spd; 
        target = _target;
        hitef = hit;
    }
    
    private void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        float dist = Vector3.Distance(transform.position, target.position);
        if (dist < 0.3f)
        {
            Destroy(gameObject);
            Instantiate(hitef,target);
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
