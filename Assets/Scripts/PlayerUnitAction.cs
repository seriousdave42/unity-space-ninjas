using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUnitAction : MonoBehaviour
{
    [SerializeField]
    private GameObject physicalAttack;
 
    [SerializeField]
    private GameObject magicalAttack;

    [SerializeField]
    private GameObject healSkill;
 
    private GameObject currentAttack;

    [SerializeField]
    private Sprite faceSprite;
 
    void Awake () 
    {
        this.physicalAttack = Instantiate(this.physicalAttack, this.transform) as GameObject;
        this.magicalAttack = Instantiate(this.magicalAttack, this.transform) as GameObject;
        this.healSkill = Instantiate(this.healSkill, this.transform) as GameObject;
 
        this.physicalAttack.GetComponent<AttackTarget>().owner = this.gameObject;
        this.magicalAttack.GetComponent<AttackTarget>().owner = this.gameObject;
        this.healSkill.GetComponent<HealTarget>().owner = this.gameObject;
 
        this.currentAttack = this.physicalAttack;
    }
 
    public void act (GameObject target) 
    {
        this.currentAttack.GetComponent<AttackTarget>().hit(target);
    }

    public void heal (GameObject target) 
    {
        this.healSkill.GetComponent<HealTarget>().heal(target);
    }

    public void selectAttack (bool physical)
    {
        this.currentAttack = (physical) ? this.physicalAttack : this.magicalAttack;
    }

    public void updateHUD()
    {
        GameObject playerUnitFace = GameObject.Find("PlayerUnitFace") as GameObject;
        playerUnitFace.GetComponent<Image>().sprite = this.faceSprite;
 
        GameObject playerUnitHealthBar = GameObject.Find("PlayerUnitHealthBar") as GameObject;
        playerUnitHealthBar.GetComponent<ShowUnitHealth>().changeUnit(this.gameObject);
 
        GameObject playerUnitManaBar = GameObject.Find("PlayerUnitManaBar") as GameObject;
        playerUnitManaBar.GetComponent<ShowUnitMana>().changeUnit(this.gameObject);
    }
}