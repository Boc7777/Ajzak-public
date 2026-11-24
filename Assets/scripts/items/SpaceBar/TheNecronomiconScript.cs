using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheNecronomiconScript : MonoBehaviour
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
            _spaceBar.SetSpaceBar(4, gameObject.GetComponent<SpriteRenderer>().sprite);
        }



    }
}