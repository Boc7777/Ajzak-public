using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkBumColletHeart : MonoBehaviour
{
    [SerializeField]private List<GameObject> _spawnStuff;
    private int _blackHeart = 60;
    private int _friendlyFly =20;
    private int _fly =20;
    private float _ateHeart;

    private bool _dropItem;

    private Transform _pokeDex;
    private Transform _itemsInRoom;
    private Transform _monsterParent;

    void Start()
    {
        _pokeDex = GameObject.Find("PokeDex").transform;
        _itemsInRoom = GameObject.Find("ItemsInRoom").transform;
       
    }

    private void Update()
    {
        if (_dropItem && transform.parent.GetComponent<DarkBumMove>().Stay)
        {
            _dropItem= false;
            StartCoroutine(DropStuff());
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.name.Contains("FullRedHeart"))
        {
            Destroy(collision.gameObject);
            _ateHeart += 1;
            if (_ateHeart >= 1.5f)
            {
                _dropItem = true;
            }
        }

        if (collision.transform.name.Contains("HalfRedHeart"))
        {
            Destroy(collision.gameObject);
            _ateHeart += 0.5f;
            if (_ateHeart >= 1.5f)
            {
                _dropItem = true;
            }

        }
    }
    private IEnumerator DropStuff()
    {
       DarkBumStuff();
       yield return new WaitForSeconds(1f);
        _ateHeart -= 1.5f;
        if(_ateHeart >= 1.5f)
        {
            _dropItem = true;
        }
    }
    private void DarkBumStuff()
    {
        _ateHeart -= 1.5f;

        int randomPercent = Random.Range(1, 101);

 

        if (randomPercent < _blackHeart)
        {
            float randomX = Random.Range(10, 200) / 100;
            float randomY = Random.Range(10, 200) / 100;
            Instantiate(_spawnStuff[0], transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity, _itemsInRoom);
        }
        else if (randomPercent < _blackHeart + _friendlyFly)
        {
            Instantiate(_spawnStuff[1], transform.position, Quaternion.identity, _pokeDex);

        }
        else if (randomPercent < _blackHeart + _friendlyFly + _fly)
        {
            _monsterParent = GameObject.Find("Player").GetComponent<player_movement>().CurrentlyCentral.transform.parent.Find("Monstery");
            Instantiate(_spawnStuff[2], transform.position, Quaternion.identity, _monsterParent);

        }


    }
}
