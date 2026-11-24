using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doors : MonoBehaviour
{
   
    [SerializeField] private string _door;
    private GameObject _player;
    private GameObject _camera;
    private GameObject _miniMapCamera;

    private LongShootParent _longShootParent;

    [SerializeField] GameObject _horizontalDoors;

    private EventManager _eventManager;
   
    void Start()
    {
        _player = GameObject.Find("Player");
        _eventManager=GameObject.Find("EventManager").GetComponent<EventManager>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //uruchomienie eventu do teleportowania familiarów
            _eventManager.DoTeleportFamiliar();
            if (GameObject.Find("Player").GetComponent<PlayerItems>().HolyMantleActive)
            {
                GameObject.Find("Player").transform.Find("HitBox").GetComponent<PlayerHealth>().ImmortalHolyMantle = true;
            }
            //if (GameObject.Find("Mid").GetComponent<LongShootParent>().BrimWork)
            //{
            //    GameObject.Find("Mid").GetComponent<LongShootParent>().Disable();
            //}

            if (_door == "TOP")
            {
                _player.transform.position = new Vector3(_player.transform.position.x , _player.transform.position.y +4f, 0);
                CameraMove(0,11);
                MiniMapCameraMove(0, 11);
            }
            if (_door == "LEFT")
            {
                _player.transform.position = new Vector3(_player.transform.position.x - 6.6f, _player.transform.position.y, 0);
                CameraMove(-18, 0);
                MiniMapCameraMove(-12.5f, 0);
            }
            if (_door == "RIGHT")
            {
                _player.transform.position = new Vector3(_player.transform.position.x + 6.6f, _player.transform.position.y, 0);
                CameraMove(18, 0);
                MiniMapCameraMove(12.5f, 0);
            }
            if (_door == "BOTTOM")
            {
                _player.transform.position = new Vector3(_player.transform.position.x , _player.transform.position.y- 4f, 0);
                CameraMove(0, -11);
                MiniMapCameraMove(0, -11);
            }

            if (_player.GetComponent<PlayerItems>().MomsKnife)
            {
                //GameObject.Find("Mom's Knife(Clone)").GetComponent<MomsKnifeScript>().Stay = true;
                _player.GetComponent<PlayerShooting>().Stoploading = false;

            }
        }
    }
    private void CameraMove(float x, float y)
    {
        _camera = GameObject.FindGameObjectWithTag("camera");
        _camera.transform.position = new Vector3(_camera.transform.position.x+x, _camera.transform.position.y+y, -10);
    }
    private void MiniMapCameraMove(float x, float y)
    {
        _miniMapCamera = GameObject.FindGameObjectWithTag("MiniMapCamera");
        _miniMapCamera.transform.position = new Vector3(_miniMapCamera.transform.position.x + x, _miniMapCamera.transform.position.y + y, -10);
    }

}
