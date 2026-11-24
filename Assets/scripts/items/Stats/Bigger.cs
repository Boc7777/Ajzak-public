using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bigger : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    private PlayerStats _playerStats;

    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
           
            _itemsPlayer.BigActive = true;
            _playerStats.Dmg += 4;
            _playerStats.DmgChange = "+ 4";

            _playerStats.Tears += 0.3f; 
            _playerStats.TearsChange = "+ 0.3";
            _playerStats.NewStats();
           
        }

        

    }
}
