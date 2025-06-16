using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private float speed = 5f;
    private float damage = 5f;
    public void initTarget(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if(target == null)
        {
            Destroy(gameObject);
        }

        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * speed * Time.deltaTime;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && other.transform == target)
        {
            Debug.Log("공격 적중");
            IDamaged hit = other.GetComponent<IDamaged>();
            if (hit != null)
            {
                Destroy(gameObject);
                hit.TakeDamage(damage);
            }
        }
    }
}
