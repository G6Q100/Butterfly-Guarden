using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Seed : MonoBehaviour
{
    public Material[] material;

    private void OnMouseOver()
    {
        if (GetComponent<Renderer>().material != material[1])
        {
            GetComponent<Renderer>().material = material[1];
        }

        if (GameObject.Find("InfoText") != null)
        {
            GameObject.Find("InfoText").transform.position = Input.mousePosition + Vector3.up * 50;
            GameObject.Find("LeftClick").GetComponent<Image>().enabled = true;
            GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
            GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoText").GetComponent<Text>().text = "Seed \n Pick Up:";
        }
    }

    private void OnMouseExit()
    {
        if (GetComponent<Renderer>().material != material[0])
        {
            GetComponent<Renderer>().material = material[0];
        }
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
