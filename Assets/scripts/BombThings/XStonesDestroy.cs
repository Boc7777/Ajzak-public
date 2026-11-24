using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XStonesDestroy : MonoBehaviour
{
    private int _percentageForSoulHeart;
    private int _percentageForKey;
    private int _percentageForBombs;
    private int _percentageForGoldenChest;
    private int _percentageForSmallRock;
    private int _randomType;
    private int _amountOfThisType;
    private int _percentageForPickUps;
    private int _percentageForOthers;
    private int _randomPickUp;
    private int _randomOther;
    private int _percentageForAmount;
    

    [SerializeField]private GameObject _goldenChest;
    private GameObject _RandomKey;
    private GameObject _RandomBomb;
    [SerializeField]private GameObject _soulHeart;
    [SerializeField]private GameObject _smallRock;

    private ChestPickupManager _chestPickup;
    private StoneManagment _stoneManagment;
    private GameObject _itemsInRoom;

    private void Start()
    {
        _percentageForPickUps = 80; //80
        _percentageForOthers = 20;
        _percentageForSoulHeart = 60;
        _percentageForKey = 20;
        _percentageForBombs = 20;
        _percentageForSmallRock = 50;
        _percentageForGoldenChest = 50;

        _chestPickup = GameObject.Find("ChestManager").GetComponent<ChestPickupManager>();
        _stoneManagment = GameObject.Find("RockManager").GetComponent<StoneManagment>();

        _itemsInRoom = GameObject.FindGameObjectWithTag("ItemsInRoom").gameObject;
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            XStoneRandomDrop();
            Destroy(gameObject);
        }
    }

    private void XStoneRandomDrop()
    {
        if (_stoneManagment._smallRockSpawned == true)
        {
            _percentageForSmallRock = 0;
            _percentageForGoldenChest = 100;
        }
        _randomType = Random.Range(1, 101);
        
        if(_randomType <= _percentageForPickUps)
        {
            _percentageForAmount = Random.Range(1, 101);
            if(_percentageForAmount <= 70)
            {
                _amountOfThisType = 1;
            }
            else if(_percentageForAmount > 70 && _percentageForAmount <= 90)
            {
                _amountOfThisType = 2;
            }
            else
            {
                _amountOfThisType = 3;
            }
            
            for(int i=0; i< _amountOfThisType; i++)
            {
                _randomPickUp = Random.Range(1, 101);
                if (_randomPickUp <= _percentageForSoulHeart) //losowanie Soulheartow
                {
                    Instantiate(_soulHeart, gameObject.transform.position, Quaternion.identity, _itemsInRoom.transform);
                   
                }
                else if (_randomPickUp > _percentageForSoulHeart && _randomPickUp <= _percentageForKey+_percentageForSoulHeart) //losowanie Kluczy
                {
                    _RandomKey= _chestPickup.GoldenChestKeys[Random.Range(0, _chestPickup.GoldenChestKeys.Count)];
                    Instantiate(_RandomKey, gameObject.transform.position, Quaternion.identity, _itemsInRoom.transform);
                    if (_amountOfThisType ==1)
                    {
                        
                        _amountOfThisType++;
                       
                    }
                }
                else if (_randomPickUp > _percentageForSoulHeart+_percentageForKey && _randomPickUp<= _percentageForSoulHeart +_percentageForKey + _percentageForBombs) //losowanie bomb
                {
                    _RandomBomb = _chestPickup.GoldenChestBombs[Random.Range(0, _chestPickup.GoldenChestBombs.Count)];
                    Instantiate(_RandomBomb, gameObject.transform.position, Quaternion.identity, _itemsInRoom.transform);
                    if (_amountOfThisType == 1)
                    {

                        _amountOfThisType++;
                        
                    }
                }
            }
        }
        else if(_randomType > _percentageForPickUps) //losowanie skrzynki lub SmallRocka
        {
            _randomOther = Random.Range(1, 101);
            
            if (_randomOther <= _percentageForGoldenChest)
            {
                Instantiate(_goldenChest, transform.position, Quaternion.identity, _itemsInRoom.transform);
            }
            else if (_randomOther > _percentageForGoldenChest)
            {
                _stoneManagment._smallRockSpawned = true;
                Instantiate(_smallRock, transform.position, Quaternion.identity, _itemsInRoom.transform);
            }
        }

        

        
    }
}
