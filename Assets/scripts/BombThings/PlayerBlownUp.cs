using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlownUp : MonoBehaviour
{
    private PlayerHealth _playerHealth;

    private int _damage =2;

    private void Start()
    {
        _playerHealth = GameObject.Find("Player/HitBox").GetComponent<PlayerHealth>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            _playerHealth.TakeDamage(_damage);
        }
    }
}
