using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccubusPiedestal : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    [SerializeField] private GameObject _succubus;
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
            _itemsPlayer.SuccubusActive = true;
            Instantiate(_succubus, transform.position, Quaternion.identity, _familiarParent);

        }
    }
}