using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private int _damage;
    [SerializeField] private float _speed;
    [SerializeField] private float _yOffset = 1.5f;
    [SerializeField] private GameObject _explosionVFX; 
    [SerializeField] private float _explosionDelay = 1.5f;

    private Transform _target;
    private Rigidbody _rb;
    private bool _hasExplosed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        if (_explosionVFX != null)
        {
            _explosionVFX.SetActive(false);
        }
    }

    private void Update()
    {
        if (_hasExplosed) return; 

        Vector3 direction = (_target.position + new Vector3(0, _yOffset, 0) - transform.position).normalized;
        _rb.velocity = direction * _speed;
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthEnemy>() != null && !_hasExplosed)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        _hasExplosed = true; 
        _rb.velocity = Vector3.zero; 
        transform.localScale = Vector3.one * _radius * 2; 

       
        if (_explosionVFX != null)
        {
            _explosionVFX.SetActive(true);
        }

        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in hitColliders)
        {
            HealthEnemy health = collider.GetComponent<HealthEnemy>();
            if (health != null)
            {
                health.ReceiveDamage(_damage);
            }
        }

        Destroy(gameObject, _explosionDelay);
    }
}
