using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player; // Referência ao Transform do jogador
    [SerializeField] private Vector3 _offset; // Deslocamento inicial da câmera em relação ao jogador
    [SerializeField] private float _smoothSpeed = 0.125f; // Velocidade de suavização do movimento da câmera

    [SerializeField] private Vector2 _xBounds; // Limites para o eixo X da câmera
    [SerializeField] private Vector2 _zBounds; // Limites para o eixo Z da câmera

    private void Start()
    {
        if (_player != null)
        {
            // Calcula o deslocamento inicial com base na posição inicial do jogador e da câmera
            _offset = transform.position - _player.position;
        }
        else
        {
            Debug.LogWarning("O jogador não foi atribuído ao script CameraFollow.");
        }
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            // Calcula a posição desejada da câmera com base na posição do jogador e no deslocamento
            Vector3 desiredPosition = _player.position + _offset;

            // Restringe a posição desejada da câmera aos limites especificados
            float clampedX = Mathf.Clamp(desiredPosition.x, _xBounds.x, _xBounds.y);
            float clampedZ = Mathf.Clamp(desiredPosition.z, _zBounds.x, _zBounds.y);

            // Mantém a altura (Y) sem clamping
            Vector3 clampedPosition = new Vector3(clampedX, desiredPosition.y, clampedZ);

            // Suaviza o movimento da câmera para a posição desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, _smoothSpeed);

            // Atualiza a posição da câmera
            transform.position = smoothedPosition;
        }
    }
}
