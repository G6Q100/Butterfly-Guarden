using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public int exp = 0;
    public int lv = 1, lvUpExp = 50, skillPoint = 0;

    public Text lvText, expText;

    public static LevelSystem instance = null; // original
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
            exp = PlayerPrefs.GetInt("exp");
            lvUpExp = PlayerPrefs.GetInt("lvUpExp");
            lv = PlayerPrefs.GetInt("lv");
            skillPoint = PlayerPrefs.GetInt("skillPoint");
        }

        lvText.text = "Lv: " + lv.ToString();
        expText.text = "Exp: " + exp.ToString();
    }

    private void Update()
    {
       
        if (exp >= lvUpExp)
        {
            exp -= lvUpExp;
            lvUpExp += 5 * lv;
            lv++;
            skillPoint++;

            lvText.text = "Lv: " + lv.ToString();
            expText.text = "Exp: " + exp.ToString();
        }

        PlayerPrefs.SetInt("exp", exp);
        PlayerPrefs.SetInt("lvUpExp", lvUpExp);
        PlayerPrefs.SetInt("lv", lv);
        PlayerPrefs.SetInt("skillPoint", skillPoint);
    }
}
