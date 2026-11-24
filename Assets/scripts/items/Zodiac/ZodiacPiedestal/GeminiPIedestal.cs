using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeminiPIedestal : MonoBehaviour
{
    private PlayerItems _itemsPlayer;
    [SerializeField] private GameObject _gemini;
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
            Debug.Log("fdf");
            Instantiate(_gemini, transform.position, Quaternion.identity, _familiarParent);

        }
    }
}
