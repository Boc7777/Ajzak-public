using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawOfTheVoidItem : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    [SerializeField] private GameObject _mawOfTheVoid;
    private GameObject _player;
    
    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _player = GameObject.Find("Player");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _itemsPlayer.MawOfTheVoidActive = true;
        }

    }
}
