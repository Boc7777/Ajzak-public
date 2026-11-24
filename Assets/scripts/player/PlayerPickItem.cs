using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickItem : MonoBehaviour
{
    private ItemManager _itemList;
    private string _name;
    private void Start()
    {
        _itemList = GameObject.Find("ItemManager").GetComponent<ItemManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isItem = collision.CompareTag("Item");

        if (!isItem) 
            return; 

            _name = collision.gameObject.name;
            _itemList.RemoveGoldenChestItem(_name);
            _itemList.RemoveTreasureRoomItem(_name);
            _itemList.RemoveDevilRoomItem(_name);
            _itemList.RemoveAngelRoomItem(_name);
        Destroy(collision.gameObject);
        
    }
}
