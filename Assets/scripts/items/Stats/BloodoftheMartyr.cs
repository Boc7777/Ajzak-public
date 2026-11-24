using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodoftheMartyr : MonoBehaviour
{
    private PlayerStats _playerStats;
    private PlayerItems _itemsPlayer;

    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _itemsPlayer.BloodMartyrActive = true;

            _playerStats.Dmg += 1;
            _playerStats.DmgChange = "+ 1";

           
            _playerStats.NewStats();

        }


    }
}