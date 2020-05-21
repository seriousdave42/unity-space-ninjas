using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddHealCallback : MonoBehaviour
{
    // Use this for initialization
    void Start () 
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => addCallback());
    }
 
    private void addCallback () 
    {
        GameObject playerParty = GameObject.Find("PlayerParty");
        playerParty.GetComponent<SelectUnit>().selectHeal();
    }
}