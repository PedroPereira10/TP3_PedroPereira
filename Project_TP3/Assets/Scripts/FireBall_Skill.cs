using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall_Skill : MonoBehaviour
{
    [SerializeField] private FireBall _fireBall;
    [SerializeField] private Transform _CharacterHand;
    [SerializeField] private float _coolDownDelay = 5f;
    [SerializeField] private float _animationDelay = 0.5f;
    [SerializeField] private Animator _animator;

    private float _timer;

    void Update()
    {
        _timer += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Mouse1) && _timer > _coolDownDelay)
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                HealthEnemy health = hit.collider.GetComponent<HealthEnemy>();
                if (health != null)
                {
                    _animator.transform.parent.LookAt(health.transform.position);
                    StartCoroutine(SendFireBall(health.transform));
                }
            }
        }
    }

    public float GetCoolDownRatio()
    {
        return 1 - (_timer / _coolDownDelay);
    }

    private IEnumerator SendFireBall(Transform target)
    {
        _animator.SetBool("IsFiring", true);
        yield return new WaitForSeconds(_animationDelay);
        FireBall newFireBall = Instantiate(_fireBall, _CharacterHand.position, Quaternion.identity);
        newFireBall.SetTarget(target);
        _timer = 0;
        yield return new WaitForSeconds(_animationDelay);
        _animator.SetBool("IsFiring", false);
    }
}
