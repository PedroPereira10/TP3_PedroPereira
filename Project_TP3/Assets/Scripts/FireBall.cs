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
    }

    private void Update()
    {
        Vector3 direction = (_target.position + new Vector3(0, _yOffset, 0) - transform.position).normalized;
        if (!_hasExplosed)
        {
            _rb.velocity = direction * _speed;
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<HealthPlayer>() != null && !_hasExplosed)
        {
            Explosion();
            Debug.Log("Explosion done");
        }
    }

    private void Explosion()
    {
        transform.localScale = Vector3.one * _radius * 2;
        _explosionVFX.SetActive(true);
        Collider[] hitCollider = Physics.OverlapSphere(transform.position, _radius);
        foreach (Collider collider in hitCollider)
        {
            HealthPlayer health = collider.GetComponent<HealthPlayer>();
            if (health != null)
            {
                health.ReceiveDamage(_damage);
            }
        }
        _hasExplosed = true;
        _rb.velocity = Vector3.zero;
        Destroy(gameObject, _explosionDelay);
    }
}
