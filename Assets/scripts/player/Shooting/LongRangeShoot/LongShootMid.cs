using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LongShootMid : MonoBehaviour
{
    //pozycje
    public Vector3 StartPoint;
    private Vector2 _endPoint;
    public Vector2 EndPosition;



    //dane linerenderer
    private LineRenderer lineRenderer;
    private List<RopeSegment> ropeSegments = new List<RopeSegment>();
    public float RopeSegLen = 0.25f;
    private int segmentLength;
    private float lineWidth = 0.5f;
    private float _segmentCordX;
    private float _segmentCordY;



    //collider (zmienna do porównywania pozycji collidera to linerenderera)
    private Vector2[] _ropePositionsV2Check = new Vector2[0];

    //nie pamiêtam
    private Vector2 _positionElement;

    //zmienna od raycasta
    [SerializeField] private RaycastLIneRenderer raycastLIneRenderer;

    private DirectionShoot _direction;





    public float ChangePosX, ChangePosY;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        ChangePosX = 0;
        ChangePosY = 0f;
    }
    private void OnEnable()
    {
        StartPoint = transform.parent.parent.position;

        _direction = GameObject.Find("Player").GetComponent<DirectionShoot>();


        if (raycastLIneRenderer.Hit.collider != null && raycastLIneRenderer.Hit.collider)
        {
            segmentLength = (int)(raycastLIneRenderer.Hit.distance / RopeSegLen);

        }

        float rotation = transform.parent.parent.rotation.z;

       

        if ((rotation <= 0.383f && rotation >= -0.383f) || rotation >= 0.922 || rotation <= -0.922)
        {
            _segmentCordX = (raycastLIneRenderer.Hit.point.x - transform.parent.parent.position.x) / (segmentLength - 1);
            _segmentCordY = RopeSegLen * _direction.DirectionValueY;

        }
        else
        {
            _segmentCordX = RopeSegLen * _direction.DirectionValueX;
            _segmentCordY = (raycastLIneRenderer.Hit.point.y - transform.parent.parent.position.y) / (segmentLength - 1);
        }

        

        

        _positionElement = StartPoint + new Vector3(ChangePosX, ChangePosY, 0);
        for (int i = 0; i < segmentLength; i++)
        {

            this.ropeSegments.Add(new RopeSegment(new Vector3(_positionElement.x, _positionElement.y)));
            _positionElement += new Vector2(_segmentCordX, _segmentCordY);

        }

        _endPoint = new Vector2(raycastLIneRenderer.Hit.point.x, raycastLIneRenderer.Hit.point.y);

    }

    void Update()
    {
        StartPoint = transform.parent.parent.position;


        if (raycastLIneRenderer.Hit.collider != null && raycastLIneRenderer.Hit.collider)
        {


            _endPoint = new Vector2(raycastLIneRenderer.Hit.point.x, raycastLIneRenderer.Hit.point.y);
            int oldSegmentLength = segmentLength;
            segmentLength = (int)(raycastLIneRenderer.Hit.distance / RopeSegLen);

            if (oldSegmentLength != segmentLength)
            {
                ChangeLength(segmentLength - oldSegmentLength);
            }
            

        }


        this.DrawRope();


    }

    private void FixedUpdate()
    {

       this.Simulate();

    }

    private void ChangeLength(int value)
    {

        for (int i = 0; i < Mathf.Abs(value); i++)
        {
            if (value > 0)
            {
                RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];

                this.ropeSegments.Add(new RopeSegment(endSegment.posNow));
                _positionElement += new Vector2(_segmentCordX, _segmentCordY);

            }
            else if (value < 0)
            {
                RopeSegment lastSegment = this.ropeSegments[this.ropeSegments.Count - 1];
                this.ropeSegments.RemoveAt(0);
                _positionElement -= new Vector2(_segmentCordX, _segmentCordY);

            }
        }

    }
    private void Simulate()
    {
        //SIMULATION

        Vector2 startPosition = StartPoint;
        int range = 0;
        if (segmentLength > 6 && raycastLIneRenderer.BrimstoneActive)
        {
            range = 5;
        }
        for (int i = 1; i < this.segmentLength - range; i++)
        {
            startPosition += new Vector2(_segmentCordX, _segmentCordY);
            RopeSegment firstSegment = this.ropeSegments[i];

            firstSegment.posNow = startPosition;
            this.ropeSegments[i] = firstSegment;
        }


        //CONSTRAINTS
        for (int i = 0; i < 50; i++)
        {
           this.ApplyConstraint();
        }
    }

    private void ApplyConstraint()
    {
        //Constrant to First Point 
        RopeSegment firstSegment = this.ropeSegments[0];
        firstSegment.posNow = this.StartPoint;
        this.ropeSegments[0] = firstSegment;


        //Constrant to Second Point 
        RopeSegment endSegment = this.ropeSegments[this.ropeSegments.Count - 1];
        _endPoint = new Vector2(raycastLIneRenderer.Hit.point.x, raycastLIneRenderer.Hit.point.y);
        this.ropeSegments[this.ropeSegments.Count - 1] = endSegment;

        Vector2 startPosition = StartPoint;

        for (int i = 0; i < this.segmentLength - 1; i++)
        {

            RopeSegment firstSeg = this.ropeSegments[i];
            RopeSegment secondSeg = this.ropeSegments[i + 1];

            float dist = (firstSeg.posNow - secondSeg.posNow).magnitude;
            float error = Mathf.Abs(dist - this.RopeSegLen);
            Vector2 changeDir = Vector2.zero;

            if (dist > RopeSegLen)
            {
                changeDir = (firstSeg.posNow - secondSeg.posNow).normalized;
            }
            else if (dist < RopeSegLen)
            {
                changeDir = (secondSeg.posNow - firstSeg.posNow).normalized;
            }

            Vector2 changeAmount = changeDir * error;
            if (i != 0)
            {
                firstSeg.posNow -= changeAmount * 0.4f;
                this.ropeSegments[i] = firstSeg;
                secondSeg.posNow += changeAmount * 0.4f;
                this.ropeSegments[i + 1] = secondSeg;
            }



        }
    }

    private void DrawRope()
    {
        float lineWidth = this.lineWidth;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        Vector3[] ropePositionsV3 = new Vector3[this.segmentLength];
        Vector2[] ropePositionsV2 = new Vector2[this.segmentLength];

        bool setCollider = false;

        for (int i = 0; i < this.segmentLength; i++)
        {

            ropePositionsV3[i] = this.ropeSegments[i].posNow;
            ropePositionsV2[i] = this.ropeSegments[i].posNow;

            if (_ropePositionsV2Check.Length == ropePositionsV2.Length)
            {
                if (_ropePositionsV2Check[i] != ropePositionsV2[i])
                {
                    setCollider = true;
                }
            }
            else
            {
                setCollider = true;
            }
        }

        EndPosition = ropeSegments[ropeSegments.Count - 1].posNow;
        lineRenderer.positionCount = ropePositionsV3.Length;

        //ustawia pozycje lineRenderer
        lineRenderer.SetPositions(ropePositionsV3);

        //ustawia collider
        if (setCollider)
        {
            _ropePositionsV2Check = ropePositionsV2;

            transform.GetComponent<ColiderLineRenderer>().SetCollider(ropePositionsV2);
        }


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