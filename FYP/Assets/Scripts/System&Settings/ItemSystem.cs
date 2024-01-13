using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSystem : MonoBehaviour
{
    public int seed = 0, fertilizer = 0, fertilizerNum = 10;
    public Text itemText, itemText2, fertilizerNumText;

    public Image item, item2, item3, item4;

    public int itemNum = 1;
    public bool shop = false;

    public GameObject shopUI;

    public static ItemSystem instance = null; // original
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

    private void Start()
    {
        if (PlayerPrefs.GetInt("tutorial") == 1)
        {
            fertilizer = PlayerPrefs.GetInt("fertilizer");
            fertilizerNum = PlayerPrefs.GetInt("fertilizerNum");
            seed = PlayerPrefs.GetInt("seed");
        }

        if (fertilizerNum > 0)
            fertilizerNumText.text = "We still have " + fertilizerNum + " of them to sell today!";
        else
            fertilizerNumText.text = "Thats was the last one for today, you can come back tomorrow for more!";

        itemText.text = "Seed: " + seed.ToString();
        itemText2.text = "Manure: " + fertilizer.ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            itemNum = 1;
            item.color = new Color(1, 1, 1, 1);
            item2.color = new Color(1, 1, 1, 0.5f);
            item3.color = new Color(1, 1, 1, 0.5f);
            item4.color = new Color(1, 1, 1, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            itemNum = 2;
            item.color = new Color(1, 1, 1, 0.5f);
            item2.color = new Color(1, 1, 1, 1);
            item3.color = new Color(1, 1, 1, 0.5f);
            item4.color = new Color(1, 1, 1, 0.5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            itemNum = 3;
            item.color = new Color(1, 1, 1, 0.5f);
            item2.color = new Color(1, 1, 1, 0.5f);
            item3.color = new Color(1, 1, 1, 1);
            item4.color = new Color(1, 1, 1, 0.5f);

        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            itemNum = 4;
            item.color = new Color(1, 1, 1, 0.5f);
            item2.color = new Color(1, 1, 1, 0.5f);
            item3.color = new Color(1, 1, 1, 0.5f);
            item4.color = new Color(1, 1, 1, 1);
        }

        PlayerPrefs.SetInt("fertilizer", fertilizer);
        PlayerPrefs.SetInt("fertilizerNum", fertilizerNum);
        PlayerPrefs.SetInt("seed", seed);
    }

    public void BuyFertilizer()
    {
        if (fertilizerNum > 0)
        {
            fertilizer++;
            fertilizerNum--;
            itemText2.text = "Manure: " + fertilizer.ToString();

            if (fertilizerNum > 0)
                fertilizerNumText.text = "We still have " + fertilizerNum + " of them to sell today!";
            else
                fertilizerNumText.text = "Thats was the last one for today, you can come back tomorrow for more!";
        }
    }

    public void ClosedShop()
    {
        Time.timeScale = 1;
        shopUI.SetActive(false);
        shop = false;
    }
}
