using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSounds : MonoBehaviour
{
    public string Smierc;
    public string Damage;
    public string Shot;

    private audioManager _audioManager;

    void Start()
    {
        _audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<audioManager>();
    }
    public void DeathSound()
    {

        _audioManager.PlaySound(Smierc);
    }

    public void DamageSound()
    {
        _audioManager.PlaySound(Damage);
    }

    public void ShotSound()
    {
        _audioManager.PlaySound(Shot);
    }

    
}
