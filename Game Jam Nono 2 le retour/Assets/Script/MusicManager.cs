using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public AudioClip musicIntro;
    public AudioClip musicPlayer;

    public AudioSource AudioSource;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
        AudioSource.loop = false;
    }

    public IEnumerator PlayIntro()
    {
        AudioSource.clip = musicIntro;
        AudioSource.Play();
        yield return new WaitForSeconds(musicIntro.length);
        AudioSource.clip = musicPlayer;
        AudioSource.loop = true;
        AudioSource.Play();
    }
}
