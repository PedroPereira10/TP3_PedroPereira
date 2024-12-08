using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 1f;
    [SerializeField] private float _attackCoolDown = 1.5f;
    [SerializeField] private int _damage = 20;
    [SerializeField] private List<Crystal> _collectedCrystals = new List<Crystal>();

    private Camera _camera;
    private Rigidbody _rb;
    private Animator _animator;
    private HealthEnemy _currentEnemy;
    private Vector3 _targetPosition;
    private bool _attackIsActive;

    void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        Crystal crystal = other.GetComponent<Crystal>();
        if (crystal != null && !crystal.IsCollected)
        {
            crystal.IsCollected = true;  
            _collectedCrystals.Add(crystal);  
            crystal.gameObject.SetActive(false); 
            Debug.Log("Cristal collecté !");
        }
    }

    public bool HasCrystalToDeposit()
    {
        return _collectedCrystals.Count > 0; 
    }

    
    public void RemoveCrystal()
    {
        if (_collectedCrystals.Count > 0)
        {
            Crystal crystalToRemove = _collectedCrystals[0];
            _collectedCrystals.RemoveAt(0);  
            Debug.Log("Cristal déposé sur le socle !");
        }
    }


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Ray ray;
            RaycastHit hit;
            ray = _camera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit))
            {
                HealthEnemy enemy = hit.collider.gameObject.GetComponent<HealthEnemy>();

                if (enemy != null)
                {
                    _currentEnemy = enemy;
                    _attackIsActive = true;
                }

                else
                {
                    _currentEnemy = null;
                    _targetPosition = hit.point;
                    transform.LookAt(_targetPosition);
                }

            }
        }

        if (_currentEnemy != null)
        {
            _targetPosition = _currentEnemy.transform.position;
            transform.LookAt(_currentEnemy.transform.position);
        }

        float distance = (transform.position - _targetPosition).magnitude;
        Vector3 direction = (_targetPosition - transform.position).normalized;

        if (distance > _stoppingDistance)
        {
            _rb.velocity = _movementSpeed * direction;
            _animator.SetBool("IsWalking", true);
        }

        else
        {
            _animator.SetBool("IsWalking", false);
            _rb.velocity = Vector3.zero;
        }

        if (_attackIsActive && distance < _stoppingDistance)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetBool("IsAttacking", true);
        _attackIsActive = false;
        _currentEnemy.ReceiveDamage(_damage);
    }

    public void ResetAttack()
    {
        _animator.SetBool("IsAttacking", false);
    }
}

