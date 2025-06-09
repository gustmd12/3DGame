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
        direction = (target.position - transform.position).normalized;

        transform.position += direction * speed * Time.deltaTime;

        float dist = Vector3.Distance(transform.position, target.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform == target)
        {
            Debug.Log("Q РћСп");
            IDamaged hit = other.GetComponent<IDamaged>();
            if (hit != null)
            {
                Instantiate(hitef, target);
                Destroy(gameObject);
                hit.TakeDamage(10);
            }
        }
    }



}
