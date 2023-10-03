using UnityEngine;
using UnityEngine.UI;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject _loot;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _attackCooldown;
    [SerializeField] private SaveLoadManager _saveLoadManager;
    [SerializeField] private int _health;
    [SerializeField] private Image _healtBarImage;
    private int _damage = 8;
    private int _currentHealth;
    private float _cooldownTimer = Mathf.Infinity;
    public float MoveSpeed { get => _moveSpeed; }

    private void Start()
    {
        _currentHealth = _health;
        _saveLoadManager = FindAnyObjectByType<SaveLoadManager>();
    }

    private void Update()
    {
        _cooldownTimer += Time.deltaTime;
        if (_currentHealth <= 0)
        {
            Die();
        }
    }


    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healtBarImage.fillAmount = (float)_currentHealth / (float)_health;
    }

    private void Die()
    {
        Destroy(gameObject);
        Instantiate(_loot, transform.position, transform.rotation);
        _saveLoadManager.EnemySaves.Remove(gameObject);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerBehaviour>() != null)
        {
            if (_cooldownTimer > _attackCooldown)
            {
                collision.gameObject.GetComponent<PlayerBehaviour>().TakeDamage(_damage);
                _cooldownTimer = 0;
            }
        }
    }
}
