using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audiocontroller : MonoBehaviour
{
    public static Audiocontroller instance;
    AudioSource audiosource;
    public AudioClip spirit;
    public AudioClip Katanasound;
    public AudioClip swordsound;
    public AudioClip flight;
    public AudioClip menu;
    public AudioClip charge;
    public AudioClip phase;
    public AudioClip ninja;
    public AudioClip windsound;
    public AudioClip throwing;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void playkatanaswing()
    {
        audiosource.PlayOneShot(Katanasound);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);

    }
    public void playspirit()
    {
        audiosource.PlayOneShot(spirit);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }
    public void playspiritcharger()
    {
        audiosource.PlayOneShot(spirit);
        GetComponent<AudioSource>().pitch = Random.Range(0.25f, 0.40f);
    }
    public void playswordswing()
    {
        audiosource.PlayOneShot(swordsound);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }
    public void playflight()
    {
        audiosource.PlayOneShot(flight);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }

    public void playmenu()
    {
        audiosource.PlayOneShot(menu);
        GetComponent<AudioSource>().pitch = Random.Range(0.96f, 1f);
    }

    public void playcharge()
    {
        audiosource.PlayOneShot(charge);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }

    public void playshoot()
    {
        audiosource.PlayOneShot(phase);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }
    public void playphase()
    {
        audiosource.PlayOneShot(phase);
        GetComponent<AudioSource>().pitch = Random.Range(0.20f, 0.40f);
    }

    public void playninja()
    {
        audiosource.PlayOneShot(ninja);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }

    public void playwind()
    {
        audiosource.PlayOneShot(windsound);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }
    public void playthrow()
    {
        audiosource.PlayOneShot(throwing);
        GetComponent<AudioSource>().pitch = Random.Range(0.85f, 1f);
    }
    public void playfallsword()
    {
        audiosource.PlayOneShot(Katanasound);
        GetComponent<AudioSource>().pitch = Random.Range(1.30f, 1.50f);
    }
}
