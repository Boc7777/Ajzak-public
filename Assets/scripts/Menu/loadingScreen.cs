using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class loadingScreen : MonoBehaviour
{
    private RoomsPrefabs _templates;
    private Image _image;
    public float Zanik = 1f;
    private player_movement _playerMovement;

    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();
        _image = gameObject.GetComponent<Image>();
        _playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<player_movement>();
    
    }

    void Update()
    {
        if (_templates._specialRoomsToSpawn == 0 && _image.color.a > 0)
        {   
            
            Color customColor = new Color(1f, 1f, 1f, Zanik);
            _image.color = customColor;
            Zanik -= 0.03f;
        }
       
        if(_image.color.a > 0)
        {
            _playerMovement.BlockMovement = 0;
        }
        else
        {
            _playerMovement.BlockMovement = 1;
        }
    }
    public void ColorReset()
    {
        _image.color = new Color(1f, 1f, 1f, 1f);
        Zanik = 1f;
    } 
}

