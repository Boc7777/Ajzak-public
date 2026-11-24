using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class arrows : MonoBehaviour
{
    //---rzeczy zwi¹zane z tears--
        private Rigidbody2D rb;
        [SerializeField] private GameObject _tears,_bomb;
        //poruszanie
        private Vector2 _StartPosition;
        public Vector2 EndPosition;
        private float _directionX;
        private float _shootSpeedMove;

        //dynamicze strzelanie
        private float _dynamicDirectionX, _dynamicDirectionY;
        private float _dynamic;

        public float Scale;

        //statyski
        public float Dmg, Tears, ShootSpeed, Range;

        //direction shooting
        public float ShootingY,ShootingX;

        //d³ugoœæ drogi do przebycia
        public float RangeDinstance;    

    //--end ters--

   
    //--rzeczy zwi¹zane z player--
        public float Direction;
        private PlayerStats _StatsScript;
        private PlayerShooting _Shooting;
        private PlayerItems _itemsPlayer;
        private TearsCreator _tearsCreator;
        private Vector3 _playerPosition;
        private float _horizontal, _vertical;

    //--end player---


    //---items--
        public bool Poison,Piercing, Spectral, Spider, CricketsBody,Ipecac,RubberCement,Proptosis,DrFetus, BloodMartyr;

        //spider
        public bool Copy;

        //crickedbody
        private GameObject arrowCopy;
        private float _randomAngle;

        //antygravity
        public bool StopMove;

        //ipecac
        private bool _flyIpecac;
        private bool _highPositionIpecac;
        [SerializeField]private GameObject _ipecacBoom;

        //rubercement
        private bool _rubberCementHit;
        private float _rubberDynamic;

        //proptosis
        public float PercentScale;

        //DrFetus
        private bool DrFetusSlow;

        //MultidimensionalBaby
        public bool MultiBabyHit;

        //AngelicPrism
        public bool AngelicPrismSpawn;

        //DeathsTouch
        public bool DeathsTouch;

    //--end item----




    void Awake()
    {
       
        //kierunek
        Direction = GameObject.Find("Player").GetComponent<DirectionShoot>().Angle;

        
        //zmienne od kierunku chodzenia
        _horizontal = GameObject.Find("Player").GetComponent<player_movement>().Horizontal;
        _vertical = GameObject.Find("Player").GetComponent<player_movement>().Vertical;


        _flyIpecac = false;

        //startowa pozycja
        _StartPosition = transform.position;

        _StatsScript = GameObject.Find("Player").GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        _Shooting = GameObject.Find("Player").GetComponent<PlayerShooting>();
        _itemsPlayer = GameObject.Find("Player").GetComponent<PlayerItems>();

        //stats
        Range = _StatsScript.Range;
        ShootSpeed = _StatsScript.ShootSpeed;
        Dmg = _StatsScript.Dmg;
        Tears=_StatsScript.Tears;
        _shootSpeedMove = ShootSpeed;


        RangeDinstance = Range;



      
    }
    private void Start()
    {
        _tearsCreator=GameObject.Find("Player").GetComponent<TearsCreator>();
        //pocz¹tkowa scala dla proptosis


        Items();
    }

    private void Update()
    {
        //strzelanie ipecac
        if (Ipecac)
        {
            //mo¿liwoœæ latania ipecac
            if (Vector2.Distance(transform.position, _StartPosition) > Range * 0.2f)
            {
                _flyIpecac = true;

            }
            if (Vector2.Distance(transform.position, _StartPosition) > Range * 0.98f)
            {
                _flyIpecac = false;

            }

            //sposób rzucania ipecac
            if ((_directionX == 1 || _directionX == -1) && !_highPositionIpecac)
            {
                EndPosition = new Vector2(EndPosition.x, EndPosition.y + 3f);
                _highPositionIpecac = true;

            }
            else if (Vector2.Distance(transform.position, _StartPosition) > Range * 0.1f && (_directionX == 1 || _directionX == -1))
            {
                EndPosition = new Vector2(EndPosition.x, EndPosition.y - 6*Time.deltaTime);
            }



        }
        //pocisk opada 
        else if(Vector2.Distance(transform.position, _StartPosition) > Range * 0.8f && (_directionX == 1 || _directionX == -1)&&!_rubberCementHit && !DrFetus)
        {

            EndPosition = new Vector2(EndPosition.x,EndPosition.y-0.025f);
        }
        
        
        if (!Copy )
        {

          
            if (DrFetus && Vector2.Distance(transform.position, _StartPosition) > Range * 0.5)
            {
                _shootSpeedMove = ShootSpeed * Mathf.Abs(Vector2.Distance(transform.position, _StartPosition) / Range - 1.43f);
            }

            //usuwanie tears
            if (Vector2.Distance(transform.position, _StartPosition) > RangeDinstance && !Ipecac && !DrFetus)
            {
                DestroyBullet();
            }
            //usuwanie ipecac
            else if(Vector2.Distance(transform.position, _StartPosition) > 5+(RangeDinstance * 0.035f) && Ipecac && !DrFetus)
            {
                DestroyBullet();
            }

            //zmniejszanie tears jeœli ma proptois
            if (Proptosis&& Vector2.Distance(transform.position, _StartPosition)>0.1f)
            {

                PercentScale = Scale * Mathf.Abs(Vector2.Distance(transform.position, _StartPosition) / Range - 1f);
                transform.localScale = new Vector2(PercentScale, PercentScale);
            }

        }
        else
        {
            //usuwanie copy po 1 dystanie
            {            if (Vector2.Distance(transform.position, _StartPosition) > 1)

                DestroyBullet();
            }
        }

        
        
    }
    void FixedUpdate()
    {
        //poruszanie tearss
        if (!StopMove&&!Copy)
        {

            transform.position = Vector2.MoveTowards(transform.position, EndPosition, _shootSpeedMove * Time.deltaTime);

        }
        if (Copy)
        {
            rb.velocity = transform.up * 250*Time.deltaTime;
        }
    }

    private IEnumerator DeathsTouchRotate(float rotationZ)
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, rotationZ - 72);
        yield return new WaitForSeconds(0.058f);
        StartCoroutine(DeathsTouchRotate(rotationZ - 72));

    }
    private void DestroyBullet()
    {
        //pojawanie cricktebody
        if (CricketsBody && !Copy)
        {
            _randomAngle = Random.Range(0, 90);

            arrowCopy = Instantiate(_tears, gameObject.transform.position, Quaternion.Euler(0, 0,_randomAngle), _Shooting._parent);
            arrowCopy.transform.localScale = transform.localScale / 2;
            arrowCopy.GetComponent<arrows>().Copy = true;

            arrowCopy = Instantiate(_tears, gameObject.transform.position, Quaternion.Euler(0, 0, _randomAngle+90), _Shooting._parent);
            arrowCopy.transform.localScale = transform.localScale / 2;
            arrowCopy.GetComponent<arrows>().Copy = true;

            arrowCopy = Instantiate(_tears, gameObject.transform.position, Quaternion.Euler(0, 0,_randomAngle-180 ), _Shooting._parent);
            arrowCopy.transform.localScale = transform.localScale / 2;
            arrowCopy.GetComponent<arrows>().Copy = true;

            arrowCopy = Instantiate(_tears, gameObject.transform.position, Quaternion.Euler(0, 0, _randomAngle -90), _Shooting._parent);
            arrowCopy.transform.localScale = transform.localScale / 2;
            arrowCopy.GetComponent<arrows>().Copy = true;
        }

        //pojawanie wybuchu
        if (Ipecac)
        {
            Instantiate(_ipecacBoom, gameObject.transform.position, Quaternion.Euler(0, 0, 0), _Shooting._parent);

        }
        Destroy(gameObject);
    }
    private void Items()
    {
        if (Proptosis)
        {
            Scale = transform.localScale.x * 2.3f;

        }

        if (Spectral == true)
        {
            GetComponent<SpriteRenderer>().sortingOrder = 10;
        }
        
        
        if (Ipecac)
        {
            GetComponent<BoxCollider2D>().size = new Vector2(0.8f, 0.8f);
        }

        if (GetComponent<SpriteRenderer>().sprite == _itemsPlayer.DeathsTouchSprite)
        {
            StartCoroutine(DeathsTouchRotate(0));
        }

    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("FootChecker")&&DrFetus)
        {
            ShootSpeed = 0;

        }

        if (collision.CompareTag("wall"))
        {
            ShootSpeed = 0;

            if (RubberCement)
            {
                RubberCementFunction();

            }
            else
            {
                if (Ipecac)
                {
                    Instantiate(_ipecacBoom, gameObject.transform.position, Quaternion.Euler(0, 0, 0), _Shooting._parent);
                }

                if (!DrFetus)
                {
                    Destroy(gameObject);

                }
            }

        }
        if (collision.CompareTag("Rocks") && Spectral == false && !_flyIpecac)
        {
            ShootSpeed = 0;

            if (RubberCement)
            {
                RubberCementFunction();

            }
            else
            {
                if (Ipecac)
                {
                    Instantiate(_ipecacBoom, gameObject.transform.position, Quaternion.Euler(0, 0, 0), _Shooting._parent);
                }
                if (!DrFetus)
                {
                    Destroy(gameObject);

                }
            }
        }
            
        if (collision.CompareTag("monster") && Piercing == false)
        {
            GameObject.Find("EventManager").GetComponent<EventManager>().FuncDestroyTears();

            ShootSpeed = 0;

            if (Ipecac)
            {
                Instantiate(_ipecacBoom, gameObject.transform.position, Quaternion.Euler(0, 0, 0), _Shooting._parent);
            }
            if (!DrFetus)
            {
                Destroy(gameObject);

            }
        }
        
        
        
    }
    
    private void RubberCementFunction()
    {
        _playerPosition = GameObject.Find("Player").transform.position;

        Range -= Vector2.Distance(transform.position, _StartPosition);
        if (_dynamic > 1.3f)
        {
            Range *= 1.3f;
            

            
        }

        if (Direction == 0|| Direction == 180)
        {
            if (_dynamic > 1.3f)
            {
                if (_playerPosition.x - transform.position.x > 0.2f)
                {
                    _rubberDynamic = 8f;
                }
                else if (_playerPosition.x - transform.position.x < -0.2f)
                {
                    _rubberDynamic = -8f;

                }
            }
            EndPosition = new Vector2(EndPosition.x + _rubberDynamic, -EndPosition.y);
        }
        
        else if (Direction == 90|| Direction == -90)
        {
            if (_dynamic > 1.3f)
            {
                if (_playerPosition.y - transform.position.y > 0.2f)
                {
                    _rubberDynamic = 8f;
                }
                else if (_playerPosition.y - transform.position.y < -0.2f)
                {
                    _rubberDynamic = -8f;
                }
            }
            EndPosition = new Vector2(-EndPosition.x, EndPosition.y + _rubberDynamic);

        }
        


        _StartPosition = transform.position;
        _rubberCementHit = true;
    }

    public void SetEndPosition(float changeX,float changeY)
    {
        _dynamic = GameObject.Find("Player").GetComponent<player_movement>().DynamicMove;

        if (_dynamic > 1.3f)
        {
            Range += 0.4f;
            if (_horizontal > 0)
            {
                _dynamicDirectionX = 2f;
            }
            else if (_horizontal < 0)
            {
                _dynamicDirectionX = -2f;
            }

            if (_vertical > 0)
            {
                _dynamicDirectionY = 2f;
            }
            else if (_vertical < 0)
            {
                _dynamicDirectionY = -2f;
            }
        }


        if (Direction == 0)
        {
            EndPosition = new Vector2(_StartPosition.x + _dynamicDirectionX + changeX, _StartPosition.y + _StatsScript.Range + 10);
        }
        else if (Direction == 180)
        {
            EndPosition = new Vector2(_StartPosition.x + _dynamicDirectionX + changeX, _StartPosition.y - _StatsScript.Range - 10);

        }
        else if (Direction == 90)
        {
            _directionX = 1;
            EndPosition = new Vector2(_StartPosition.x - _StatsScript.Range - 10, _StartPosition.y + _dynamicDirectionY + changeY);

        }
        else if (Direction == -90)
        {
            _directionX = -1;
            EndPosition = new Vector2(_StartPosition.x + _StatsScript.Range + 10, _StartPosition.y + _dynamicDirectionY + changeY);

        }
    }

    public void NewEndPosition(float changeX, float changeY)
    {
        

        if (Direction == 0)
        {
            EndPosition -= new Vector2(changeX,0);
            transform.position -= new Vector3(changeX,0,0);
        }
        else if (Direction == 180)
        {
            EndPosition -= new Vector2(changeX,0);
            transform.position -= new Vector3(changeX, 0, 0);


        }
        else if (Direction == 90)
        {
            _directionX = 1;
            EndPosition -= new Vector2(0,changeY);
            transform.position -= new Vector3(0, changeY, 0);

        }
        else if (Direction == -90)
        {
            _directionX = -1;
            EndPosition -= new Vector2(0,changeY);
            transform.position -= new Vector3(0, changeY, 0);

        }
    }

    public void SetVariableFromBrother(GameObject brother)
    {
        arrows brotherScript= brother.GetComponent<arrows>();
        _StartPosition = transform.position;
        RangeDinstance = brotherScript.RangeDinstance- Vector2.Distance(brother.transform.position, brotherScript._StartPosition);
        EndPosition = brotherScript.EndPosition;

    }

    


}
