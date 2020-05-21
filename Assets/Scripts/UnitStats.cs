using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitStats : MonoBehaviour, IComparable
{
    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject damageTextPrefab;

    [SerializeField]
    private GameObject healingTextPrefab;

    [SerializeField]
    private Vector2 damageTextPosition;

    public float health;
    public float mana;
    public float attack;
    public float magic;
    public float defense;
    public float speed;

    public int nextActTurn;

    private bool dead = false;

    private GameObject statusText;
 
    public void calculateNextActTurn (int currentTurn)
    {
        this.nextActTurn = currentTurn + (int)Math.Ceiling(100.0f / this.speed);
    }
 
    public int CompareTo (object otherStats)
    {
        return nextActTurn.CompareTo(((UnitStats)otherStats).nextActTurn);
    }
 
    public bool isDead ()
    {
        return this.dead;
    }

    public void receiveDamage (float damage) 
    {
        this.health -= damage;
        animator.Play("Hit");
    
        GameObject HUDCanvas = GameObject.Find("HUDCanvas");
        GameObject damageText = Instantiate(this.damageTextPrefab, HUDCanvas.transform) as GameObject;
        damageText.GetComponent<Text>().text = "" + (int)damage;
        damageText.transform.localPosition = this.damageTextPosition;
        damageText.transform.localScale = new Vector2(1.0f, 1.0f);
        this.statusText = damageText;
        Invoke("killText", 0.5f);
    
        if(this.health <= 0)
        {
            this.dead = true;
            this.gameObject.tag = "DeadUnit";
            Destroy(this.statusText);
            Destroy(this.gameObject);
        }
        
        GameObject nextTurn = GameObject.Find("TurnSystem");
        nextTurn.GetComponent<TurnSystem>().nextTurnCall();
    }

    public void receiveHealing (float hp) 
    {
        this.health += hp;
    
        GameObject HUDCanvas = GameObject.Find("HUDCanvas");
        GameObject healingText = Instantiate(this.healingTextPrefab, HUDCanvas.transform) as GameObject;
        healingText.GetComponent<Text>().text = "" + (int)hp;
        healingText.transform.localPosition = this.damageTextPosition;
        healingText.transform.localScale = new Vector2(1.0f, 1.0f);
        this.statusText = healingText;
        Invoke("killText", 0.5f);
    
        if(this.health <= 0)
        {
            this.dead = true;
            this.gameObject.tag = "DeadUnit";
            Destroy(this.statusText);
            Destroy(this.gameObject);
        }
        
        GameObject nextTurn = GameObject.Find("TurnSystem");
        nextTurn.GetComponent<TurnSystem>().nextTurnCall();
    }

    public void killText ()
    {
        Destroy(this.statusText);
    }
}
