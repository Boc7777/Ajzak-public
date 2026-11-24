using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrFetus : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    [SerializeField]private GameObject _bombTears;
    private TearsCreator _tearsCreator;
    private PlayerStats _playerStats;
    private float _oldTears;

    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _tearsCreator = GameObject.Find("Player").GetComponent<TearsCreator>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _tearsCreator.CurrentlyTears = _bombTears;
            _itemsPlayer.DrFetusActive = true;

            _oldTears = _playerStats.Tears;
            _playerStats.Tears *= 2.5f;
            _playerStats.TearsChange ="+ "+(_playerStats.Tears - _oldTears).ToString() ;
            _playerStats.NewStats();

        }



    }
}
