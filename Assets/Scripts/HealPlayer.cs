using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayer : MonoBehaviour
{
    public void selectHealTarget (string charName)
    {
        GameObject partyData = GameObject.Find("PlayerParty");
        GameObject healTarget = GameObject.Find(charName);
        partyData.GetComponent<SelectUnit>().healPlayerTarget(healTarget);
    }
}
