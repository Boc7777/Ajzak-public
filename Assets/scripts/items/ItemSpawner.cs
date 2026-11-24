using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    private ItemManager _templates;
   
    private int _rand;
    private GameObject _item;
    void Start()
    {
        _templates = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }

    
    void Update()
    {
        

            _rand = 0;
            _rand = Random.Range(0, _templates.TreasureRoomItems.Count);

            _item = _templates.TreasureRoomItems[_rand];

            Instantiate(_item, gameObject.transform.position, Quaternion.identity, gameObject.transform.parent)  ;

           
            Destroy(gameObject);
        
    }
}
