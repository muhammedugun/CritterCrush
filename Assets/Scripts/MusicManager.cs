using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Men� ve seviyeler i�in kullan�lan ses kaynaklar�n� tan�mlar.
/// </summary>
public class MusicManager : MonoBehaviour
{
    public AudioSource menuAudioSource, inLevelAudioSource;

    private static MusicManager instance;

    /// <summary>
    /// Singleton deseni kullanarak MusicManager �rne�ini al�r.
    /// </summary>
    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();

                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(MusicManager).Name);
                    instance = singletonObject.AddComponent<MusicManager>();
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("SoundVolume", 1f);

        if (CheckMute())
        {
            MuteMusic();
        }
        else
        {
            UnMuteMusic();
        }
    }

    /// <summary>
    /// M�zik sesinin kapal� olup olmad���n� kontrol eder.
    /// </summary>
    /// <returns>M�zik kapal�ysa true, a��ksa false d�ner.</returns>
    public static bool CheckMute()
    {
        return PlayerPrefs.GetFloat("MusicVolume", 1f) == 0f;
    }

    /// <summary>
    /// M�zikleri kapal� duruma getirir.
    /// </summary>
    public void MuteMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", 0f);
        menuAudioSource.volume = 0f;
        inLevelAudioSource.volume = 0f;
    }

    /// <summary>
    /// M�zikleri a��k duruma getirir.
    /// </summary>
    public void UnMuteMusic()
    {
        PlayerPrefs.SetFloat("MusicVolume", 1f);
        menuAudioSource.volume = 1f;
        inLevelAudioSource.volume = 1f;
    }

    private void OnLevelWasLoaded(int level)
    {
        string name = SceneManager.GetSceneByBuildIndex(level).name;
        if (name == "Main" || name == "LevelSelection")
        {
            inLevelAudioSource.Stop();
            if (!menuAudioSource.isPlaying)
                menuAudioSource.Play();
        }
        else
        {
            menuAudioSource.Stop();
            if (!inLevelAudioSource.isPlaying)
                inLevelAudioSource.Play();
        }
    }
}
