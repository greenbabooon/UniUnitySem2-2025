using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }


    [Header("Audio Source")] //what is used to play the audio
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("Audio Clip")] //actual audio clips, add more clips here to assign them and therefore be able to play them
    public AudioClip background;
    public AudioClip death;
    public AudioClip pickup;

    // for now I have assigned audio using game objects in Unity, but in the future we should try and assign sounds with scripts and the audio manager
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() //plays the 'background' clip on start
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip) //used to play SFX, call method in other scripts with the 'Audio' tag assigned onto objects
    {
        SFXSource.PlayOneShot(clip);
    }

    /* calling the tag in other scripts
     * private void Start()
    {
        audioManager = gameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    } */

    //https://youtu.be/N8whM1GjH4w?si=EiPQk1tN_VvUUmv3 I used this video when making this script and AudioManager 
}
