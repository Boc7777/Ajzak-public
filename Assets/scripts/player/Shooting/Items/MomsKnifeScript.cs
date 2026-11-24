using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MomsKnifeScript : MonoBehaviour
{
    private GameObject _playerCords;
    private float _directionValueX=0;
    private float _directionValueY=0;
    private float _directionX;
    private float _directionY;

    private DirectionShoot _directionShoot;
    private bool _directionForward;



    void Start()
    {
        _playerCords = GameObject.Find("Player");
        _directionShoot = GameObject.Find("Player").GetComponent<DirectionShoot>();
    }

    void Update()
    {

        transform.position = _playerCords.transform.position + new Vector3(_directionValueX * 0.6f, _directionValueY * 0.9f - 0.4f, 0);
        
        if (!transform.GetChild(0).GetComponent<MomsKnifeMove>().enabled)
        {
            Direction();


        }


    }
    private void Direction()
    {
        if (_directionShoot.SumDirection == 1)
        {
            _directionForward = true;
        }
        else if(_directionShoot.SumDirection > 1)
        {
            _directionForward = false;

        }

        if (_directionForward)
        {

            _directionValueX = _directionShoot.DirectionValueX;
            _directionValueY = _directionShoot.DirectionValueY;

            _directionX = _directionShoot.DirectionX;
            _directionY = _directionShoot.DirectionY;

            transform.rotation = Quaternion.Euler(0, 0, _directionShoot.Angle);
           
            if (_directionShoot.Angle == 90)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else if (_directionShoot.Angle == -90)
            {
                
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
        }
        else
        {
            _directionValueX = _directionShoot.DirectionValue2X;
            _directionValueY = _directionShoot.DirectionValue2Y;

            transform.rotation = Quaternion.Euler(0, 0, _directionShoot.Angle2);

        }
    }    
    


    public void Reset()
    {
        transform.position = _playerCords.transform.position + new Vector3(_directionValueX * 0.6f, _directionValueY * 0.9f - 0.4f, 0);

    }
}
