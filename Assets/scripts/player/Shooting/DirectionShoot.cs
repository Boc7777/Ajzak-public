using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.LightAnchor;
using UnityEngine.UIElements;

public class DirectionShoot : MonoBehaviour
{
    public int OldAngle;
    public int Angle;
    public int Angle2;
    [HideInInspector] public float SumDirection;

    private int[] _direction = new int[4];

    public bool LongShootDelayStatus;

    //pracuje ogólnie nie zale¿nie od znaku
    public int DirectionX;
    public int DirectionY;

    //wartoœæ dodatnia albo minusowa dla prostych kierunków
    public int DirectionValueY;
    public int DirectionValueX;

    //wartoœæ dodatnia albo minusowa dla k¹tów
    public int DirectionValue2Y;
    public int DirectionValue2X;

    public int Rotation;
    
    public Vector2 _raycastDirection;




    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Direction();
    }
    private void Direction()
    {

        OldAngle = Angle;
        if (Input.GetKey("up"))
        {

            _direction[0] = 1;
            Angle = 0;

            Rotation = 0;

            DirectionValueY = 1;
            DirectionValueX = 0; 
            DirectionX = 0;
            DirectionY = 1;
          
            _raycastDirection = Vector2.up;

        }
        else
        {
            _direction[0] = 0;
        }

        if (Input.GetKey("down"))
        {
            _direction[1] = 1;
            Angle = 180;

            Rotation = 180;


            DirectionValueY = -1;
            DirectionValueX = 0;
            DirectionX = 0;
            DirectionY = 1;
            _raycastDirection = Vector2.down;
        }
        else
        {
            _direction[1] = 0;

        }

        if (Input.GetKey("left"))
        {
            _direction[2] = 1;
            Angle = 90;

            Rotation = 90;


            DirectionValueY = 0;
            DirectionValueX = -1;
            DirectionX = 1;
            DirectionY = 0;
            _raycastDirection = Vector2.left;

        }
        else
        {
            _direction[2] = 0;

        }

        if (Input.GetKey("right"))
        {
            _direction[3] = 1;
            Angle = -90;

            Rotation = -90;


            DirectionValueY = 0;
            DirectionValueX = 1;
            DirectionX = 1;
            DirectionY = 0;
            _raycastDirection = Vector2.right;


        }
        else
        {
            _direction[3] = 0;

        }

        if (_direction[0] == 1 && _direction[2] == 1)
        {
            Angle2 = 45;
            DirectionValue2Y = 1;
            DirectionValue2X = -1;
        }
        else if (_direction[0] == 1 && _direction[3] == 1)
        {
            Angle2 = -45;
            DirectionValue2Y = 1;
            DirectionValue2X = 1;
        }
        else if (_direction[1] == 1 && _direction[2] == 1)
        {
            Angle2 = 125;
            DirectionValue2Y = -1;
            DirectionValue2X = -1;
        }
        else if (_direction[1] == 1 && _direction[3] == 1)
        {
            Angle2 = -125;
            DirectionValue2Y = -1;
            DirectionValue2X = 1;
        }

        SumDirection = _direction[0] + _direction[1] + _direction[2] + _direction[3];

        if (OldAngle != Angle)
        {
            StartCoroutine(LongShootDelay());
        }

    }
    public IEnumerator LongShootDelay()
    {
        LongShootDelayStatus = true;
        yield return new WaitForSeconds(0.03f);
        LongShootDelayStatus = false;

    }
}
