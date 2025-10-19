using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    public AudioClip[] audioClips;//contains the audio clips that we use for music. is persistant across scenes 
    public AudioSource source;

    void Awake()
    {
        gameObject.GetComponent<AudioSource>();
        source.Play();
    }
    public void setMusicClip(int clipIndex)
    {
        source.clip = audioClips[clipIndex];
    }
    public void setMusicVolume(float vol){
        source.volume = vol;
    }
}
