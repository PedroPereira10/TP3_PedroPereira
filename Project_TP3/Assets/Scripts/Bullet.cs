using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _speed = 10f;
    private Vector3 _direction;

    public void Initialize(Vector3 direction)
    {
        _direction = direction.normalized;
    }

    void Update()
    {
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        HealthPlayer targetHealth = other.GetComponent<HealthPlayer>();
        if (targetHealth != null)
        {
            targetHealth.ReceiveDamage(_damage); 
            Destroy(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
