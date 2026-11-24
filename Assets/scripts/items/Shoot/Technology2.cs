using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Technology2 : MonoBehaviour
{
    private PlayerItems _playerItem;

    void Start()
    {
        _playerItem = GameObject.Find("Player").GetComponent<PlayerItems>();


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _playerItem.Technology2 = true;

        }

    }
}