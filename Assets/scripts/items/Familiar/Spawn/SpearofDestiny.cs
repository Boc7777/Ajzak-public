using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpearofDestiny : MonoBehaviour
{
    private float _angle;
    private PlayerShooting _playerShooting;
    private DirectionShoot directionShoot;
    private GameObject _playerCords;

    private float _xCord,_yCord;
    void Start()
    {
        _playerShooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
        directionShoot = GameObject.Find("Player").GetComponent<DirectionShoot>();
        _playerCords = GameObject.Find("Player");
        _xCord = 0;
        _yCord = 1.2f;

    }

    void Update()
    {

        Direction();
        transform.position = _playerCords.transform.position + new Vector3(_xCord * 0.7f, _yCord * 0.9f - 0.3f, 0);

    }
    private void Direction()
    {
        if (directionShoot.SumDirection == 1)
        {
            _angle = directionShoot.Angle;
            if (_angle == 0)
            {
                _yCord = 1.2f;
                _xCord = 0;
                transform.rotation = Quaternion.Euler(0, 0, 0);

            }
            else if (_angle == 180)
            {
                _yCord = -1f;
                _xCord = 0;
                transform.rotation = Quaternion.Euler(0, 0, 180);

            }
            else if (_angle == 90)
            {
                _xCord = -1f;
                _yCord = 0;
                transform.rotation = Quaternion.Euler(0, 0, 90);
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else if (_angle == -90)
            {
                _xCord = 1f;
                _yCord = 0;
                transform.rotation = Quaternion.Euler(0, 0, -90);
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
        }
        else if (directionShoot.SumDirection > 1)
        {
            _angle = _playerShooting.Angle2;

            if (_angle == 45)
            {
                _xCord = -1f;
                _yCord= 1f;
                transform.rotation = Quaternion.Euler(0, 0, 45);
            }
            else if (_angle == -45)
            {
                _xCord = 1f;
                _yCord = 1f;
                transform.rotation = Quaternion.Euler(0, 0, -45);
            }
            else if (_angle == 125)
            {
                _xCord = -0.9f;
                _yCord = -0.7f;
                transform.rotation = Quaternion.Euler(0, 0, 125);
            }
            else if (_angle == -125)
            {
                _xCord = 0.9f;
                _yCord = -0.7f;
                transform.rotation = Quaternion.Euler(0, 0, -125);
            }
        }
    }


}
