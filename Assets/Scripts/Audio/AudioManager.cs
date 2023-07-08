using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager> 
{
    public AudioSource audioSource => GetComponent<AudioSource>();

    public void PlayRandomClip(List<AudioClip> clips)
    {
        AudioClip clip = clips[UnityEngine.Random.Range(0, clips.Count)];
        Debug.Log($"Playing clip {clip.name}");
        audioSource.PlayOneShot(clip, 1.0f);
    }
}
