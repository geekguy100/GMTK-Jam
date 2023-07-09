using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager> 
{
    [Header("Audio Sources")]
    [SerializeField] private AudioSource objectAudioSource;
    [SerializeField] private AudioSource ambientAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    void Start()
    {
        // Play ambient audio.
        if(ambientAudioSource != null)
            ambientAudioSource.Play();
    }

    public void PlayRandomObjectClip(List<AudioClip> clips)
    {
        AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Count)];
        Debug.Log($"Playing clip {clip.name}");
        objectAudioSource.PlayOneShot(clip, 1.0f);
    }

    public void PlayMusic()
    {
        musicAudioSource.Play();
    }
}
