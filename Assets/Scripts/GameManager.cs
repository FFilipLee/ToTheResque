using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public AudioSource backgroundMusicSource;
    public AudioClip backgroundMusic; // Assign this in the Inspector or through code

    void Awake()
    {
        // Singleton pattern to ensure only one GameManager exists
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // Keep GameManager alive between scenes
    }

    void Start()
    {
        // Ensure backgroundMusic is assigned through the Inspector or programmatically
        if (backgroundMusic != null)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogError("Background music clip is not assigned!");
        }
    }
}

