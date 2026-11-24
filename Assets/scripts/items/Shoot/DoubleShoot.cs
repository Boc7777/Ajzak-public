using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleShoot : MonoBehaviour
{
    private PlayerItems _playerItems;
    private PlayerShooting _playerShooting;

    void Start()
    {
        _playerItems = GameObject.Find("Player").GetComponent<PlayerItems>();
        _playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _playerItems.DoubleShoot = true;
            _playerShooting.MoveValue = 0.2f;

        }
    }
}
