using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirtManager : MonoBehaviour
{
    public Dirt[] dirtGroup;
    private float[] dirtGrowthTime;
    private float[] dirtDryTime;
    private float[] currentMax;
    private string[] dirtState;

    private int butterflyChance = 36;

    public bool original = false;

    public int waterLossLv = 0, growthSpeedLv = 0, seedDropChance = 0;

    public static DirtManager instance = null; // original
    private void Awake()
    {
        if (instance == null)      // original check
        {
            instance = this;
        }
        else if (instance != this)      // original check
        {
            Destroy(gameObject);
        }

    }
    void Start()
    {
        dirtGroup = GameObject.Find("Dirt Grid").GetComponentsInChildren<Dirt>();
        dirtGrowthTime = new float[dirtGroup.Length];
        dirtDryTime = new float[dirtGroup.Length];
        currentMax = new float[dirtGroup.Length];
        dirtState = new string[dirtGroup.Length];


        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            for (int i = 0; i < dirtGrowthTime.Length; i++)
            {
                dirtGrowthTime[i] = PlayerPrefs.GetFloat(dirtGroup[i].name + "dirtGrowthTime");
                dirtDryTime[i] = PlayerPrefs.GetFloat(dirtGroup[i].name + "dirtDryTime");
                currentMax[i] = PlayerPrefs.GetFloat(dirtGroup[i].name + "currentMax");
                dirtState[i] = PlayerPrefs.GetString(dirtGroup[i].name + "dirtState");
            }
            butterflyChance = PlayerPrefs.GetInt("butterflyChance");
            waterLossLv = PlayerPrefs.GetInt("waterLossLv");
            growthSpeedLv = PlayerPrefs.GetInt("growthSpeedLv");
            seedDropChance = PlayerPrefs.GetInt("seedDropChance");
        }

        DontDestroyOnLoad(gameObject);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < dirtGrowthTime.Length; i++)
        {
            dirtGrowthTime[i] = dirtGroup[i].growthTime;
            dirtDryTime[i] = dirtGroup[i].dryTime;
            currentMax[i] = dirtGroup[i].currentMax;
            dirtState[i] = dirtGroup[i].state;

            if (TimeController.instance.timeMove > 0 && 
                dirtGroup[i].state != "pollinated" && dirtGroup[i].state != "butterfly" &&
                dirtGroup[i].state != "dead" && dirtGroup[i].state != "hovered" &&
                dirtGroup[i].state != "normal")
            {
                dirtGroup[i].growthTime -= Time.deltaTime * (100 + growthSpeedLv) / 100;
            }

            if (TimeController.instance.timeMove > 0 &&
                dirtGroup[i].state != "normal" && dirtGroup[i].state != "hovered" &&
                dirtGroup[i].state != "dead")
            {
                dirtGroup[i].dryTime -= Time.deltaTime * 50 / (100 + waterLossLv);
            }

            switch (dirtGroup[i].state)
            {
                case "selected":
                    if(dirtGroup[i].growthTime <= 0)
                    {
                        dirtGroup[i].growthTime += Random.Range(128, 228);
                        dirtGroup[i].currentMax = dirtGroup[i].growthTime;
                        dirtGroup[i].state = "growth";
                    }
                    break;
                case "growth":
                    if (dirtGroup[i].growthTime <= 0)
                    {
                        int randomEvent = Random.Range(0, 100);
                        if(randomEvent > butterflyChance)
                        {

                            dirtGroup[i].growthTime += Random.Range(12, 22);
                            dirtGroup[i].currentMax = dirtGroup[i].growthTime;
                            dirtGroup[i].state = "pollinating";
                        }
                        else
                        {

                            dirtGroup[i].growthTime += Random.Range(32, 36);
                            dirtGroup[i].currentMax = dirtGroup[i].growthTime;
                            dirtGroup[i].state = "hatched";
                        }
                    }
                    break;
                case "pollinating":
                    if (dirtGroup[i].growthTime <= 0)
                    {
                        dirtGroup[i].state = "pollinated";
                    }
                    break;
                case "hatched":
                    if (dirtGroup[i].growthTime <= 0)
                    {
                        dirtGroup[i].growthTime += Random.Range(68, 98);
                        dirtGroup[i].currentMax = dirtGroup[i].growthTime;
                        dirtGroup[i].state = "caterpillar";
                    }
                    break;
                case "caterpillar":
                    if (dirtGroup[i].growthTime <= 0)
                    {
                        dirtGroup[i].growthTime += Random.Range(158, 188);
                        dirtGroup[i].currentMax = dirtGroup[i].growthTime;
                        dirtGroup[i].state = "chrysalis";
                    }
                    break;
                case "chrysalis":
                    if (dirtGroup[i].growthTime <= 0)
                    {
                        int rand = Random.Range(0, 100);
                        if (rand > 90)
                            rand = 2;
                        else
                        {
                            rand = Random.Range(0, 6);
                            if (rand == 2)
                                rand = 1;
                        }
                        dirtGroup[i].butterflyType[rand].gameObject.SetActive(true);
                        dirtGroup[i].types = rand;
                       dirtGroup[i].state = "butterfly";
                    }
                    break;
            }

            dirtGrowthTime[i] = dirtGroup[i].growthTime;
            dirtDryTime[i] = dirtGroup[i].dryTime;
            currentMax[i] = dirtGroup[i].currentMax;
            dirtState[i] = dirtGroup[i].state;


            PlayerPrefs.SetFloat(dirtGroup[i].name + "dirtGrowthTime", dirtGrowthTime[i]);
            PlayerPrefs.SetFloat(dirtGroup[i].name + "dirtDryTime", dirtDryTime[i]);
            PlayerPrefs.SetFloat(dirtGroup[i].name + "currentMax", currentMax[i]);
            PlayerPrefs.SetString(dirtGroup[i].name + "dirtState", dirtState[i]);
        }
        PlayerPrefs.SetInt("butterflyChance", butterflyChance);
        PlayerPrefs.SetInt("waterLossLv", waterLossLv);
        PlayerPrefs.SetInt("growthSpeedLv", growthSpeedLv);
        PlayerPrefs.SetInt("seedDropChance", seedDropChance);
    }


    private void OnLevelWasLoaded(int level)
    {
        if (level == SceneManager.GetSceneByName("WinScreen").buildIndex || level == 0)
            Destroy(gameObject);

        if (GameObject.Find("Dirt Grid") != null)
        {
            dirtGroup = GameObject.Find("Dirt Grid").GetComponentsInChildren<Dirt>();
            for (int i = 0; i < dirtGroup.Length; i++)
            {
                dirtGroup[i].growthTime = dirtGrowthTime[i];
                dirtGroup[i].dryTime = dirtDryTime[i];
                dirtGroup[i].currentMax = currentMax[i];
                dirtGroup[i].state = dirtState[i];
            }
        }
        original = true;
    }
}
