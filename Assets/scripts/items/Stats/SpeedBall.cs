using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBall : MonoBehaviour
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
           

            _playerStats.ShootSpeed += 0.2f;
            _playerStats.ShootSpeedChange = "+ 0.2";

            _playerStats.MovementSpeed += 0.3f;
            _playerStats.MovementSpeedChange = "+ 0.3";

            

            _playerStats.NewStats();

            GameObject.Find("ItemManager").GetComponent<TransformPlayer>().SpunItem += 1;

        }


    }
}
