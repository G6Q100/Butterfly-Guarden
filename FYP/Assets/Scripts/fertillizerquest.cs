using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fertillizerquest : MonoBehaviour
{
    public GameObject fertillizer;

    void Update()
    {
        if (PlayerPrefs.GetInt("quest") != 3)
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            fertillizer.SetActive(false);
        }
        else
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            fertillizer.SetActive(true);
        }
    }

    private void OnMouseOver()
    {
        if (GameObject.Find("InfoText") != null)
        {
            GameObject.Find("InfoText").transform.position = Input.mousePosition + Vector3.up * 50;
            GameObject.Find("LeftClick").GetComponent<Image>().enabled = true;
            GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
            GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoText").GetComponent<Text>().text = "Fertillzer";
        }
    }

    private void OnMouseExit()
    {
        if (GameObject.Find("InfoText") != null)
        {
            GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
            GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoText").GetComponent<Text>().text = "";
        }
    }
}
