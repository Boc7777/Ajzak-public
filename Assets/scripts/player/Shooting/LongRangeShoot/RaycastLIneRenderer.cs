using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using static LongShootMid;

public class RaycastLIneRenderer : MonoBehaviour
{
    //raycast
    public RaycastHit2D Hit;
    private Vector2 _raycastDirection;
    public int Layer, Layer2;

    //sprawdza czy strzela brimstone
    public bool BrimstoneActive;

    [SerializeField] private LongShootMid _longShootMid;
    [SerializeField] private GameObject _lineRenderer;

    private bool _multiBaby;
    private bool _angelicPrism;

    private DirectionShoot _directionShoot;
    void Start()
    {
        _directionShoot = GameObject.Find("Player").GetComponent<DirectionShoot>();
    }

    private void OnEnable()
    {
       

        
    }

    void Update()
    {
        if (BrimstoneActive)
        {
            Hit = Physics2D.Raycast(GameObject.Find(transform.parent.parent.GetComponent<LongShootParent>().FollowPositionName).transform.position , _directionShoot._raycastDirection, Mathf.Infinity, 1 << 7);
            
        }
        else
        {
            Hit = Physics2D.Raycast(transform.parent.parent.transform.position, transform.up, Mathf.Infinity, 1 << 6 | 1 << 7);;
            //Debug.DrawRay(transform.parent.parent.transform.position, transform.up, Color.blue);
           


        }

        
        if (Hit.collider != null)
        {
            
                if (Hit.collider.name == "MultidimensionalBaby(Clone)" && !_multiBaby)
                {
                    GameObject.Find("MultidimensionalBaby(Clone)").GetComponent<MultidimensionalBaby>().MultiBabyLongShootON(gameObject);
                    _multiBaby = true;

                }
                else if (Hit.collider.name != "MultidimensionalBaby(Clone)" && _multiBaby)
                {
                    GameObject.Find("MultidimensionalBaby(Clone)").GetComponent<MultidimensionalBaby>().MultiBabyLongShootOff(gameObject);
                    _multiBaby = false;

                }


                if (Hit.collider.name == "Angelic Prism(Clone)" && !_angelicPrism)
                {
                    GameObject.Find("Angelic Prism(Clone)").GetComponent<AngelicPrism>().PrismLongShootON(gameObject);
                    _angelicPrism = true;

                }
                else if (Hit.collider.name != "Angelic Prism(Clone)" && _angelicPrism)
                {
                    GameObject.Find("Angelic Prism(Clone)").GetComponent<AngelicPrism>().PrismLongShootOff(gameObject);
                    _angelicPrism = false;

                }
            

            if ((int)(Hit.distance / _longShootMid.RopeSegLen) > 1 && !_lineRenderer.activeSelf)
            {
                _lineRenderer.SetActive(true);
            }
            else if ((int)(Hit.distance / _longShootMid.RopeSegLen) <= 1 && _lineRenderer.activeSelf)
            {
               _lineRenderer.SetActive(false);

            }
        }
    }

    

    private void OnDisable()
    {
        BrimstoneActive = false;
        if (_multiBaby)
        {
            GameObject.Find("MultidimensionalBaby(Clone)").GetComponent<MultidimensionalBaby>().MultiBabyLongShootOff(gameObject);
            _multiBaby = false;

        }

        if (_angelicPrism)
        {
            GameObject.Find("Angelic Prism(Clone)").GetComponent<AngelicPrism>().PrismLongShootOff(gameObject);
            _angelicPrism = false;
        }
        _lineRenderer.SetActive(false);
    }


}
