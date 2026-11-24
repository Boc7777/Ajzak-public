using System.Collections.Generic;
using UnityEngine;


public class AngelicPrism : MonoBehaviour
{
    private Transform _playerCord;

    private float _timer;
    private float x, y;
    public List<Color> colorPrism = new List<Color>();

    public List<GameObject> _tears;

    private int _randomColor;

    private float _angelicPrismMove;
    private int[] _angelisPrismIndex;

    Dictionary<int, List<GameObject>> _dictonaryActiveLongRange = new Dictionary<int, List<GameObject>>();



    void Start()
    {
        _angelisPrismIndex = new int[] { 0, 0 };

        _playerCord = GameObject.Find("Player").transform;


    }

    private void Update()
    {
        _timer += Time.deltaTime / 2;
        x = Mathf.Cos(_timer) * 2;
        y = Mathf.Sin(_timer) * 2;
        transform.position = _playerCord.transform.position + new Vector3(x, y, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<arrows>() && !collision.GetComponent<arrows>().AngelicPrismSpawn) 
        {
            colorPrism.Add(Color.red);
            colorPrism.Add(Color.yellow);
            colorPrism.Add(Color.green);
            colorPrism.Add(Color.blue);
            Destroy(collision.gameObject);

            GameObject.Find("Player").GetComponent<TearsCreator>().CreateTears(collision.GetComponent<arrows>().Direction, 4,transform.position);
            _tears = GameObject.Find("Player").GetComponent<TearsCreator>().Tears;

            _angelicPrismMove = -4.5f;
            for (int i = 0; i < _tears.Count; i++)
            {

                _tears[i].GetComponent<arrows>().AngelicPrismSpawn = true;

                _randomColor = Random.Range(0, colorPrism.Count);
                _tears[i].GetComponent<Renderer>().material.color = colorPrism[_randomColor];
                colorPrism.RemoveAt(_randomColor);
                _tears[i].GetComponent<arrows>().SetEndPosition(_angelicPrismMove, _angelicPrismMove);
                _angelicPrismMove += 3f;
            }
            
        }
       
    }
    public void PrismLongShootON(GameObject lineRenderer)
    {
        List<GameObject> _longShoot = GameObject.Find("Player").GetComponent<TearsCreator>().ActiveLongRange(4, lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().TypeShoot, gameObject.name);
        _longShoot[0].GetComponent<LongShootParent>().MovePosition(0,GetComponent<BoxCollider2D>().bounds.size);
        _longShoot[1].GetComponent<LongShootParent>().MovePosition(0,GetComponent<BoxCollider2D>().bounds.size);
        _longShoot[2].GetComponent<LongShootParent>().MovePosition(0, GetComponent<BoxCollider2D>().bounds.size);
        _longShoot[3].GetComponent<LongShootParent>().MovePosition(0, GetComponent<BoxCollider2D>().bounds.size);


        float angle = GameObject.Find("Player").GetComponent<DirectionShoot>().Angle;
        _longShoot[0].transform.rotation = Quaternion.Euler(0,0, angle+30);
        _longShoot[1].transform.rotation = Quaternion.Euler(0, 0, angle+15);

        

        _longShoot[2].transform.rotation = Quaternion.Euler(0, 0, angle -30);
        _longShoot[3].transform.rotation = Quaternion.Euler(0, 0, angle - 15);




        _dictonaryActiveLongRange.Add(lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS, _longShoot);
    }

    public void PrismLongShootOff(GameObject lineRenderer)
    {

        _dictonaryActiveLongRange[lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS][0].GetComponent<LongShootParent>().Disable();
        _dictonaryActiveLongRange[lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS][1].GetComponent<LongShootParent>().Disable();
        _dictonaryActiveLongRange[lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS][2].GetComponent<LongShootParent>().Disable();
        _dictonaryActiveLongRange[lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS][3].GetComponent<LongShootParent>().Disable();

        _dictonaryActiveLongRange.Remove(lineRenderer.transform.parent.parent.GetComponent<LongShootParent>().IndexCreatedLS);
    }

}
