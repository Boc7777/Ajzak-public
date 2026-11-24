using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MawOfTheVoid : MonoBehaviour
{
    private float _radius;
    private bool _maxRadius;
    private void OnEnable()
    {
        _radius = 1.5f;
        _maxRadius = false;
    }
    private void Update()
    {
        if(!_maxRadius)
        {
            if(_radius >= 2)
            {
                _maxRadius = true;
            }
            _radius += Time.deltaTime;
            Debug.Log(_radius);
        }
        else
        {
            _radius -= Time.deltaTime;
        }

        if(_radius < 1f)
        {
            gameObject.SetActive(false);
        }

        DrawCircle(_radius, 0.5f);

    }
    public void DrawCircle(float radius, float lineWidth)
    {
        var segments = 360;
        var line = GetComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startWidth = lineWidth;
        line.endWidth = lineWidth;
        line.positionCount = segments + 1;

        var pointCount = segments + 1; // add extra point to make startpoint and endpoint the same to close the circle
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius);
        }

        line.SetPositions(points);
    }
}
