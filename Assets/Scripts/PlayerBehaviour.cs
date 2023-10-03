using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] private FloatingJoystick _joystick;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rb;
    [SerializeField] private int _health;
    [SerializeField] private InventoryManager _inventoryManager;
    [SerializeField] private GameObject _healthBar;
    [SerializeField] private Image _healtBarImage;
    private int _currentHealth;

    private Vector3 _moveVector;
    public Vector3 MoveVector { get => _moveVector; }

    void Start()
    {
        _currentHealth = _health;
    }

    void Update()
    {
        if (_currentHealth <= 0)
        {
            Die();
        }
        Flip();
    }

    private void FixedUpdate()
    {
        Run();
        _moveVector = new Vector3(transform.position.x, transform.position.y);
    }

    private void Flip()
    {
        if (_joystick.Horizontal > 0)
        {
            transform.localScale = new Vector2(2.3f, 2.3f);
        }
        else if (_joystick.Horizontal < 0)
        {
            transform.localScale = new Vector2(-2.3f, 2.3f);
        }
        //_spriteRenderer.flipX = _joystick.Horizontal < 0;
    }

    private void Run()
    {
        _rb.velocity = new Vector2(_joystick.Horizontal * _moveSpeed, _joystick.Vertical * _moveSpeed);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        _healtBarImage.fillAmount = (float)_currentHealth / (float)_health;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Item>())
        {
            _inventoryManager.AddItem(collision.gameObject.GetComponent<Item>().ItemSO, collision.gameObject.GetComponent<Item>().Amount);
            Destroy(collision.gameObject);
        }
    }

    public void LoadDataPlayer(Save.PlayerSaveData save)
    {
        transform.position = new Vector3(save.Position.x, save.Position.y, save.Position.z);
        transform.position = new Vector3(save.Direction.x, save.Direction.y, save.Direction.z);
    }
}
