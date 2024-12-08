using UnityEngine;

public class Pedestal : MonoBehaviour
{
    [SerializeField] private int _requiredCrystals = 3;  
    [SerializeField] private GameObject _door;          
    private int _currentCrystalCount = 0;                
    [SerializeField] private Player _player;             

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.GetComponent<Player>())
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
