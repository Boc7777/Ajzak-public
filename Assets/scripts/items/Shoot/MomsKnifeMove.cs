using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MomsKnifeMove : MonoBehaviour
{
    public float Distance;
    private GameObject _playerCords;
    private float _xCord = 0;
    private float _yCord = 0;
    private Vector3 _newPosition;
    private Vector3 _previousPosition;
    public bool Stay;
    private PlayerStats _playerStats;

    private bool _throw;



    void Start()
    {
        Stay = true;
        _playerCords = GameObject.Find("Player");
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
    }


   
    public void SetPatch(float sumDirection)
    {
        _throw = true;
        DirectionShoot directonShoot = GameObject.Find("Player").GetComponent<DirectionShoot>();
        if (sumDirection == 1)
        {
            _xCord = directonShoot.DirectionValueX;
            _yCord = directonShoot.DirectionValueY;
        }
        else
        {
            _xCord = directonShoot.DirectionValue2X;
            _yCord = directonShoot.DirectionValue2Y;
        }
        _newPosition = transform.position + new Vector3(_xCord * 120f * Distance, _yCord * 120f * Distance, 0);
    }

    void Update()
    {
        if (_throw)
        {
            Throw();
        }
        else
        {
            Back();
        }
      
    }
    private void Throw()
    {
       

        if (Vector3.Distance(transform.position, _playerCords.transform.position) >= _playerStats.Range * 1.5f * Distance)
        {
            _throw = false;
            
        }
        
        transform.position = Vector2.MoveTowards(transform.position, _newPosition, 12f * Time.deltaTime);
    }

    private void Back()
    {
        _previousPosition = _playerCords.transform.position + new Vector3(_xCord * 0.6f, _yCord * 0.9f - 0.4f, 0);
        transform.position = Vector2.MoveTowards(transform.position, _previousPosition, 12f * Time.deltaTime);

        if (transform.position == _previousPosition)
        {
            GetComponent<MomsKnifeMove>().enabled = false;
        }
    }
}
