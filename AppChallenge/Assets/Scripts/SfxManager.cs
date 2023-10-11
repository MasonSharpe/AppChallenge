using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SfxManager : MonoBehaviour {


    public static SfxManager instance;
    public AudioClip[] clips;


    private void Awake() {
        instance = this;
    }

    public void PlaySoundEffect(int clipIndex, float volume = 1, float pitch = 1, float pan = 0, int priority = 128) {
        AudioSource audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.panStereo = pan;
        audioSource.priority = priority;
        audioSource.PlayOneShot(clips[clipIndex]);
        Destroy(audioSource, clips[clipIndex].length);
    }
}
