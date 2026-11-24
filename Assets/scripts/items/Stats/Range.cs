using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Range : MonoBehaviour
{
    private PlayerStats _playerStats;

    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("HitBox"))
        {
            _playerStats.Range+=1f;
            _playerStats.RangeChange = "+ 1";
            _playerStats.NewStats();

        }

    }
}


