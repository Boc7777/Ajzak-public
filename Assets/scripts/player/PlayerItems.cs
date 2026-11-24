using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerItems : MonoBehaviour
{
    [HideInInspector] public bool PoisonActive, BigActive, PiercingActive, SpectralActive, SpiderActive, CricketsBodyActive, AntiGravityActive, BrimstoneActive, MomsKnife, CursedEye, LaserActive, DoubleShoot,IpecacActive,Fly,RubberCementActive,ProptosisActive,DrFetusActive,Technology2,BloodMartyrActive,Habit,HolyMantleActive,MawOfTheVoidActive, ChocolateMilkActive, DeathsTouchActive;
    [HideInInspector] public bool SuccubusActive,MultidimensionalBaby,SpearofDestiny;
    [SerializeField] public Sprite DeathsTouchSprite;




    public void ActiveItems(GameObject _tears)
    {

        _tears.GetComponent<arrows>().Poison = PoisonActive;
        _tears.GetComponent<arrows>().Piercing = PiercingActive;
        _tears.GetComponent<arrows>().Spectral = SpectralActive;
        _tears.GetComponent<arrows>().Spider = SpiderActive;
        _tears.GetComponent<arrows>().CricketsBody = CricketsBodyActive;
        _tears.GetComponent<arrows>().Ipecac = IpecacActive;
        _tears.GetComponent<arrows>().RubberCement = RubberCementActive;
        _tears.GetComponent<arrows>().Proptosis = ProptosisActive;
        _tears.GetComponent<arrows>().DrFetus = DrFetusActive;
        _tears.GetComponent<arrows>().BloodMartyr = BloodMartyrActive;
        _tears.GetComponent<arrows>().DeathsTouch = DeathsTouchActive;

        
        if (BigActive == true)
        {
            _tears.transform.localScale = new Vector3(2f, 2f, 0);
        }
        if (PoisonActive == true)
        {
            _tears.GetComponent<Renderer>().material.color = Color.green;

        }
        if (AntiGravityActive)
        {
            _tears.GetComponent<arrows>().StopMove = true;
        }
        if (BloodMartyrActive)
        {
            _tears.GetComponent<arrows>().Scale = transform.localScale.x * 1.3f;

            _tears.transform.localScale *= 1.3f;
            gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
        if (DeathsTouchActive && !DrFetusActive && !IpecacActive)
        {
            _tears.GetComponent<SpriteRenderer>().sprite = DeathsTouchSprite;
        }

    }

}