using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public List<Image> HealthContainers;

    [SerializeField]private Sprite _fullHeart;
    [SerializeField]private Sprite _halfHeart;
    [SerializeField]private Sprite _emptyHeart;
    [SerializeField]private Sprite _FullSoulHeart;
    [SerializeField]private Sprite _HalfSoulHeart;
    [SerializeField]private Sprite _FullDarkHeart;
    [SerializeField]private Sprite _HalfDarkHeart;

    [SerializeField] private ParticleSystem _darkHeartDead;

    public int _ActualHealth;
    public int _bonusHearts=0;
    public int _maxHealth;

    public bool Immortal;
    public bool ImmortalHolyMantle;

    public bool HPUP = false;

    public bool RedHeartExist = true;

    public int[] _serca = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

    private PlayerStats _playerStats;
    private RoomsPrefabs _templates;

    private bool _zeroActiv = false;
    
    public bool TakenDamage = false;

    private bool halfSoulHeartActive = false;
    private int AmountSoulHeartDeleted = 0;

    private bool _darkHeartBreak = false;
    private bool _usuwanieParticli = false;

    private float delayDamage = 2;

    private float prog = 0.2f;

    public GameObject _pauseMenu;


    void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();
        _playerStats = GameObject.Find("Player").GetComponent<PlayerStats>();
       

        _ActualHealth = 6;
        _maxHealth = _ActualHealth;
        for (int i = 0; i < _ActualHealth/2; i++)  //napelnianie tablicy startowymi serduchami 
        {
            HealthContainers[i].gameObject.SetActive(true);
            _serca[i] = 42;
        }  
    }
    private void Update()
    {

        if(delayDamage <= 1.1)
        {
            delayDamage += Time.deltaTime;

            //float zaokraglona = Mathf.Floor(delayDamage*10);

            if (delayDamage > prog)
            {
                prog += 0.2f;
                blinking();
            }

        }
        else
        {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            prog = 0.2f;
            
        }
   

        

        DisplayHeart();

        RedHeartChecker();

        if (HPUP == true)
        {
            HPUP = false;
            _maxHealth += 2;
            if (_serca[11] == 0)
            {
                for (int i = 11; i >= 0; i--)
                {
                    if (_serca[i] != 0 && (_serca[i] != 42 && _serca[i] != 41 && _serca[i] != 40))
                    {
                        _serca[i + 1] = _serca[i];
                    }
                    if (_serca[i] == 42 || _serca[i] == 41 || _serca[i] == 40)
                    {
                        _serca[i + 1] = 40;
                        break;
                    }
                    else if(i == 0)
                    {
                        _serca[0] = 40;
                    }
                }

                for (int i = 11; i >= 0; i--)
                {
                    if (_serca[0] == 40)
                    {
                        _ActualHealth += 2;
                        _serca[0] = 42;
                        break;
                    }
                    if (_serca[i] == 41 && _ActualHealth < _maxHealth) //if serce jest halfheartem
                    {
                        _serca[i] = 42;
                        _ActualHealth += 1;

                        if (_ActualHealth != _maxHealth)
                        {
                            _serca[i + 1] = 41;
                            _ActualHealth += 1;
                        }
                        break;
                    }
                    else if (_serca[i] == 42 && _ActualHealth < _maxHealth) //if serce jest fullheartem
                    {
                        _ActualHealth += 2;
                        _serca[i + 1] = 42;
                        break;
                    }
                    

                }

            }

            else if (_serca[11] != 0)
            {
                for (int i = 0; i <= 11; i++)
                {

                    if (_serca[i] != 42 && _serca[i] != 41 && _serca[i] != 40)
                    {
                        _serca[i] = 40;
                        _bonusHearts -= 2;



                        if (_serca[11] == 51)
                        {
                            _serca[11] = 52;
                            _bonusHearts += 1;
                        }
                        break;
                    }

                }

                for (int i = 11; i >= 0; i--)
                {
                    if (_serca[i] == 41 && _ActualHealth < _maxHealth) //if serce jest halfheartem
                    {
                        _serca[i] = 42;
                        _ActualHealth += 1;

                        if (_ActualHealth != _maxHealth)
                        {
                            _serca[i + 1] = 41;
                            _ActualHealth += 1;
                        }
                        break;
                    }
                    else if (_serca[i] == 42 && _ActualHealth < _maxHealth) //if serce jest fullheartem
                    {
                        _ActualHealth += 2;
                        _serca[i + 1] = 42;
                        break;
                    }

                }

            }

        }

        if (_ActualHealth <= 0 && _bonusHearts <= 0)
        {
            Dead();
        }
        if (_serca[0] == 0 && _serca[1] == 0 && _serca[2] == 0)
        {
            Dead();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("FullHeart") && _ActualHealth != _maxHealth)
        {
            for (int i = 11; i >= 0; i--)
            {
                if (_serca[i] == 41 && _ActualHealth < _maxHealth) //if serce jest halfheartem
                {
                    _serca[i] = 42;
                    _ActualHealth += 1;

                    if (_ActualHealth != _maxHealth)
                    {

                        _serca[i + 1] = 41;

                        _ActualHealth += 1;
                    }
                    Destroy(collision.gameObject);
                    break;
                }
                else if (_serca[i] == 42 && _ActualHealth < _maxHealth) //if serce jest fullheartem
                {
                    _ActualHealth += 2;
                    _serca[i + 1] = 42;
                    Destroy(collision.gameObject);
                    break;
                }

            }
        }

        if (collision.CompareTag("HalfHeart") && _ActualHealth != _maxHealth)
        {

            for (int i = 11; i >= 0; i--)
            {
                if (_serca[i] == 41 && _ActualHealth < _maxHealth) //if serce jest halfheartem
                {
                    _serca[i] = 42;
                    _ActualHealth += 1;
                    Destroy(collision.gameObject);
                    break;
                }
                else if (_serca[i] == 42 && _ActualHealth < _maxHealth) //if serce jest fullheartem
                {
                    _serca[i + 1] = 41;
                    _ActualHealth += 1;
                    Destroy(collision.gameObject);
                    break;
                }
            }
        }

        if (collision.CompareTag("FullSoulHeart") && _ActualHealth + _bonusHearts != 24 && _serca[11] == 0)
        {
            for (int i = 11; i >= 0; i--)
            {
                if (_serca[i] != 0)
                {
                    if (_serca[i] == 51) //if serce jest Halfheartem
                    {
                        _serca[i] = 52;
                        _serca[i + 1] = 51;
                        _bonusHearts += 2;
                        Destroy(collision.gameObject);
                        break;
                    }
                    if (_serca[i] == 61) //if serce jest Halfheartem
                    {
                        _serca[i] = 62;
                        _serca[i + 1] = 51;
                        _bonusHearts += 2;
                        Destroy(collision.gameObject);
                        break;
                    }

                    else if (_serca[i] == 42 || _serca[i] == 40 || _serca[i] == 41 || _serca[i] == 52 || _serca[i] == 62 || _serca[i] == 61)
                    {
                        _serca[i + 1] = 52;
                        _bonusHearts += 2;
                        Destroy(collision.gameObject);
                        break;
                    }
                }
            }
        }

        if (collision.CompareTag("HalfSoulHeart") && _ActualHealth + _bonusHearts != 24 && _serca[11] == 0)
        {
            for (int i = 11; i >= 0; i--)
            {
                if (_serca[i] != 0)
                {
                    if (_serca[i] == 51) //if serce jest Halfheartem
                    {
                        _serca[i] = 52;
                        _bonusHearts += 1;

                        Destroy(collision.gameObject);
                        break;
                    }

                    if (_serca[i] == 61) //if serce jest Halfheartem
                    {
                        _serca[i] = 62;
                        _bonusHearts += 1;

                        Destroy(collision.gameObject);
                        break;
                    }

                    else if (_serca[i] == 42 || _serca[i] == 41 || _serca[i] == 40 || _serca[i] == 62 || _serca[i] == 61 || _serca[i] == 52) //if serce jest Fullheartem
                    {
                        _serca[i + 1] = 51;
                        _bonusHearts += 1;
                        Destroy(collision.gameObject);
                        break;
                    }
                }
            }
        }

        if (collision.CompareTag("FullDarkHeart") && _ActualHealth != 24 && _serca[11] == 0)
        {
            for (int i = 11; i >= 0; i--)
            {
                if (_serca[i] != 0)
                {
                    if (_serca[i] == 61)
                    {
                        _serca[i] = 62;
                        _serca[i + 1] = 61;
                        _bonusHearts += 2;
                        Destroy(collision.gameObject);
                        break;
                    }
                    if (_serca[i] == 51)
                    {
                        _serca[i] = 52;
                        _serca[i + 1] = 61;
                        _bonusHearts += 2;
                        Destroy(collision.gameObject);
                        break;
                    }
                    else if (_serca[i] == 42 || _serca[i] == 41 || _serca[i] == 40 || _serca[i] == 62 || _serca[i] == 51 || _serca[i] == 52)// wszystkie fule   
                    {
                        _serca[i + 1] = 62;
                        _bonusHearts += 2;
                        Destroy(collision.gameObject);
                        break;
                    }

                }
            }
        }

        if(collision.CompareTag("central"))
        {
            if(_darkHeartBreak == true)
            {

                for (int i = 0; i <= collision.transform.parent.parent.Find("Monstery").childCount-1; i++)
                {
                    


                    GameObject Monster = collision.transform.parent.parent.Find("Monstery").GetChild(i).gameObject;
                    collision.gameObject.GetComponent<cetral>().SpawnedMonsters.Remove(Monster);

                    Destroy(Monster);
                    Instantiate(_darkHeartDead, Monster.transform.position,Quaternion.identity,Monster.transform.parent.parent);
                    
                }
                _darkHeartBreak = false;
            }
        }
    }

    public void TakeDamage(int Damage)
    {
        if(delayDamage > 1) {
            TakenDamage = true;

            if (GameObject.Find("Player").GetComponent<PlayerItems>().Habit)
            {
                GameObject.Find("Player").GetComponent<SpaceBar>().Charging();
            }
            if (!Immortal && !ImmortalHolyMantle)
            {
                if (_playerStats.BloodLustActive)
                {
                    _playerStats.BloodLust();
                }

                if (Damage == 1)
                {
                    for (int i = 11; i >= 0; i--) //for co leci od ostatnich serduch
                    {
                        //Normal Hearty
                        if (_serca[i] == 41 || _serca[i] == 42)
                        {
                            _ActualHealth -= 1;
                            _serca[i] -= 1;
                            DeletePercentagesDevilRoom();
                            break;
                        }
                        //SoulHearty
                        if (_serca[i] == 52 || _serca[i] == 51)
                        {
                            _bonusHearts -= 1;
                            _serca[i] -= 1;
                            break;
                        }
                        //DarkHearty
                        if (_serca[i] == 61 || _serca[i] == 62)
                        {
                            _bonusHearts -= 1;
                            _serca[i] -= 1;
                            break;
                        }
                    }
                }

                if (Damage == 2)
                {
                    if (_serca[0] == 41) // if ostatnie serce jest halfheartem a dostajemy 2damage == dead
                    {
                        HealthContainers[0].sprite = _emptyHeart;
                        Dead();
                    }

                    for (int i = 11; i >= 0; i--)
                    {
                        if (_serca[i] == 42) //if serce jest FullHeartem
                        {
                            _ActualHealth -= 2;
                            _serca[i] = 40;

                            DeletePercentagesDevilRoom();
                            break;
                        }

                        else if (_serca[i] == 41) //if serce jest HalfHeartem
                        {
                            _ActualHealth -= 2;
                            _serca[i] = 40;
                            if (_serca[0] != 41 && _serca[0] != 40) //if ostatnie nie rowna sie 1 lub 0 bo pozniej cofam index a jesli go nie ma to wywala error
                            {
                                _serca[i - 1] = 41; //o tu 
                            }

                            DeletePercentagesDevilRoom();
                            break;
                        }

                        else if (_serca[i] == 52) //if serce jest FullSoulHeartem
                        {
                            _bonusHearts -= 2;
                            _serca[i] = 50;
                            break;
                        }

                        else if (_serca[i] == 51) //if serce jest HalfSoulHeartem
                        {
                            _bonusHearts -= 1;
                            _serca[i] = 50;

                            if (_serca[i - 1] == 52 || _serca[i - 1] == 61 || _serca[i - 1] == 62)
                            {
                                _bonusHearts -= 1;
                                _serca[i - 1]--;
                                break;
                            }

                            else if (_serca[i - 1] == 42 || _serca[i - 1] == 41)
                            {

                                _ActualHealth -= 1;
                                _serca[i - 1]--;

                                DeletePercentagesDevilRoom();
                                break;
                            }

                            else if (_serca[i - 1] == 40) //if serce za nim jest emptyheartem
                            {
                                for (int a = 11; a >= 0; a--) //szukamy zwyklego serca 
                                {
                                    if (_serca[a] == 41) //if serce za nim jest Halfheartem
                                    {
                                        _ActualHealth -= 1;
                                        _serca[a] = 40;

                                        DeletePercentagesDevilRoom();
                                        break;
                                    }
                                    else if (_serca[a] == 42) //if serce za nim jest Fullheartem
                                    {
                                        _ActualHealth -= 1;
                                        _serca[a] = 41;

                                        DeletePercentagesDevilRoom();
                                        break;
                                    }
                                }
                            }

                            break;
                        }

                        else if (_serca[i] == 62) //if serce jest FullHeartem
                        {
                            _bonusHearts -= 2;
                            _serca[i] -= 2;
                            break;
                        }

                        else if (_serca[i] == 61)
                        {
                            _bonusHearts -= 1;
                            _serca[i] -= 1;

                            if (_serca[i - 1] == 62 || _serca[i - 1] == 52 || _serca[i - 1] == 51)
                            {
                                _bonusHearts -= 1;
                                _serca[i - 1] -= 1;
                                break;
                            }
                            else if (_serca[i - 1] == 42 || _serca[i - 1] == 41)
                            {
                                _ActualHealth -= 1;
                                _serca[i - 1] -= 1;

                                DeletePercentagesDevilRoom();
                                break;
                            }
                            else if (_serca[i - 1] == 40) //if serce za nim jest emptyheartem
                            {
                                for (int a = 11; a >= 0; a--) //szukamy zwyklego serca 
                                {
                                    if (_serca[a] == 41) //if serce za nim jest Halfheartem
                                    {
                                        _ActualHealth -= 1;
                                        _serca[a] = 40;

                                        DeletePercentagesDevilRoom();
                                        break;
                                    }
                                    else if (_serca[a] == 42) //if serce za nim jest Fullheartem
                                    {
                                        _ActualHealth -= 1;
                                        _serca[a] = 41;

                                        DeletePercentagesDevilRoom();
                                        break;
                                    }
                                }
                            }
                            break;
                        }
                    }

                }

            }

            ImmortalHolyMantle = false;
            delayDamage = 0;

        }

    }

    public void Dead()
    {
        _pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    private void DisplayHeart()
    {
        for (int i = 0; i <= 11; i++) //caly czas sprawdzane sa wszystkie indeksty tablicy _serca
        {
            //NormalHearty
            if (_serca[i] == 42) 
            {
                HealthContainers[i].gameObject.SetActive(true);
                HealthContainers[i].sprite = _fullHeart;
            }
            if(_serca[i] == 41)
            {
                HealthContainers[i].gameObject.SetActive(true);
                HealthContainers[i].sprite = _halfHeart;
            }
            if (_serca[i] == 40)
            {
                HealthContainers[i].gameObject.SetActive(true);
                HealthContainers[i].sprite = _emptyHeart;
            }
            //SoulHearty
            if (_serca[i] == 52)
            {
                HealthContainers[i].gameObject.SetActive(true);
                HealthContainers[i].sprite = _FullSoulHeart;

            }
            if (_serca[i] == 51)
            {
                HealthContainers[i].gameObject.SetActive(true);
                HealthContainers[i].sprite = _HalfSoulHeart;
            }
            if (_serca[i] == 50)
            {
                HealthContainers[i].gameObject.SetActive(false);
                _serca[i] = 0;
            }
            //DarkHearty 
            if (_serca[i] == 62)
            {
                HealthContainers[i].gameObject.SetActive(true);
                HealthContainers[i].sprite = _FullDarkHeart;

            }
            if (_serca[i] == 61)
            {
                HealthContainers[i].gameObject.SetActive(true);
                HealthContainers[i].sprite = _HalfDarkHeart;

            }
            else if (_serca[i] == 60)
            {
                _darkHeartBreak = true;
                HealthContainers[i].gameObject.SetActive(false);
                _serca[i] = 0;

            }
            else if (_serca[i] == 0)
            {
                HealthContainers[i].gameObject.SetActive(false);
            }
        }
    }

    private void RedHeartChecker()
    {
        if (_serca[0] == 42 || _serca[0] == 41)
        {
            RedHeartExist = true;
        }
        else
        {
            RedHeartExist = false;
        }
    }

    public void HeartDeleter(int HeartsAmount , int KindOfHearts)
    {
        if(KindOfHearts == 40)
        {
            for (int i = 0; i < HeartsAmount; i++)
            {
                _maxHealth -= 2;
                for (int a = 0; a <= 11; a++)
                {
                    if(_serca[0] != 40 && _serca[0] != 41 && _serca[0] != 42)
                    {
                        break;
                    }

                    if (_serca[a] != 40 && _serca[a] != 41 && _serca[a] != 42)
                    {
                        if(_serca[a - 1] == 41)
                        {
                            _ActualHealth -= 1;
                           
                        }

                        else if (_serca[a - 1] == 42)
                        {
                            _ActualHealth -= 2;
                            
                        }

                        _serca[a - 1] = 0;
                        a = 20;


                        for (int b = 0; b <= 11; b++)
                        {
                            if (_serca[b] != 40 && _serca[b] != 41 && _serca[b] != 42)
                            {
                                if (_serca[b] == 0 && _zeroActiv == true)
                                {
                                    _serca[b - 1] = _serca[b];
                                }

                                if (_serca[b] == 0)
                                {
                                    _zeroActiv = true;
                                }

                                if (_serca[b] != 0)
                                {
                                    _serca[b - 1] = _serca[b];
                                }
                            }
                        }
                        _serca[11] = 0;
                        _zeroActiv = false;
                    }
                }
            }
        }

        if(KindOfHearts == 52)
        {
            for (int i = 0; i < HeartsAmount; i++)
            {
                for (int a = 11; a >= 0; a--)
                {
                    if (_serca[a] == 51 || _serca[a] == 52)
                    {
      
                        if (_serca[a] == 51)
                        {
                            _bonusHearts -= 1;
                            halfSoulHeartActive = true;
                            AmountSoulHeartDeleted += 1;
                        }

                        else if (_serca[a] == 52)
                        {
                            _bonusHearts -= 2;
                            AmountSoulHeartDeleted += 2;
                        }

                        _serca[a] = 0;
                        a = -1;

                        for (int b = 0; b <= 10; b++)
                        {
                            if (_serca[b] == 0)
                            {
                                _serca[b] = _serca[b+1];
                                _serca[b + 1] = 0;
                            }
                        }
                        _serca[11] = 0;
                    }
                }
            }

            if(halfSoulHeartActive)
            {
                Debug.Log("polowka");
                for (int i = 11; i >= 0; i--)
                {

                    if(_serca[i] == 52)
                    {
                        _serca[i] = 51;
                        AmountSoulHeartDeleted++;
                        i = -1;
                    }

                    else if (_serca[i] == 51)
                    {
                        _serca[i] = 0;
                        AmountSoulHeartDeleted++;
                        i = -1;
                    }

                    else if (_serca[i] == 62)
                    {
                        _serca[i] = 61;
                        AmountSoulHeartDeleted++;
                        i = -1;
                    }

                    else if (_serca[i] == 61)
                    {
                        _serca[i] = 0;
                        AmountSoulHeartDeleted++;
                        i = -1;
                    }
                }
                halfSoulHeartActive = false;
            }

            if(AmountSoulHeartDeleted != HeartsAmount * 2)
            {
                int czarneDoDeleta = ((HeartsAmount*2) - AmountSoulHeartDeleted)/2;

                for (int i = 0; i < czarneDoDeleta; i++)
                {
                    for (int a = 11; a >= 0; a--)
                    {
                        if (_serca[a] == 61 || _serca[a] == 62)
                        {

                            if (_serca[a] == 61)
                            {
                                _bonusHearts -= 1;
                            }

                            else if (_serca[a] == 62)
                            {
                                _bonusHearts -= 2;
                            }

                            _serca[a] = 0;
                            a = -1;

                            for (int b = 0; b <= 10; b++)
                            {
                                if (_serca[b] == 0)
                                {
                                    _serca[b] = _serca[b + 1];
                                    _serca[b + 1] = 0;
                                }
                            }
                            _serca[11] = 0;
                        }
                    }
                }
            }
             
            AmountSoulHeartDeleted = 0;
        }
    }  

    private void DeletePercentagesDevilRoom() 
    {
        if (_templates.minusChanceDevilRoom == false)
        {
            if (_templates.minusChanceDevilRoomBoosRoom == true)
            {
                _playerStats.DevilRoomChance = "-" + Mathf.Round((_playerStats.DevilRoomPercentages - (_playerStats.DevilRoomPercentages -1)) * 10.0f) * 0.1f;
                _playerStats.DevilRoomPercentages = 1;
                _playerStats.NewStats();

                _templates.minusChanceDevilRoomBoosRoom = false;
            }
            else
            {
                _playerStats.DevilRoomChance = "-" + Mathf.Round((_playerStats.DevilRoomPercentages - (_playerStats.DevilRoomPercentages / 3)) * 10.0f) * 0.1f;
                _playerStats.DevilRoomPercentages /= 3;
                _playerStats.NewStats();
                _templates.minusChanceDevilRoom = true;
            }
        }
    }

    private void blinking()
    {
        if (transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled == true)
        {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            transform.parent.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
}
