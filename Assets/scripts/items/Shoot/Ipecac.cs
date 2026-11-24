using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ipecac : MonoBehaviour
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

            _itemsPlayer.IpecacActive = true;
            _playerStats.Dmg += 20;
            _playerStats.DmgChange = "+ 20";

            _playerStats.Tears += 1f;
            _playerStats.TearsChange = "+ 1.0";
            _playerStats.NewStats();

        }



    }
}