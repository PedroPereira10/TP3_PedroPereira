using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private GameObject _victoryScreen;

    void Start()
    {
        Time.timeScale = 1; 
    }

    public void GameOver()
    {
        _gameOverScreen.SetActive(true);
        Time.timeScale = 0;
        AudioManager.Instance.PauseMusic(); 
        AudioManager.Instance.PlayLoseSound(); 
    }

    public void Victory()
    {
        _victoryScreen.SetActive(true);
        Time.timeScale = 0;
        AudioManager.Instance.PauseMusic(); 
        AudioManager.Instance.PlayWinSound(); 
    }

    public void PlayAgain()
    {
        Time.timeScale = 1; 
        AudioManager.Instance.ResumeMusic(); 
        UnityEngine.SceneManagement.SceneManager.LoadScene("Diablo"); 
    }
}
