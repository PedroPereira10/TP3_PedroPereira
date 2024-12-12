using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private int _requiredCrystals = 3;  
    [SerializeField] private GameObject _door;          
    private int _currentCrystalCount = 0;                
    [SerializeField] private Player _player;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision d�tect�e avec : " + collision.collider.name);

        if (collision.collider.GetComponent<Player>())
        {
            Debug.Log("Le joueur est sur le socle");

            if (_player.HasCrystalToDeposit())  
            {
                DepositCrystal();
            }
        }
    }

    private void DepositCrystal()
    {
        _currentCrystalCount++;

        _player.RemoveCrystal();
        Debug.Log("Cristal d�pos�, " + _currentCrystalCount + " sur " + _requiredCrystals);
        AudioManager.Instance.ItemDeposited();

        if (_currentCrystalCount >= _requiredCrystals)
        {
            OpenDoor();
        }
    }

    private void OpenDoor()
    {
        if (_door != null)
        {
            Destroy(_door); 
            Debug.Log("Porte ouverte, tous les cristaux sont plac�s!");
        }
        else
        {
            Debug.LogWarning("Aucune porte assign�e au socle.");
        }
    }
}
