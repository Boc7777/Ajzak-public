using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class GeminiRope : MonoBehaviour
{
    //pozycje
    private Vector2 _startPoint;
    private Vector2 _endPoint;
    private Vector2 _distance;


    //dane linerenderer
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public float RopeSegLen = 0.25f;
    private int segmentLength = 10;
    //private float lineWidth = 0.1f;
    



    

    //nie pamiêtam
    private Vector2 _positionElement;



    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();

    }
    private void OnEnable()
    {
        _positionElement = new Vector3(0, 0, 0);
        

        for (int i = 0; i < segmentLength; i++)
        {

            this.ropeSegments.Add(new RopeSegment(new Vector3(_positionElement.x, _positionElement.y)));
            _positionElement += new Vector2(1, 1);

        }

    }

    void Update()
    {
        _startPoint = GameObject.Find("Player").transform.position;
        _endPoint = transform.position;
        _distance =  (_startPoint - _endPoint) / segmentLength;
        Vector2 changeAmount = new Vector2(_distance.x / segmentLength, _distance.y / segmentLength);

        Debug.Log(_startPoint - _endPoint);

        for (int i = 0; i < segmentLength; i++)
        {
            RopeSegment firstSeg = this.ropeSegments[i];

            firstSeg.posNow = _endPoint + changeAmount;
            this.ropeSegments[i] = firstSeg;

            changeAmount += _distance;
        }
        Debug.Log(changeAmount);
        this.DrawRope();


    }

    private void FixedUpdate()
    {

        this.Simulate();

    }

    private void Simulate()
    {
        //SIMULATION

        //Vector2 startPosition = StartPoint;
        //int range = 0;
        //if (segmentLength > 6)
        //{
        //    range = 5;
        //}
        //for (int i = 1; i < this.segmentLength - range; i++)
        //{
        //    startPosition += new Vector2(_segmentCordX, _segmentCordY);
        //    RopeSegment firstSegment = this.ropeSegments[i];

        //    firstSegment.posNow = startPosition;
        //    this.ropeSegments[i] = firstSegment;
        //}


        //CONSTRAINTS
        //for (int i = 0; i < 50; i++)
        //{
        //    this.ApplyConstraint();
        //}
    }

    //private void ApplyConstraint()
    //{
    //    //Constrant to First Point 
    //    RopeSegment firstSegment = this.ropeSegments[0];
    //    firstSegment.posNow = this.StartPoint;
    //    this.ropeSegments[0] = firstSegment;


    //    //Constrant to Second Point 
    //    RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
    //    _endPoint = new Vector2(raycastLIneRenderer.Hit.point.x, raycastLIneRenderer.Hit.point.y);
    //    this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

    //    Vector2 startPosition = StartPoint;

    //    for (int i = 0; i < this.segmentLength - 1; i++)
    //    {

    //        RopeSegment firstSeg = this.ropeSegments[i];
    //        RopeSegment secondSeg = this.ropeSegments[i + 1];

    //        float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
    //        float error = Mathf.Abs(dist - this.RopeSegLen);
    //        Vector2 changeDir = Vector2.zero;

    //        if (dist > RopeSegLen)
    //        {
    //            changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
    //        }
    //        else if (dist < RopeSegLen)
    //        {
    //            changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
    //        }

    //        Vector2 changeAmount = changeDir * error;
    //        if (i != 0)
    //        {
    //            firstSeg.posNow -= changeAmount * 0.4f;
    //            this.ropeSegments[i] = firstSeg;
    //            secondSeg.posNow += changeAmount * 0.4f;
    //            this.ropeSegments[i + 1] = secondSeg;
    //        }



    //    }
    //}

    private void DrawRope()
    {
        //float lineWidth = this.lineWidth;
        //lineRenderer.startWidth = lineWidth;
        //lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositionsV3 = new Vector3[this.segmentLength];


        for (int i = 0; i < this.segmentLength; i++)
        {

            ropePositionsV3[i] = this.ropeSegments[i].posNow;

           
        }

        lineRenderer.positionCount = ropePositionsV3.Length;
        lineRenderer.SetPositions(ropePositionsV3);




    }

    public struct RopeSegment
    {
        public Vector2 posNow;
        public Vector2 posOld;

        public RopeSegment(Vector2 pos)
        {
            this.posNow = pos;
            this.posOld = pos;
        }
    }
    private void OnDisable()
    {
        ropeSegments = new List<RopeSegment>();
        lineRenderer.positionCount = 0;
    }
}
