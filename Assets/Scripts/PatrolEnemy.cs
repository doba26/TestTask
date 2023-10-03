using UnityEngine;

public class PatrolEnemy : MonoBehaviour
{
    [SerializeField] private float _startWaitTime;
    [SerializeField] private GameObject[] _points;
    [SerializeField] private float _stopingDistance;
    private float _waitTime;
    private int _randomPoint;
    private float _speed;
    private Transform _player;
    private bool _isPatrol = false;
    private bool _isAngry = false;
    private Vector3 _moveVector;
    public Vector3 MoveVector { get => _moveVector; }


    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _points = GameObject.FindGameObjectsWithTag("Point");
        _speed = GetComponent<EnemyBehaviour>().MoveSpeed;
        _waitTime = _startWaitTime;
        _randomPoint = Random.Range(0, _points.Length);
    }

    private void Update()
    {
        if (_player != null)
        {
            if (Vector2.Distance(transform.position, _player.position) < _stopingDistance)
            {
                _isAngry = true;
                _isPatrol = false;
            }
            if (Vector2.Distance(transform.position, _player.position) > _stopingDistance && _isAngry == false)
            {
                _isPatrol = true;
                _isAngry = false;
            }
            _moveVector = new Vector3(transform.position.x, transform.position.y);
            if (_isPatrol)
            {
                PatrolPlace();
            }
            else if (_isAngry)
            {
                Angry();
        }
    }
        else if (_player.transform == null)
        {
            PatrolPlace();
}
    }

    private void Angry()
    {
        transform.position = Vector2.MoveTowards(transform.position, _player.position, _speed * Time.deltaTime);
    }

    public void PatrolPlace()
    {
        {
            transform.position = Vector2.MoveTowards(transform.position, _points[_randomPoint].transform.position, _speed * Time.deltaTime);
            if (_points[_randomPoint].transform.position.x > transform.position.x)
            {
                transform.localScale = new Vector2(2.3f, 2.3f);
            }
            else if (_points[_randomPoint].transform.position.x < transform.position.x)
            {
                transform.localScale = new Vector2(-2.3f, 2.3f);
            }
            if (Vector2.Distance(transform.position, _points[_randomPoint].transform.position) < 0.2f)
            {

                if (_waitTime <= 0)
                {
                    _randomPoint = Random.Range(0, _points.Length);
                    _waitTime = _startWaitTime;
                }
                else
                {
                    _waitTime -= Time.deltaTime;
                }
            }
        }
    }

    public void LoadDataEnemy(Save.EnemySaveData save)
    {
        transform.position = new Vector3(save.Position.x, save.Position.y, save.Position.z);
        transform.position = new Vector3(save.Direction.x, save.Direction.y, save.Direction.z);
    }
}
