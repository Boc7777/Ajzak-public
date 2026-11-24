using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopRoomItemSpawner : MonoBehaviour
{
    [SerializeField] private GameObject dolars1;
    [SerializeField] private GameObject dolars2;
    [SerializeField] private GameObject dolars3;
    [SerializeField] private GameObject dolars5;
    [SerializeField] private GameObject dolars7;
    [SerializeField] private GameObject dolars15;

    private prefabsContainer _prefabsContainer;
    private ItemManager _itemManager;
    private Pickups _pickups;
 
    private GameObject _items;

    private GameObject _item;
    private GameObject _cena;

    private int _Price;

    private Vector3 _position;

    private void Start()
    {
        _prefabsContainer = GameObject.FindGameObjectWithTag("container").GetComponent<prefabsContainer>();
        _itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        _items = GameObject.FindGameObjectWithTag("ItemsInRoom");
        _pickups = GameObject.FindGameObjectWithTag("HitBox").GetComponent<Pickups>();

        int _randGeneral = Random.Range(0, 100);

        if (_randGeneral <= 15)
        {
            int _rand1 = Random.Range(0, _itemManager.TreasureRoomItems.Count);
            _item = Instantiate(_itemManager.TreasureRoomItems[_rand1], gameObject.transform.position, Quaternion.identity, _items.transform);
        
            _position = _item.transform.position;
            _cena = Instantiate(dolars15, new Vector2(_item.transform.position.x, _item.transform.position.y - 0.8f), Quaternion.identity, transform);
            _Price = 15;
        }

        else
        {
            int _rand2 = Random.Range(0, _prefabsContainer.ShopItems.Count);
            _item = Instantiate(_prefabsContainer.ShopItems[_rand2], gameObject.transform.position, Quaternion.identity, _items.transform);

            _position = _item.transform.position;
            if (_item.name.Contains("key"))
            {
                _cena = Instantiate(dolars5, new Vector2(_item.transform.position.x, _item.transform.position.y - 0.8f), Quaternion.identity, transform);
                _Price = 5;
            }
            else if (_item.name.Contains("FullRedHeart"))
            {
                _cena = Instantiate(dolars3, new Vector2(_item.transform.position.x, _item.transform.position.y - 0.8f), Quaternion.identity, transform);
                _Price = 3;
            }
            else if (_item.name.Contains("FullSoulHeart"))
            {
                _cena = Instantiate(dolars5, new Vector2(_item.transform.position.x, _item.transform.position.y - 0.8f), Quaternion.identity, transform);
                _Price = 5;
            }
            else if (_item.name.Contains("battery"))
            {
                _cena = Instantiate(dolars5, new Vector2(_item.transform.position.x, _item.transform.position.y - 0.8f), Quaternion.identity, transform);
                _Price = 5;
            }
            else if (_item.name.Contains("bomb"))
            {
                _cena = Instantiate(dolars5, new Vector2(_item.transform.position.x, _item.transform.position.y - 0.8f), Quaternion.identity, transform);
                _Price = 5;
            }
        }

    }
    private void Update()
    {

        if (_item == null)
        {
            _pickups.CoinsAmount -= _Price;
            Destroy(_cena);
            Destroy(gameObject);
        }

        else
        {
            if (_pickups.CoinsAmount >= _Price)
            {
                _item.GetComponent<Collider2D>().enabled = true;
            }
            else
            {                
                _item.GetComponent<Collider2D>().enabled = false;
            }
            _item.transform.position = _position;
        }
        
    }
}
