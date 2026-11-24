using UnityEngine.Audio;
using System;
using UnityEngine;

public class audioManager : MonoBehaviour
{
    public Sound[] sounds;


    void Awake()
    {
        foreach(Sound s in sounds)
        {
           s.source = gameObject.AddComponent<AudioSource>();
           s.source.clip = s.clip;

           s.source.volume = s.volume;
           s.source.pitch = s.pitch;
        }
    }

    public void PlaySound(string name)
    {
        foreach(Sound dzwiek in sounds)
        {
            if (dzwiek.name == name)
            {
                dzwiek.source.Play();
            }
            else
            {
                Debug.Log("nie znaleziono dzwieku");
            }
        }
    }
}
