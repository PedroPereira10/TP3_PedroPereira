using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioClip _backgroundMusic;
    [SerializeField] private AudioClip _itemCollectSound;
    [SerializeField] private AudioClip _loseSound;
    [SerializeField] private AudioClip _winSound;
    [SerializeField] private AudioClip _deposit;

    private AudioSource _musicSource;
    private AudioSource _sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        _musicSource = gameObject.AddComponent<AudioSource>();
        _sfxSource = gameObject.AddComponent<AudioSource>();

        _musicSource.loop = true;
        _musicSource.clip = _backgroundMusic;
        _musicSource.Play();
    }

    public void PlayItemCollectSound()
    {
        _sfxSource.PlayOneShot(_itemCollectSound);
    }

    public void ItemDeposited()
    {
        _sfxSource.PlayOneShot(_deposit);
    }

    public void PlayLoseSound()
    {
        _sfxSource.PlayOneShot(_loseSound);
    }

    public void PlayWinSound()
    {
        _sfxSource.PlayOneShot(_winSound);
    }

    public void PauseMusic()
    {
        if (_musicSource.isPlaying)
        {
            _musicSource.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (!_musicSource.isPlaying)
        {
            _musicSource.Play();
        }
    }
}
