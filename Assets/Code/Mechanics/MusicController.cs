using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    AudioSource audiosource;
    public AudioClip BossMusic;
    public AudioClip WorldMusic;
    public AudioClip TowerMusic;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.loop = true;
    }

    public void playworldtheme()
    {
        audiosource.Stop();
        audiosource.PlayOneShot(WorldMusic);
        audiosource.loop = true;
    }
    public void playbosstheme()
    {
        audiosource.Stop();
        audiosource.PlayOneShot(BossMusic);
        GetComponent<AudioSource>().pitch = 1f;
        audiosource.loop = true;
    }
    public void playtowertheme()
    {
        audiosource.Stop();
        audiosource.PlayOneShot(TowerMusic);
        audiosource.loop = true;
        GetComponent<AudioSource>().pitch = 0.85f;
    }
}
