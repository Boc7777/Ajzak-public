using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikes : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    void Start()
    {
        _playerHealth = GameObject.Find("Player/HitBox").GetComponent<PlayerHealth>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.CompareTag("HitBox"))
        {
            _playerHealth.TakeDamage(1);
           
        }   
    }
}
