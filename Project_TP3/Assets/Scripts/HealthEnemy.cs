using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public void ReceiveDamage(int damage)
    {
        _health -= damage;
        Debug.Log("Health Enemy remaining: " + _health);

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}

