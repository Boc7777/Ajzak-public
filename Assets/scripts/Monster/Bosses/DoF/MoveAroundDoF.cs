using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveAroundDoF : MonoBehaviour
{
    private Transform _DOFCords;
    private float _timer;
    private float x, y;
    private Vector3 _flyPosition;
    private float _movespeed = 3.15f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        _DOFCords = GameObject.Find("DukeOfFly(Clone)").transform;
    }
    // Update is called once per frame
    void Update()
    {

        x = Mathf.Cos(_timer) * 1.3f;
        y = Mathf.Sin(_timer) * 1.3f;
        _timer += Time.deltaTime * 1.7f;


        _flyPosition = _DOFCords.transform.position + new Vector3(x, y, 0);

       
        transform.position = Vector2.MoveTowards(transform.position, _flyPosition, _movespeed * Time.deltaTime);
    }
}
