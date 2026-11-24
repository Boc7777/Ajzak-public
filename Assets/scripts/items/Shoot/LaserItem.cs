using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserItem : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    private PlayerShooting _shooting;

    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _shooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _itemsPlayer.LaserActive = true;
           
        }



    }
}
