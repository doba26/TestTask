using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemy;
    private Vector2 _position;

    private void Start()
    {
        Spawn();
    }

    private void Spawn()
    {
        for (int i = 0; i < _enemy.Length; i++)
        {
            _position.y = Random.Range(-10, 10);
            _position.x = Random.Range(-12, 20);

            _enemy[i].transform.position = new Vector2(_position.x, _position.y);
            _enemy[i].SetActive(true);

        }
    }
}
