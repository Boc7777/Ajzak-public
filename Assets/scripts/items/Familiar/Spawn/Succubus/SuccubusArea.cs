using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusArea : MonoBehaviour
{
    private PlayerStats _playerStats;
    private float _oldDmg;
    private float _changeDmg;

    void Start()
    {
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            _oldDmg = _playerStats.Dmg;
            _playerStats.Dmg *= 1.5f;
            _changeDmg = _playerStats.Dmg-_oldDmg;
            _playerStats.DmgChange = "+ "+(_changeDmg).ToString();

            _playerStats.NewStats();

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            _playerStats.DmgChange ="- " +(_changeDmg).ToString();
            _playerStats.Dmg = _oldDmg;

            _playerStats.NewStats();

        }
    }
}

