using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeConstructor : MonoBehaviour
{
    private LeftSideOfStone _leftSideOfStone;
    private RightSideOfStone _rightSideOfStone;
    private UpperSideOfStone _upperSideOfStone;
    private BottomSideOfStone _bottomSideOfStone;
    [SerializeField]private GameObject _bridge;
    private void Start()
    {
        _leftSideOfStone = gameObject.transform.Find("Left").GetComponent<LeftSideOfStone>();
        _rightSideOfStone = gameObject.transform.Find("Right").GetComponent <RightSideOfStone>();
        _upperSideOfStone = gameObject.transform.Find("Top").GetComponent<UpperSideOfStone>();
        _bottomSideOfStone = gameObject.transform.Find("Bottom").GetComponent<BottomSideOfStone>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Explosion"))
        {
            if(_rightSideOfStone.BombInRight == true && _leftSideOfStone.IsHoleOnLeft == true)
            {
                Instantiate(_bridge, _leftSideOfStone.LeftBridgePosition, Quaternion.identity, transform.parent);
                Destroy(gameObject);
                Destroy(_leftSideOfStone.HoleOnLeft);
                
            }
            else if(_leftSideOfStone.BombInLeft ==true && _rightSideOfStone.IsHoleOnRight ==true)
            {
                Instantiate(_bridge, _rightSideOfStone.BridgeRightPosition, Quaternion.identity);
                Destroy(gameObject);
                Destroy(_rightSideOfStone.HoleOnRight);
            }
            else if (_upperSideOfStone.BombInTop == true && _bottomSideOfStone.IsHoleOnBot == true)
            {
                Instantiate(_bridge, _bottomSideOfStone.BridgeBottomPosition, Quaternion.identity);
                Destroy(gameObject);
                Destroy(_bottomSideOfStone.HoleOnBottom);
               
            }
            else if(_bottomSideOfStone.BombInBot == true&& _upperSideOfStone.IsHoleOnTop == true)
            {
                Instantiate(_bridge, _upperSideOfStone.BridgeUpPosition, Quaternion.identity);
                Destroy(gameObject);
                Destroy(_upperSideOfStone.HoleOnTop);
            }

        }

        
    }
}
