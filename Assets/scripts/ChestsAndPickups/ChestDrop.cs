using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestDrop : MonoBehaviour
{
    //Parent do dodawania
    private GameObject _itemParent;

    private Rigidbody2D _rb2d;

    
    //Zmienne do losowania
    private int _RandomType;
    private int _percentageForPickups;
    private int _percentageForItem;
    private int _percentageForAnotherSmallerChest;
    private int _RandomPickup;
    private int _RandomAmountOfPickups;

    //Wylosowane Obiekty
    private GameObject _randomItem;
    private GameObject _RandomCoin;
    private GameObject _RandomHeart;
    private GameObject _RandomKey;
    private GameObject _RandomBomb;
    [SerializeField]private GameObject _goldenChest;
    [SerializeField]private GameObject _stoneChest;
    private GameObject _SmallerChest;

    //Rodzaj Skrzynki
    [SerializeField] private bool _isGoldenChest;
    [SerializeField] private bool _isStoneChest;

    //Procenty na Dane Pickupy
    private int _coinsPercentage;
    private int _heartsPercenatge;
    private int _keysPercentage;
    private int _bombsPercentage;

    //Listy
    private ItemManager _itemList;
    private ChestPickupManager _chestPickup;

    //Sprite zamknietej skrzynki
    [SerializeField] private Sprite _lockedGoldenChest;
    [SerializeField] private Sprite _lockedStoneChest;

    private float _whereInstantiate;
    private ChestUpChecker _chestUpChecker;
    [SerializeField]private bool _normalChest;

    void Start()
    {
        _percentageForPickups = 60; //procent na pickupy //60
        _percentageForItem = 20;   //procent na item //20
        _percentageForAnotherSmallerChest = 20; //procent na mniejsza skrzynke ktora moze dropn¹æ //20
        _coinsPercentage = 35; //35%
        _heartsPercenatge = 20; //20%
        _keysPercentage = 15; // 15%
        _bombsPercentage = 30; // 30%
        _itemList = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        _chestPickup = GameObject.Find("ChestManager").GetComponent<ChestPickupManager>();
        _rb2d = gameObject.GetComponent<Rigidbody2D>();
        _itemParent = GameObject.FindGameObjectWithTag("ItemsInRoom").gameObject;
        if(_normalChest)
        _chestUpChecker = gameObject.transform.Find("UpChecker").GetComponent<ChestUpChecker>();
        //_whereInstantiate = -1f;
        
    }
    private void Update()
    {
        if (_normalChest)
        {
            if (_chestUpChecker.OpenedFromTop == true)
            {
                _whereInstantiate = -1f;

            }
            else if (_chestUpChecker.OpenedFromTop == false)
            {
                _whereInstantiate = 0.4f;

            }
        }
    }
    public void ChestDroprate()
    {


        if (_itemList.GoldenChestItems.Count == 0) //jesli wyczerpi¹ sie itemy z listy to losuja siê tylko itemy
        {
            _percentageForPickups = 101;

        }
        _RandomType = Random.Range(1, 101);
        
        if (_RandomType <= _percentageForPickups)
        {
            _RandomAmountOfPickups = Random.Range(2, 8);
            for (int i = 0; i < _RandomAmountOfPickups; i++)
            {
                _RandomPickup = Random.Range(1, 101);
                if (_RandomPickup <= _coinsPercentage) //losowanie monet
                {
                    _RandomCoin = _chestPickup.GoldenChestCoins[Random.Range(0, _chestPickup.GoldenChestCoins.Count)];
                    Instantiate(_RandomCoin, gameObject.transform.position + new Vector3(0, _whereInstantiate, 0), Quaternion.identity, _itemParent.transform);

                }
                else if (_RandomPickup > _coinsPercentage && _RandomPickup <= _heartsPercenatge+_coinsPercentage) // losowanie Serc
                {
                    _RandomHeart = _chestPickup.GoldenChestHearts[Random.Range(0, _chestPickup.GoldenChestHearts.Count)];
                    Instantiate(_RandomHeart, gameObject.transform.position + new Vector3(0, _whereInstantiate, 0), Quaternion.identity, _itemParent.transform);
                }
                else if (_RandomPickup > _heartsPercenatge+_coinsPercentage && _RandomPickup <= _keysPercentage+_heartsPercenatge+_coinsPercentage) // losowanie Kluczy
                {
                    _RandomKey = _chestPickup.GoldenChestKeys[Random.Range(0, _chestPickup.GoldenChestKeys.Count)];
                    Instantiate(_RandomKey, gameObject.transform.position + new Vector3(0, _whereInstantiate, 0), Quaternion.identity, _itemParent.transform);
                }
                else if(_RandomPickup >= 101 - _bombsPercentage) //losowanie bomb
                {
                    _RandomBomb = _chestPickup.GoldenChestBombs[Random.Range(0, _chestPickup.GoldenChestBombs.Count)];
                    Instantiate(_RandomBomb, gameObject.transform.position + new Vector3(0, _whereInstantiate, 0), Quaternion.identity, _itemParent.transform);
                }

            }
        }
        else if (_RandomType > _percentageForPickups && _RandomType <= _percentageForPickups+_percentageForItem) //losowanie itemow
        {
            _randomItem = _itemList.GoldenChestItems[Random.Range(0, _itemList.GoldenChestItems.Count)];
            gameObject.transform.Find("PushBack").gameObject.SetActive(true); //kiedy wylosuje sie item odpala sie niewidzialny kwadrat "PushBack"(zeby missclikiem nie wziac jakiegos slabego itema) i znika po 1 sekundzie
            StartCoroutine(_itemSpawn(0.25f)); //Item Respi sie po czasie zeby go nie wziac odrazu
            _rb2d.isKinematic = true; //wylaczenie RigidBody dla skrzynki z Itemem
            StartCoroutine(_pushbackCooldown(0.5f)); //Wylaczenie PushBacku
            
        }

        else if (_RandomType >= _percentageForPickups + _percentageForItem && _RandomType <= _percentageForPickups + _percentageForItem + _percentageForAnotherSmallerChest) //losowanie mniejszej skrzynki
        {
            StartCoroutine(_spawnMiniChest(0.3f));
            StartCoroutine(_openMiniChest(0.5f));
        }
        
    }
    private IEnumerator _pushbackCooldown(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        gameObject.transform.Find("PushBack").gameObject.SetActive(false);
    }

    private IEnumerator _itemSpawn(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        Instantiate(_randomItem, gameObject.transform.position + new Vector3(0, 0.4f, 0), Quaternion.identity, _itemParent.transform);
    }
    private IEnumerator _openMiniChest(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        if(_isGoldenChest)
        _SmallerChest.transform.tag = "LockedMiniChest";

        else if (_isStoneChest)
        {
            _SmallerChest.transform.tag = "LockedStoneChest";
        }
    }
    private IEnumerator _spawnMiniChest(float cooldown)
    {
        yield return new WaitForSeconds(cooldown);
        if (_isGoldenChest == true)
        {

            _SmallerChest = Instantiate(_goldenChest, gameObject.transform.position + new Vector3(-0.2f, -0.4f, 0), Quaternion.identity, _itemParent.transform);
            _SmallerChest.gameObject.GetComponent<SpriteRenderer>().sprite = _lockedGoldenChest;
            _SmallerChest.transform.localScale /= 1.5f;
            if (_SmallerChest.transform.localScale.x <= 1.5f)
            {
                _SmallerChest.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }

        if (_isStoneChest == true)
        {
            _SmallerChest = Instantiate(_stoneChest, gameObject.transform.position + new Vector3(-0.2f, -0.4f, 0), Quaternion.identity, _itemParent.transform);


            _SmallerChest.gameObject.GetComponent<SpriteRenderer>().sprite = _lockedStoneChest;
            _SmallerChest.transform.localScale /= 1.5f;
            if (_SmallerChest.transform.localScale.x <= 1.5f)
            {
                _SmallerChest.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            }
        }
    }
    
}

