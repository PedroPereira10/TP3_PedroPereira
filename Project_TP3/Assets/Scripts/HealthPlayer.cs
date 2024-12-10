using UnityEngine;
using UnityEngine.UI;

public class HealthPlayer : MonoBehaviour
{
    [SerializeField] private int _health = 100;
    [SerializeField] private Image _healthBarFill;
    [SerializeField] private GameManager _gameManager;

    private int _maxHealth;

    void Start()
    {
        _maxHealth = _health;
        UpdateHealthBar();

        if (_gameManager == null)
        {
            _gameManager = FindObjectOfType<GameManager>();
        }
    }

    public void ReceiveDamage(int damage)
    {
        _health -= damage;
        _health = Mathf.Clamp(_health, 0, _maxHealth);

        Debug.Log("Health player remaining: " + _health);

        UpdateHealthBar();

        if (_health <= 0)
        {
            Destroy(gameObject);

            if (_gameManager != null)
            {
                _gameManager.GameOver();
            }

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayLoseSound();
            }
        }
    }

    private void UpdateHealthBar()
    {
        if (_healthBarFill != null)
        {
            _healthBarFill.fillAmount = (float)_health / _maxHealth;
        }
    }
}
