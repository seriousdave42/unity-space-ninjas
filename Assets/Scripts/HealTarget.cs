using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealTarget : MonoBehaviour
{
    public GameObject owner;
 
    [SerializeField]
    private float manaCost;
  
    public void heal (GameObject target) 
    {
        UnitStats ownerStats = this.owner.GetComponent<UnitStats>();
        UnitStats targetStats = target.GetComponent<UnitStats>();
 
        if(ownerStats.mana >= this.manaCost) 
        {
            float hp = Math.Min(ownerStats.magic, 100-targetStats.health);
 
            targetStats.receiveHealing(hp);
 
            ownerStats.mana -= this.manaCost;
        }
    }
}