using UnityEngine;

public class ShootPlayer : MonoBehaviour
{
    [SerializeField] private float _shootCooldown;
    [SerializeField] private Transform _bulletSpawn;
    [SerializeField] private GameObject[] _bullets;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private int _distance;
    private RaycastHit2D hitInfo;
    private float _cooldownTimer = Mathf.Infinity;

   
    private void Update()
    {
        SearchEnemy();
        _cooldownTimer += Time.deltaTime;
    }

    public void Shoot()
    {
        _cooldownTimer = 0;
        _bullets[FindBullet()].transform.position = _bulletSpawn.position;
        _bullets[FindBullet()].GetComponent<Projectile>().SetDirection(Mathf.Sign(-transform.localScale.x));
    }

    private int FindBullet()
    {
        for(int i = 0; i < _bullets.Length; i++)
        {
            if (_bullets[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void SearchEnemy()
    {
        if (transform.localScale.x <= 0.5)
        {
            Debug.DrawRay(transform.position, -transform.right * _distance);
            hitInfo = Physics2D.Raycast(transform.position, -transform.right, _distance, _mask);
        }
        else if (transform.localScale.x >= -0.5)
        {
            Debug.DrawRay(transform.position, transform.right * _distance);
            hitInfo = Physics2D.Raycast(transform.position, transform.right, _distance, _mask);
        }
        if (hitInfo.collider != null)
        {
            if (hitInfo.collider.GetComponent<EnemyBehaviour>() )
            {
                if (_cooldownTimer > _shootCooldown)
                {
                    Shoot();
                }
            }
        }
    }
}
