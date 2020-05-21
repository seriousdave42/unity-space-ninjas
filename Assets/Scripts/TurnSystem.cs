using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnSystem : MonoBehaviour
{
    private List<UnitStats> unitsStats;

    private GameObject playerParty;
 
    [SerializeField]
    private GameObject actionsMenu, enemyUnitsMenu, playerUnitsMenu;
 
    void Start ()
    {
        this.playerParty = GameObject.Find("PlayerParty");

        unitsStats = new List<UnitStats>();
        GameObject[] playerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
        
        foreach(GameObject playerUnit in playerUnits) 
        {
            UnitStats currentUnitStats = playerUnit.GetComponent<UnitStats>();
            currentUnitStats.calculateNextActTurn(0);
            unitsStats.Add (currentUnitStats);
        }
 
        GameObject[] enemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
 
        foreach(GameObject enemyUnit in enemyUnits) 
        {
            UnitStats currentUnitStats = enemyUnit.GetComponent<UnitStats>();
            currentUnitStats.calculateNextActTurn(0);
            unitsStats.Add(currentUnitStats);
        }
 
        unitsStats.Sort();
 
        this.actionsMenu.SetActive(false);
        this.enemyUnitsMenu.SetActive(false);
        this.playerUnitsMenu.SetActive(false);
 
        this.nextTurn();
    }

    public void nextTurnCall()
    {
        Invoke("nextTurn", 1);
    }
 
    public void nextTurn () 
    {
        GameObject[] remainingEnemyUnits = GameObject.FindGameObjectsWithTag("EnemyUnit");
        GameObject[] remainingPlayerUnits = GameObject.FindGameObjectsWithTag("PlayerUnit");
    
        if(remainingEnemyUnits.Length == 0) 
        {
            foreach(GameObject character in remainingPlayerUnits)
            {
                UnitStats afterBattleStats = character.GetComponent<UnitStats>();
                afterBattleStats.health = 100;
                afterBattleStats.mana = 100;
            }
            SceneManager.LoadScene("Space");
        }

        if(remainingPlayerUnits.Length == 0) 
        {
            SceneManager.LoadScene("Title");
        }

        UnitStats currentUnitStats = unitsStats [0];
        unitsStats.Remove(currentUnitStats);
 
        if(!currentUnitStats.isDead()) 
        {
            GameObject currentUnit = currentUnitStats.gameObject;
 
            currentUnitStats.calculateNextActTurn(currentUnitStats.nextActTurn);
            unitsStats.Add(currentUnitStats);
            unitsStats.Sort();
 
            if(currentUnit.tag == "PlayerUnit")
            {
                this.playerParty.GetComponent<SelectUnit>().selectCurrentUnit(currentUnit.gameObject);
            } 
            else
            {
                currentUnit.GetComponent<EnemyUnitAction>().act();
            }
        } 
        else 
        {
            this.nextTurn();
        }
    }
}