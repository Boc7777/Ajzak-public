using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngelicPrismPiedestal : MonoBehaviour
{

    private PlayerItems _itemsPlayer;
    [SerializeField] private GameObject _angelicPrismInst;
    private Transform _familiarParent;

    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _familiarParent = GameObject.Find("FamiliarPlayer").transform;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _itemsPlayer.MultidimensionalBaby = true;
            Instantiate(_angelicPrismInst, transform.position, Quaternion.identity, _familiarParent);

        }
    }
}