using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed = 2f;

    public void initTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;


        float dist = Vector3.Distance(transform.position, target.position);
        if(dist < 0.3f)
        {
            Destroy(gameObject);
        }
    }
}
