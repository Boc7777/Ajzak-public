using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuppysHead : MonoBehaviour
{
    private SpaceBar _spaceBar;

    void Start()
    {
        _spaceBar = GameObject.Find("Player").GetComponent<SpaceBar>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("HitBox"))
        {
            _spaceBar.SetSpaceBar(1, gameObject.GetComponent<SpriteRenderer>().sprite);
            GameObject.Find("ItemManager").GetComponent<TransformPlayer>().GuppsItems += 1;
        }

    }
}
