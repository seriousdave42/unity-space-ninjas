using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatePlayerMenuItems : MonoBehaviour
{
    [SerializeField]
    private GameObject targetPlayerUnitPrefab;
 
    [SerializeField]
    private Sprite menuItemSprite;
 
    [SerializeField]
    private Vector2 initialPosition, itemDimensions;
 
    [SerializeField]
    private KillEnemy killEnemyScript;
 
    // Use this for initialization
    void OnEnable () 
    {
        GameObject playerUnitsMenu = GameObject.Find("PlayerUnitsMenu");
 
        GameObject[] existingItems = GameObject.FindGameObjectsWithTag("TargetPlayerUnit");
        Vector2 nextPosition = new Vector2(this.initialPosition.x + (existingItems.Length * this.itemDimensions.x), this.initialPosition.y);
 
        GameObject targetPlayerUnit = Instantiate(this.targetPlayerUnitPrefab, playerUnitsMenu.transform) as GameObject;
        targetPlayerUnit.name = "Target" + this.gameObject.name;
        targetPlayerUnit.transform.localPosition = nextPosition;
        targetPlayerUnit.transform.localScale = new Vector2(0.7f, 0.7f);
        targetPlayerUnit.GetComponent<Button>().onClick.AddListener(() => selectPlayerTarget());
        targetPlayerUnit.GetComponent<Image>().sprite = this.menuItemSprite;
 
        killEnemyScript.menuItem = targetPlayerUnit;
    }
 
    public void selectPlayerTarget () 
    {
        GameObject partyData = GameObject.Find("PlayerParty");
        partyData.GetComponent<SelectUnit>().healPlayerTarget(this.gameObject);
    }
}