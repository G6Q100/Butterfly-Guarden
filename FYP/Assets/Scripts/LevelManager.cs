using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public Vector3 spawnPoint;
    public GameObject player;
    public GameObject weatherSystem;
    public GameObject menu;
    public GameObject gameManager;
    public GameObject cutsceneManger;
    public GameObject startCutscene;

    public static LevelManager instance = null; // original

    private void Awake()
    {
        if (instance == null)      // original check
        {
            instance = this;
            GameObject spawnPlayer = Instantiate(player, spawnPoint, Quaternion.identity);

            if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            {
                Destroy(spawnPlayer);
            }

            Instantiate(cutsceneManger, Vector3.zero, Quaternion.identity);
            Instantiate(weatherSystem, spawnPoint, Quaternion.identity);
            Instantiate(menu, spawnPoint, Quaternion.identity);
            Instantiate(gameManager, spawnPoint, Quaternion.identity);
            Instantiate(startCutscene, spawnPoint, Quaternion.identity);
            StartCoroutine(WaitCutscene());
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)      // original check
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitCutscene()
    {
        PlayerController playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        playerController.canMove = -9999;
        yield return new WaitForSeconds(5);
        playerController.canMove = 1;
    }

    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("WinScreen").buildIndex || level == 0)
            Destroy(gameObject);
        else
        {
            GameObject spawnPlayer = Instantiate(player, spawnPoint, Quaternion.identity);

            if (GameObject.FindGameObjectsWithTag("Player").Length > 1)
            {
                Destroy(spawnPlayer);
            }
        }
    }

}
