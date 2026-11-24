using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gipsis : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _magazynek;


    public Animator _animator;
    private bool _isMoving = false;

    [SerializeField] private int _bulletCount;

    private Vector3 _kordyGracza;
    private Transform _playerArrows;
    private GameObject _arrow;

    private RaycastHit2D _hit;
    private int Layer = 9;

    Vector3 raycastDir;
    private float _distance;

    private bool _seria = false;
    private bool shoot = false;
    private bool _checkingshoot = false;

    void Start()
    {
        gameObject.GetComponent<Collider2D>().enabled = false;

        
    }

    void Update()
    {
        _animator.SetBool("isMoving", _isMoving);

        _kordyGracza = GameObject.FindGameObjectWithTag("Player").transform.position;
        _playerArrows = GameObject.FindGameObjectWithTag("arrows").transform;

        raycastDir = _kordyGracza - transform.position;
        _distance = Vector2.Distance((Vector2)_kordyGracza, (Vector2)gameObject.transform.position);

        _hit = Physics2D.Raycast(transform.position, raycastDir, Mathf.Infinity, 1 << 6 | 1 << 9);


        if (_hit.collider.name == "HitBox" && _distance < 4 && _seria == false && shoot == true)
        {
            _seria = true;
            StartCoroutine(Shooting(0.3f));
        }

        if (_playerArrows.childCount == 0 && _checkingshoot == false)
        {
            _checkingshoot = true;
            StartCoroutine(PlayerShotChecking(0.5f));
        }
        else
        {
            shoot = false;
        }
    }
    IEnumerator Shooting(float _delay)
    {
        _isMoving = true;
        gameObject.GetComponent<Collider2D>().enabled = true;

        for (int i = 0; i < _bulletCount; i++)
        {
            yield return new WaitForSeconds(_delay);
            _arrow = Instantiate(_bullet, gameObject.transform.position, Quaternion.identity, _magazynek);
            _arrow.GetComponent<BulletMonster>()._movementArrow = 6f;
            _arrow.transform.Find("bulletGraf").gameObject.GetComponent<SpriteRenderer>().color = Color.magenta;
            gameObject.GetComponent<MonsterSounds>().ShotSound();
           
        }

        _isMoving = false;
        gameObject.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(_delay * 6);

        _seria = false;
        _isMoving = false;
    }
    IEnumerator PlayerShotChecking(float _delay)
    {
        yield return new WaitForSeconds(_delay);
        if (_playerArrows.childCount == 0)
        {
            shoot = true;
        }
        _checkingshoot = false;
    }

   
}
