using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallRockSC : MonoBehaviour
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
            _playerStats.Dmg += 1.5f;
            _playerStats.DmgChange = "+ 1.5";

            _playerStats.Tears -= 0.1f;
            _playerStats.TearsChange = "- 0.1";

            _playerStats.MovementSpeed -= 0.2f;
            _playerStats.MovementSpeedChange = "- 0,2";

            _playerStats.NewStats();

        }
    }

}
