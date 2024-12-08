using UnityEngine;

public class Crystal : MonoBehaviour
{
    public bool IsCollected = false;  

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null && !IsCollected)
        {
            
            IsCollected = true;
            player.RemoveCrystal();  
            gameObject.SetActive(false);  
            Debug.Log("Cristal collecté !");
        }
    }
}
