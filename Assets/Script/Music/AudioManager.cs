using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource1;

    public AudioSource audioSource2;
    public AudioSource audioSource3;
    public AudioSource audioSource4;
    public AudioClip music1;

    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        audioSource1.clip = music1;
        audioSource1.Play();

        audioSource2.clip = sfx1;
        audioSource3.clip = sfx2;
        audioSource4.clip = sfx3;
    }

    public void MuteOn(){ audioSource1.Stop(); }

    public void MuteOff(){ audioSource1.Play(); }

    public void SFX1(){ audioSource2.Play(); }

    public void SFX2(){ audioSource3.Play(); }

    public void SFX3(){ audioSource4.Play(); }
}