using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalRockDestroy : MonoBehaviour
{
    private UpperSideOfStone _upperSideOfStone;
    private RightSideOfStone _rightSideOfStone;
    private LeftSideOfStone _leftSideOfStone;
    private BottomSideOfStone _bottomSideOfStone;
    private HoleDetector _holeDetector;

    private void Start()
    {
        _upperSideOfStone = gameObject.transform.Find("Top").GetComponent<UpperSideOfStone>();
        _bottomSideOfStone = gameObject.transform.Find("Bottom").GetComponent<BottomSideOfStone>();
        _leftSideOfStone = gameObject.transform.Find("Left").GetComponent<LeftSideOfStone>();
        _rightSideOfStone = gameObject.transform.Find("Right").GetComponent<RightSideOfStone>();
        _holeDetector = gameObject.transform.Find("HolesDetector").GetComponent<HoleDetector>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_holeDetector.HoleNearby)
        {
            if (collision.CompareTag("Explosion"))
            {
                Destroy(gameObject);
            }
        }
        

       
    }
    
}
