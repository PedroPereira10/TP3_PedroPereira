using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player;  
    [SerializeField] private Vector3 _offset;   
    [SerializeField] private float _smoothSpeed = 0.125f; 

    private void LateUpdate()
    {
        if (_player != null)
        {
            Vector3 desiredPosition = _player.position + _offset;

            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothSpeed);

            transform.position = smoothedPosition;
        }
    }
}
