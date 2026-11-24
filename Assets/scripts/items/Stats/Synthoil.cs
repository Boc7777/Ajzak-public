using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Synthoil : MonoBehaviour
{
    private PlayerStats _playerStats;

    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _playerStats.Dmg += 1f;
            _playerStats.DmgChange = "+ 1";


            _playerStats.Range += 5.25f;
            _playerStats.RangeChange = "+ 5.25";

            _playerStats.NewStats();

            GameObject.Find("ItemManager").GetComponent<TransformPlayer>().SpunItem += 1;

        }


    }
}
