using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MomsKnifeItem : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    [SerializeField] private GameObject _prefab;
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
            Debug.Log("spawn");
            _itemsPlayer.MomsKnife = true;
            Instantiate(_prefab, _player.transform.position, Quaternion.Euler(0, 0, 0));
        }



    }
}
