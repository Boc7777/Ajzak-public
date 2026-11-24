using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomItemSpawner : MonoBehaviour
{
    private prefabsContainer _prefabsContainer;

    public bool Spawn = false;

    private GameObject _items;

    private void Start()
    {
        _prefabsContainer = GameObject.FindGameObjectWithTag("container").GetComponent<prefabsContainer>();
        _items = GameObject.FindGameObjectWithTag("ItemsInRoom");
    }

    private void Update()
    {
        if (Spawn)
        {
             int _rand = Random.Range(0, _prefabsContainer.Items.Count);
            Instantiate(_prefabsContainer.Items[_rand], gameObject.transform.position, Quaternion.identity, _items.transform);
            Spawn = false; 
        }
    }

}
