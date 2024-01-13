using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationSettings : MonoBehaviour
{
    public Button[] butterflyNoButton;
    public GameObject[] UnknownNoButton;
    public Image[] butterflyNoImage;
    public Text[] butterflyNoText;

    public Sprite buttonIcon;

    int butterflyUn1, butterflyUn2, butterflyUn3, butterflyUn4, butterflyUn5, butterflyUn6;

    public bool butterflyUnlock = false,
        butterflyUnlock2 = false,
        butterflyUnlock3 = false,
        butterflyUnlock4 = false,
        butterflyUnlock5 = false,
        butterflyUnlock6 = false;

    public static InformationSettings instance = null; // original

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

    void Update()
    {
        if ((butterflyUnlock == true && butterflyNoButton[1].enabled == false) ||
            PlayerPrefs.GetInt(butterflyNoButton[1].name) == 1)
        {
            PlayerPrefs.SetInt(butterflyNoButton[1].name, 1);
            UnknownNoButton[1].SetActive(false);
            butterflyNoText[1].text = "1";
            butterflyNoButton[1].enabled = true;
        }
        if ((butterflyUnlock2 == true && butterflyNoButton[2].enabled == false) ||
            PlayerPrefs.GetInt(butterflyNoButton[2].name) == 1)
        {
            PlayerPrefs.SetInt(butterflyNoButton[2].name, 1);
            UnknownNoButton[2].SetActive(false);
            butterflyNoText[2].text = "2";
            butterflyNoButton[2].enabled = true;
        }
        if ((butterflyUnlock3 == true && butterflyNoButton[3].enabled == false) ||
            PlayerPrefs.GetInt(butterflyNoButton[3].name) == 1)
        {
            PlayerPrefs.SetInt(butterflyNoButton[3].name, 1);
            UnknownNoButton[3].SetActive(false);
            butterflyNoText[3].text = "3";
            butterflyNoButton[3].enabled = true;
        }
        if ((butterflyUnlock4 == true && butterflyNoButton[4].enabled == false) ||
            PlayerPrefs.GetInt(butterflyNoButton[4].name) == 1)
        {
            PlayerPrefs.SetInt(butterflyNoButton[4].name, 1);
            UnknownNoButton[4].SetActive(false);
            butterflyNoText[4].text = "4";
            butterflyNoButton[4].enabled = true;
        }
        if ((butterflyUnlock5 == true && butterflyNoButton[5].enabled == false) ||
            PlayerPrefs.GetInt(butterflyNoButton[5].name) == 1)
        {
            PlayerPrefs.SetInt(butterflyNoButton[5].name, 1);
            UnknownNoButton[5].SetActive(false);
            butterflyNoText[5].text = "5";
            butterflyNoButton[5].enabled = true;
        }
        if ((butterflyUnlock6 == true && butterflyNoButton[6].enabled == false) ||
            PlayerPrefs.GetInt(butterflyNoButton[6].name) == 1)
        {
            PlayerPrefs.SetInt(butterflyNoButton[6].name, 1);
            UnknownNoButton[6].SetActive(false);
            butterflyNoText[6].text = "6";
            butterflyNoButton[6].enabled = true;
        }
    }
}
