using TreeEditor;
using UnityEngine;

public class LaserProjectile : MonoBehaviour
{
    private float speed = 1.5f;
    private float lifeTime = 2f;
    private float damage = 3f;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position = transform.forward * speed * Time.deltaTime;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            var target = other.GetComponent<IDamaged>();
            target?.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
