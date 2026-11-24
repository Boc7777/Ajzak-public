using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piercing : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _itemsPlayer.PiercingActive = true;  
        }
    }
}
