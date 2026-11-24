using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proptosis : MonoBehaviour
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
            _itemsPlayer.ProptosisActive = true;
            _playerStats.Dmg += 2;
            _playerStats.DmgChange = "+ 2";

            _playerStats.NewStats();

        }

    }
}
