using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector]public float Dmg,Range,MovementSpeed,Tears,ShootSpeed,Luck, DevilRoomPercentages, AngelRoomPercentages;
    [HideInInspector]public string DmgChange, RangeChange, MovementSpeedChange,TearsChange,ShootSpeedChange, DevilRoomChance, AngelRoomChance;


    [SerializeField] private TextMeshProUGUI _movmentTextChange, _dmgTextChange, _rangeTextChange, _tearsTextChange, _shootSpeedTextChange, _devilRoomTextChance, _angelRoomTextChance;
    [SerializeField] private TextMeshProUGUI _movmentText, _dmgText, _rangeText,_tearsText,_shootSpeedText, _devilRoomText, _angelRoomText;

    private string _valueNumber;

    public bool BloodLustActive;
    private float _bloodLustDmg;

    private RoomsPrefabs _templates;

    private void Start()
    {
        _templates = GameObject.FindGameObjectWithTag("rooms").GetComponent<RoomsPrefabs>();


        DmgChange = null;
        RangeChange = null;
        MovementSpeedChange = null;
        TearsChange = null;
        ShootSpeedChange = null;
        DevilRoomChance = null;
        AngelRoomChance = null;


        Dmg = 3.5f;
        Range = 4.5f;
        MovementSpeed = 5f;
        Tears = 0.5f;
        ShootSpeed = 6f;
        DevilRoomPercentages = 0;
        AngelRoomPercentages = 0;

        NewStats();
    }
    public void NewStats()
    {
        if (DevilRoomPercentages <= 1 && _templates.Floor > 0)
        {
            DevilRoomPercentages = 1;
            DevilRoomChance = "-0";
        }


        StartCoroutine(ChangeText());
        Dmg = Mathf.Round(Dmg * 10.0f) * 0.1f;

        DevilRoomPercentages = Mathf.Round(DevilRoomPercentages * 10.0f) * 0.1f;

        _dmgText.text = Dmg.ToString();
        _rangeText.text = Range.ToString();
        _movmentText.text = MovementSpeed.ToString();
        _tearsText.text = Tears.ToString();
        _shootSpeedText.text = ShootSpeed.ToString();
        _devilRoomText.text = DevilRoomPercentages.ToString();
        _angelRoomText.text = AngelRoomPercentages.ToString();


       
    }

    public IEnumerator ChangeText()
    {
        if (DmgChange != null)
        {
            _dmgTextChange.text = DmgChange;
            if (DmgChange[0] == '+')
            {
                _dmgTextChange.color = Color.green;
            }
            else
            {
                _dmgTextChange.color = Color.red;

            }
        }
        if (RangeChange != null)
        {
            _rangeTextChange.text = RangeChange;
            if (RangeChange.Substring(0, 1) == "+")
            {
                _rangeTextChange.color = Color.green;
            }
            else
            {
                _rangeTextChange.color = Color.red;

            }
        }

        if (MovementSpeedChange != null)
        {
            _movmentTextChange.text = MovementSpeedChange;
            if (MovementSpeedChange.Substring(0, 1) == "+")
            {
                _movmentTextChange.color = Color.green;
            }
            else
            {
                _movmentTextChange.color = Color.red;

            }
        }

        if (TearsChange != null)
        {
            _tearsTextChange.text = TearsChange;
            if (TearsChange.Substring(0, 1) == "-")
            {
                _tearsTextChange.color = Color.green;
            }
            else
            {
                _tearsTextChange.color = Color.red;

            }
        }

        if (ShootSpeedChange != null)
        {
            _shootSpeedTextChange.text = ShootSpeedChange;
            if (ShootSpeedChange.Substring(0, 1) == "+")
            {
                _shootSpeedTextChange.color = Color.green;
            }
            else
            {
                _shootSpeedTextChange.color = Color.red;

            }
        }
        if (DevilRoomChance != null)
        {
            _devilRoomTextChance.text = DevilRoomChance;
            if (DevilRoomChance.Substring(0, 1) == "+")
            {
                _devilRoomTextChance.color = Color.green;
            }
            else
            {
                _devilRoomTextChance.color = Color.red;

            }
        }
        if (AngelRoomChance != null)
        {
            _angelRoomTextChance.text = AngelRoomChance;
            if (AngelRoomChance.Substring(0, 1) == "+")
            {
                _angelRoomTextChance.color = Color.green;
            }
            else
            {
                _angelRoomTextChance.color = Color.red;

            }
        }


        yield return new WaitForSeconds(2.5f);
        DmgChange =null;
        RangeChange = null;
        MovementSpeedChange = null;
        TearsChange = null;
        ShootSpeedChange = null;
        DevilRoomChance = null;
        AngelRoomChance = null;

        _dmgTextChange.text = DmgChange;
        _rangeTextChange.text = RangeChange;
        _movmentTextChange.text = MovementSpeedChange;
        _tearsTextChange.text = TearsChange;
        _shootSpeedTextChange.text = ShootSpeedChange;
        _devilRoomTextChance.text = DevilRoomChance;
        _angelRoomTextChance.text = AngelRoomChance;

    }

    public void BloodLust()
    {
        if (_bloodLustDmg<0.6f) {
            _bloodLustDmg += 0.1f;
            DmgChange = "+0.1";
            Dmg += 0.1f;

            ChangeText();
            NewStats();
        }
    }
    public void ResetBloodLust()
    {
        
            Dmg -= _bloodLustDmg;
            DmgChange = "-" + _bloodLustDmg;

            ChangeText();
            NewStats();
            _bloodLustDmg = 0;
        
    }

}
