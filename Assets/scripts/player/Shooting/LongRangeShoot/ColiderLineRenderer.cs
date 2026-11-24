using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Mathematics;
using UnityEngine;
using static UnityEditor.Rendering.FilterWindow;

public class ColiderLineRenderer : MonoBehaviour
{
    private LineRenderer _lineRenderer;
    private PolygonCollider2D _polygonCollider;
    private Vector2[] _pointsCollider;
    private int _angle;
    private void OnEnable()
    {
        _angle = transform.parent.parent.GetComponent<LongShootParent>().Direction;    
    }
    void Start()
    {
        _lineRenderer = this.GetComponent<LineRenderer>();
        _polygonCollider = this.GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
        
    }
    public void SetCollider(Vector2[] Pos)
    {

        _pointsCollider = new Vector2[Pos.Length*2];

        if (_angle == 0)
        {
            int index = 0;
            for (int i = 0; i < Pos.Length; i++)
            {
                if (i == 0)
                {
                    _pointsCollider[index] = new Vector2(0.2f, Pos[i].y - transform.position.y);
                    index++;
                   

                }
                _pointsCollider[index] = new Vector2(-0.2f, Pos[i].y - transform.position.y);

                index++;

            }


            for (int i = Pos.Length - 1; i > 0; i--)
            {

                _pointsCollider[index] = new Vector2(0.2f, Pos[i].y - transform.position.y);
                index++;


            }
        }
        if (_angle == -90)
        {
            //right

            int index = 0;

            for (int i = 0; i < Pos.Length; i++)
            {
                if (i == 0)
                {
                    _pointsCollider[index] = new Vector2(0.2f, Pos[i].x - transform.position.x);
                    index++;
                    

                }
                _pointsCollider[index] = new Vector2(-0.2f, Pos[i].x - transform.position.x);

                index++;

            }

            for (int i = Pos.Length - 1; i > 0; i--)
            {

                _pointsCollider[index] = new Vector2(0.2f, Pos[i].x - transform.position.x);
                index++;


            }
        }

        if (_angle == 180)
        {
            int index = 0;

            //down
            for (int i = 0; i < Pos.Length; i++)
            {
                if (i == 0)
                {
                    _pointsCollider[index] = new Vector2(0.2f, transform.position.y - Pos[i].y);
                    index++;
                   

                }
                _pointsCollider[index] = new Vector2(-0.2f, transform.position.y - Pos[i].y);

                index++;

            }

            for (int i = Pos.Length - 1; i > 0; i--)
            {

                _pointsCollider[index] = new Vector2(0.2f, transform.position.y - Pos[i].y);
                index++;


            }
        }

        if (_angle == 90)
        {
            int index = 0;
            //left
            for (int i = 0; i < Pos.Length; i++)
            {
                if (i == 0)
                {
                    _pointsCollider[index] = new Vector2(0.2f, transform.position.x - Pos[i].x);
                    index++;
                   

                }
                _pointsCollider[index] = new Vector2(-0.2f, transform.position.x - Pos[i].x);

                index++;

            }


            for (int i = Pos.Length - 1; i > 0; i--)
            {

                _pointsCollider[index] = new Vector2(0.2f, transform.position.x - Pos[i].x);
                index++;


            }

        }

        _polygonCollider.SetPath(0, _pointsCollider);

    }
}
