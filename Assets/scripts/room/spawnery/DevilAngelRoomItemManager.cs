using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevilAngelRoomItemManager : MonoBehaviour
{

    public GameObject RedHeart;
    public GameObject TwoRedHearts;
    public GameObject ThreeSoulHearts;

    private GameObject _children;

    private ItemManager _itemsManager;
    private PlayerHealth _playerHealth;
    private GameObject _items;

    private List<GameObject> ItemyWPuli = new List<GameObject>();

    private GameObject _item;
    private GameObject _cena;

    //false == devil 
    public bool DevilAngelRoom = false;
    
    private int _price;
    void Start()
    {
        
        _itemsManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        _items = GameObject.FindGameObjectWithTag("ItemsInRoom");
        _playerHealth = GameObject.FindGameObjectWithTag("HitBox").GetComponent<PlayerHealth>();
        


        if (transform.parent.parent.parent.parent.name.Contains("Angel"))
        {
            ItemyWPuli =  new List<GameObject>(_itemsManager.AngelRoomItems);
            DevilAngelRoom = true;
        }
        else
        {
            ItemyWPuli = new List<GameObject>(_itemsManager.DevilRoomItems);
        }
      

        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            //generowanie itemku 
            _children = transform.GetChild(i).gameObject;

            int _rand1 = Random.Range(0, ItemyWPuli.Count-1);
            _item = ItemyWPuli[_rand1];

            Instantiate(_item, _children.transform.position, Quaternion.identity, _children.transform);
            ItemyWPuli.Remove(_item);


            //okreslenie ceny za itemek
            if (_playerHealth.RedHeartExist == true)
            {
                if (_item.name.Contains("MultidimensionalBaby"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("Succubus"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("brimstone"))
                {
                    _price = 2;
                }
                else if (_item.name.Contains("spectral"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("Mom's KnifeItem"))
                {
                    _price = 2;
                }
                else if (_item.name.Contains("The Book of Belial 1"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("The Book of Belial2"))
                {
                    _price = 1;
                }

                //ANGEL ROOM

                else if (_item.name.Contains("Fly"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("The Bible"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("BloodoftheMartyr"))
                {
                    _price = 2;
                }
                else if (_item.name.Contains("TheHalo"))
                {
                    _price = 2;
                }
                else if (_item.name.Contains("Book of Shadows"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("Habit"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("HolyMantle"))
                {
                    _price = 2;
                }
                else if (_item.name.Contains("SpearofDestinyPiedestal"))
                {
                    _price = 1;
                }
                else if (_item.name.Contains("Angelic Prism Piedestal"))
                {
                    _price = 2;
                }
                else
                {
                    Debug.Log("nie ustawiono ceny dla   " + _item.name);
                    _price = 1;
                }
                
            }

            else
            {
                _price = 3;
            }

            ////generowanie serc pod itemkiem
            if (_price == 1)
            {
                _cena = Instantiate(RedHeart, new Vector2(_children.transform.position.x, _children.transform.position.y - 0.8f), Quaternion.identity, _children.transform);
                _price = 0;
            }

            else if (_price == 2)
            {
                _cena = Instantiate(TwoRedHearts, new Vector2(_children.transform.position.x, _children.transform.position.y - 0.8f), Quaternion.identity, _children.transform);
                _price = 0;
            }

            else if (_price == 3)
            {
                _cena = Instantiate(ThreeSoulHearts, new Vector2(_children.transform.position.x, _children.transform.position.y - 0.8f), Quaternion.identity, _children.transform);
                _price = 0;
            }


        }
    }

   
}

    
    

