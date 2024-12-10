using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _player; // Refer�ncia ao Transform do jogador
    [SerializeField] private Vector3 _offset; // Deslocamento inicial da c�mera em rela��o ao jogador
    [SerializeField] private float _smoothSpeed = 0.125f; // Velocidade de suaviza��o do movimento da c�mera

    [SerializeField] private Vector2 _xBounds; // Limites para o eixo X da c�mera
    [SerializeField] private Vector2 _zBounds; // Limites para o eixo Z da c�mera

    private void Start()
    {
        if (_player != null)
        {
            // Calcula o deslocamento inicial com base na posi��o inicial do jogador e da c�mera
            _offset = transform.position - _player.position;
        }
        else
        {
            Debug.LogWarning("O jogador n�o foi atribu�do ao script CameraFollow.");
        }
    }

    private void LateUpdate()
    {
        if (_player != null)
        {
            // Calcula a posi��o desejada da c�mera com base na posi��o do jogador e no deslocamento
            Vector3 desiredPosition = _player.position + _offset;

            // Restringe a posi��o desejada da c�mera aos limites especificados
            float clampedX = Mathf.Clamp(desiredPosition.x, _xBounds.x, _xBounds.y);
            float clampedZ = Mathf.Clamp(desiredPosition.z, _zBounds.x, _zBounds.y);

            // Mant�m a altura (Y) sem clamping
            Vector3 clampedPosition = new Vector3(clampedX, desiredPosition.y, clampedZ);

            // Suaviza o movimento da c�mera para a posi��o desejada
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, clampedPosition, _smoothSpeed);

            // Atualiza a posi��o da c�mera
            transform.position = smoothedPosition;
        }
    }
}
