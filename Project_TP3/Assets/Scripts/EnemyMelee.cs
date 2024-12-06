using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 5f;
    [SerializeField] private float _stoppingDistance = 1f;
    [SerializeField] private float _attackCoolDown = 1.5f;
    [SerializeField] private int _damage = 20;

    private Transform _target;
    private Rigidbody _rb;
    private Animator _animator;
    private HealthPlayer _targetHealth;
    private bool _isAttacking;
    private float _lastAttackTime;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponentInChildren<Animator>();

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            _target = player.transform;
            _targetHealth = player.GetComponent<HealthPlayer>();
        }
        else
        {
            Debug.LogError("Player not found! Please ensure the Player object exists in the scene.");
        }
    }

    void Update()
    {
        if (_target == null || _targetHealth == null) return;

        Vector3 targetPosition = new Vector3(_target.position.x, transform.position.y, _target.position.z);
        float distance = Vector3.Distance(transform.position, targetPosition);
        Vector3 direction = (targetPosition - transform.position).normalized;

        if (distance > _stoppingDistance)
        {
            MoveTowardsTarget(direction);
        }
        else
        {
            StopAndAttack(distance);
        }
    }

    private void MoveTowardsTarget(Vector3 direction)
    {
        transform.LookAt(new Vector3(_target.position.x, transform.position.y, _target.position.z));

        _rb.velocity = _movementSpeed * direction;
        _animator.SetBool("IsWalking", true);
        _animator.SetBool("IsAttacking", false);
    }

    private void StopAndAttack(float distance)
    {
        _rb.velocity = Vector3.zero;
        _animator.SetBool("IsWalking", false);

        if (distance <= _stoppingDistance && Time.time >= _lastAttackTime + _attackCoolDown)
        {
            Attack();
        }
    }

    private void Attack()
    {
        _animator.SetBool("IsAttacking", true);
        _isAttacking = true;

        if (_targetHealth != null)
        {
            _targetHealth.ReceiveDamage(_damage);
        }

        _lastAttackTime = Time.time;
    }

    public void ResetAttack()
    {
        _animator.SetBool("IsAttacking", false);
        _isAttacking = false;
    }
}
