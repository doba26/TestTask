using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private int _damage;
    private bool hit;
    private float _lifeTime;
    private float _direction;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void FixedUpdate()
    {
        if (hit) return;
        transform.Translate(0, _speed * _direction, 0);
        _lifeTime += Time.deltaTime;
        if (_lifeTime > 2)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        gameObject.SetActive(false);
        _collider.enabled = false;
        if (collision.gameObject.GetComponent<EnemyBehaviour>() != null)
        {
            collision.gameObject.GetComponent<EnemyBehaviour>().TakeDamage(_damage);
        }
    }

    public void SetDirection(float direction)
    {
        _lifeTime = 0;
        _direction = -direction;
        gameObject.SetActive(true);
        hit = false;
        _collider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector2(localScaleX, transform.localScale.y);
    }
}
