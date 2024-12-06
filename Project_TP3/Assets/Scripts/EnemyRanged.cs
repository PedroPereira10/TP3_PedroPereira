using System.Collections;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{
    [SerializeField] private float _stoppingDistance = 10f;
    [SerializeField] private float _attackCoolDown = 1.5f;
    [SerializeField] private GameObject _projectilePrefab;
    [SerializeField] private Transform _shootPoint;

    private Transform _target;
    private Animator _animator;
    private float _lastAttackTime;
    private bool _isAttacking;

    void Start()
    {
        _animator = GetComponent<Animator>();

        GameObject player = GameObject.Find("Player");
        if (player != null)
        {
            _target = player.transform;
        }
        else
        {
            Debug.LogError("Player not found! Please ensure the Player object exists in the scene.");
        }
    }

    void Update()
    {
        if (_target == null) return;

        float distance = Vector3.Distance(transform.position, _target.position);

        if (distance <= _stoppingDistance)
        {
            LookAtTarget();

            if (Time.time >= _lastAttackTime + _attackCoolDown && !_isAttacking)
            {
                StartCoroutine(AttackRoutine());
            }
        }
        else
        {
            _animator.SetBool("IsAttacking", false);
        }
    }

    private void LookAtTarget()
    {
        Vector3 lookDirection = (_target.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(new Vector3(lookDirection.x, 0, lookDirection.z));
    }

    private IEnumerator AttackRoutine()
    {
        _isAttacking = true; // Signale qu'une attaque est en cours
        _animator.SetBool("IsAttacking", true);

        yield return new WaitForSeconds(0.5f); // Attente avant de tirer

        if (_projectilePrefab != null && _shootPoint != null)
        {
            ShootProjectile();
        }

        _lastAttackTime = Time.time;

        yield return new WaitForSeconds(0.5f); // Temps d'animation de l'attaque

        _animator.SetBool("IsAttacking", false);
        _isAttacking = false; // Attaque terminée, prêt à attaquer à nouveau
    }

    private void ShootProjectile()
    {
        GameObject projectile = Instantiate(_projectilePrefab, _shootPoint.position, Quaternion.identity);
        Vector3 direction = (_target.position - transform.position).normalized;
        Bullet projectileScript = projectile.GetComponent<Bullet>();
        if (projectileScript != null)
        {
            projectileScript.Initialize(direction);
        }
    }
}
