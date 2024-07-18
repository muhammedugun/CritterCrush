using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicManager : MonoBehaviour
{
    public AudioSource menuAudioSource, inLevelAudioSource;

    private static MusicManager instance;
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

    private void OnLevelWasLoaded(int level)
    {
        string name = SceneManager.GetSceneByBuildIndex(level).name;
        if (name == "Main" || name == "LevelSelection")
        {
            inLevelAudioSource.Stop();
            if(!menuAudioSource.isPlaying)
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