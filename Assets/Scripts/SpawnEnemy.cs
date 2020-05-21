using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyEncounterPrefab;

    [SerializeField]
    private string sceneName;
 
    private bool spawning = false;
 
    void Start () 
    {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
 
    private void OnSceneLoaded (Scene scene, LoadSceneMode mode) 
    {
        if(scene.name != "Title" && scene.name != "Space") 
        {
            if(this.spawning) 
            {
                Instantiate(enemyEncounterPrefab);
            }
 
            SceneManager.sceneLoaded -= OnSceneLoaded;
            Destroy(this.gameObject);
        }
    }
    
    void OnTriggerEnter2D (Collider2D other) 
    {
        if(other.gameObject.tag == "Player") 
        {
            this.spawning = true;
            SceneManager.LoadScene(sceneName);
        }
    }
}