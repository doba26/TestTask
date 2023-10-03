using UnityEngine;

public class CameraMOve : MonoBehaviour
{
    [SerializeField] private Transform _player;
    void Update()
    {
        if(_player != null)
        {
            Vector3 temp = transform.position;
            temp.x = _player.position.x;
            temp.y = _player.position.y;

            transform.position = temp;
        }
        else
        {
            TransformCamera();
        }
    }

    private void TransformCamera()
    {
        Vector3 temp = transform.position;
        temp.x = 0f;
        temp.y = 0f;
    }
}
