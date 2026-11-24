using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheHalo : MonoBehaviour
{
    private PlayerStats _playerStats;
    private PlayerHealth _playerHealth;

    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
        _playerHealth = GameObject.Find("Player").transform.Find("HitBox").GetComponent<PlayerHealth>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _playerHealth.HPUP=true;

            _playerStats.Dmg += 0.3f;
            _playerStats.DmgChange = "+ 0.3";

            _playerStats.Tears -= 0.2f;
            _playerStats.TearsChange = "- 0.2   ";

            _playerStats.MovementSpeed += 0.3f;
            _playerStats.MovementSpeedChange = "+ 0.3";

            _playerStats.Range += 0.25f;
            _playerStats.RangeChange = "+ 0.25";

            _playerStats.NewStats();

        }


    }
}