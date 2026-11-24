using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombKaboom : MonoBehaviour
{
    private float _delay;
    private bool _exited;
    private GameObject _player;
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        _delay = 2;
        if (gameObject.name == "DrFetusBombParent(Clone)")
        {
            _delay = 1;
        }
        StartCoroutine(StartExplosion(_delay));
        
    }

    private IEnumerator StartExplosion(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        StartCoroutine(EndExplosion(0.82f));
    }

    private IEnumerator EndExplosion(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
        Destroy(gameObject);
    }

   
    private void OnTriggerExit2D(Collider2D collision)
    {
        
        Physics2D.IgnoreCollision(_player.GetComponent<Collider2D>(), GetComponent<Collider2D>(),false);
    }
}
