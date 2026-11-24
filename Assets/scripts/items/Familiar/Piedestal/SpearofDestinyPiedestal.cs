using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearofDestinyPiedestal : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    [SerializeField] private GameObject _spearInst;
    private Transform _familiarParent;

    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _familiarParent = GameObject.Find("Familiar").transform;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _itemsPlayer.SpearofDestiny = true;
            Instantiate(_spearInst, transform.position, Quaternion.identity, _familiarParent);

        }
    }
}