using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCtrl : MonoBehaviour
{
    public static AudioCtrl Instance;
    public AudioClip[] soundtrack;
    public Camera MainCamera;
    AudioSource As;

    float DistanceBefore=0;
    float Sowing=0.03f;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        
        As = GetComponent<AudioSource>();
        As.clip = soundtrack[3];
        As.volume = 0.3f;
        As.loop = false;
        As.Play();
    }

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if(As.isPlaying==false)
        {
            As.volume = 0.15f;
            As.clip = soundtrack[2];
            As.Play();
        }
    }

    public void PlayerWalkSoundPlay(AudioSource Asource,float Distance)
    {
        if (DistanceBefore>Distance)
        {
            Asource.pitch -= Sowing;
            DistanceBefore = Distance;
            if(Asource.pitch<0.25f)
            {
                Asource.pitch = 0.25f;
            }
        }else if(DistanceBefore < Distance)
        {
            Asource.pitch += Sowing;
            DistanceBefore = Distance;
            if (Asource.pitch > 1f)
            {
                Asource.pitch = 1f;
            }
        }

        if (!Asource.isPlaying)
        {
            Asource.Play();
        }
    }

 

    public void PlayerWalkSoundStop(AudioSource Asource)
    {
        Asource.Stop();
        Asource.pitch = 0.2f;
    }

    public void BombFlySound(AudioSource Asource)
    {
        /*
         Asource.volume += 0.01f;
        if (!Asource.isPlaying)
        {
            Asource.Play();
        } 
         */

    }

    public void PlayFailMusic()
    {
        AudioSource Asource=MainCamera.GetComponent<AudioSource>();
        Asource.Play();
    }

    public void BombExplose(Vector3 Bomb_position)
    {
        AudioSource.PlayClipAtPoint(soundtrack[6], Bomb_position, 0.6f);
    }

    public void PanelDamagedSoundPlay(Vector3 Panel_position)
    {
        AudioSource.PlayClipAtPoint(soundtrack[1],Panel_position, 0.4f);
    }

    public void MuteAllSoundEffect()
    {
        AudioSource[] sources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        for (int index = 0; index < sources.Length; ++index)
        {
            sources[index].mute = true;
        }
    }
    
}
