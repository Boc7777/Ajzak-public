using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerMenager : MonoBehaviour
{
    private cetral _cetral;
    private RoomItemSpawner _child;
    private bool _spawned = false;

    private void Start()
    {
        _cetral = gameObject.transform.parent.parent.Find("Spawnery/central").GetComponent<cetral>();
    }
    private void Update()
    {
        if (_cetral.RandomItemSpawn && !_spawned)
        {
            int _rand = Random.Range(0, gameObject.transform.childCount);
            _child = gameObject.transform.GetChild(_rand).gameObject.transform.GetComponent<RoomItemSpawner>(); 
            _child.Spawn = true;
            _spawned = true;
        }
    }
}
