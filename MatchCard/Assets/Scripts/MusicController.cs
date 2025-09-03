using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField]private AudioSource audioSource;
    private GameConfig config;
    public void InitMusic(GameConfig cfg)
    {
        config = cfg;     
        audioSource.playOnAwake = false;
    }
    public void PlayFlip() { PlaySound(config?.soundFlip); }
    public void PlayMatch() { PlaySound(config?.soundMatch); }
    public void PlayMismatch() { PlaySound(config?.soundMismatch); }
    public void PlayGameOver() { PlaySound(config?.soundGameOver); }
    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}
