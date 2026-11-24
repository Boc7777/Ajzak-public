using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathsTouch : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    [SerializeField] private GameObject _deathsTouch;

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
            _itemsPlayer.PiercingActive = true;
            _itemsPlayer.DeathsTouchActive = true;

             

            _playerStats.Dmg += 1.5f;
            _playerStats.DmgChange = "+ 1.5";

            _playerStats.Tears -= 0.3f;
            _playerStats.TearsChange = "- 0.3";

            _playerStats.NewStats();
        }
    }
}
