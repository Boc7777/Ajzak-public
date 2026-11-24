using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformPlayer : MonoBehaviour
{
    public int GuppsItems
    {
        get { return _guppysItems; }
        set 
        { 
            _guppysItems = value; 
            if(_guppysItems == 1 )
            {
                TransformGuppy();
            }
        
        }
    }
    private int _guppysItems;


    public int SpunItem
    {
        get { return _spunItems; }
        set
        {
            _spunItems = value;
            if (_spunItems == 3 )
            {
                TransformSpun();
            }

        }
    }

    private int _spunItems;


    private PlayerItems _itemsPlayer;
    private PlayerStats _playerStats;


    void Start()
    {
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();

    }


    private void TransformGuppy()
    {
        _itemsPlayer.Fly = true;
        EventManager.DestroyTears += GameObject.Find("PokeDex").GetComponent<CreateFriendlyMonster>().CreateGuppysFly;
    }

    private void TransformSpun()
    {
        _playerStats.Dmg += 2f;
        _playerStats.DmgChange = "+ 2";

        _playerStats.MovementSpeed += 0.15f;
        _playerStats.MovementSpeedChange = "+ 0.15";

        _playerStats.NewStats();
    }

    
}
