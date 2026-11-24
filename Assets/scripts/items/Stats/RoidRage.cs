using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoidRage : MonoBehaviour
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

           _playerStats.MovementSpeed += 0.6f;
            _playerStats.MovementSpeedChange = "+ 0.6";

            _playerStats.Range += 5.25f;
            _playerStats.RangeChange = "+ 5.25";

            _playerStats.NewStats();

            GameObject.Find("ItemManager").GetComponent<TransformPlayer>().SpunItem += 1;

        }


    }
}
