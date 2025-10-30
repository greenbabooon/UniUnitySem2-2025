using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMusic : MonoBehaviour
{
    public static MenuMusic Instance;

    public AudioClip[] audioClips;//contains the audio clips that we use for music. is persistant across scenes 
    public AudioSource source;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (source == null)
            source = GetComponent<AudioSource>();

        if (!source.isPlaying) 
            source.Play();

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ChangeMusicForScene(scene.name);
    }

    void ChangeMusicForScene(string sceneName)
    {
        switch (sceneName)
        {
            case "Main Menu":
                setMusicClip(0);
                break;

            case "Level-1-Prom-Hall":
                setMusicClip(1);
                break;

            case "Main Level":
                setMusicClip(2);
                break;

            default:
                setMusicClip(0);
                    break;
        }
     }

    public void setMusicClip(int clipIndex)
    {
        if (clipIndex < 0 || clipIndex >= audioClips.Length)
            return;

        if (source.clip !=audioClips[clipIndex])
        {
            source.clip = audioClips[clipIndex];
            source.Play();
        }
    }


    public void setMusicVolume(float vol){
        source.volume = vol;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
