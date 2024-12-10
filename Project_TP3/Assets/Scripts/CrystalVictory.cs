using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrystalVictory : MonoBehaviour
{
    [SerializeField] AudioClip _victorySound;
    [SerializeField] private GameManager _gameManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            Debug.Log("Cristal victoire collecté !");
            Destroy(gameObject);

            if (_victorySound != null)
            {
                AudioSource.PlayClipAtPoint(_victorySound, transform.position);
            }

            if (_gameManager != null)
            {
                _gameManager.Victory();
            }
        }
    }
}
