using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dirt : MonoBehaviour
{
    public GameObject pollinatedParticle, pollinatingParticle,
        hatchedParticle, caterpillarParticle, chrysalisParticle, butterflyParticle,
        dryParticle, overHydratedParticle;

    public Material[] material;

    public string state = "normal";

    public float growthTime, dryTime;

    public Slider plantText, plantGrowText;
    bool isPlantText = false;

    public GameObject[] butterflyType;
    public int types = 1;
    public float currentMax;

    private void FixedUpdate()
    {

        if (!isPlantText)
        {
            if(GameObject.Find("PlantText") != null)
            {
                plantText = GameObject.Find("PlantText").GetComponent<Slider>();
                
                foreach (Image silder in plantText.GetComponentsInChildren<Image>())
                    silder.enabled = false;

                plantGrowText = GameObject.Find("PlantGrowText").GetComponent<Slider>();

                foreach (Image silder in plantGrowText.GetComponentsInChildren<Image>())
                    silder.enabled = false;

                isPlantText = true;
            }
        }

        switch (state)
        {
            case "normal":
                if (GetComponent<Renderer>().material != material[0])
                    GetComponent<Renderer>().material = material[0];
                break;
            case "hovered":
                if (GetComponent<Renderer>().material != material[1])
                    GetComponent<Renderer>().material = material[1];
                break;
            case "selected":
                if (GetComponent<Renderer>().material != material[2])
                    GetComponent<Renderer>().material = material[2];
                break;
            case "growth":
                if (GetComponent<Renderer>().material != material[3])
                    GetComponent<Renderer>().material = material[3];
                break;
            case "pollinating":
                if (GetComponent<Renderer>().material != material[3])
                    GetComponent<Renderer>().material = material[3];
                if (!pollinatingParticle.activeInHierarchy)
                {
                    pollinatingParticle.SetActive(true);
                    pollinatedParticle.SetActive(false);
                    hatchedParticle.SetActive(false);
                    caterpillarParticle.SetActive(false);
                    chrysalisParticle.SetActive(false);
                    butterflyParticle.SetActive(false);
                }
                break;
            case "pollinated":
                if (GetComponent<Renderer>().material != material[3])
                    GetComponent<Renderer>().material = material[3];
                if (!pollinatedParticle.activeInHierarchy)
                {
                    pollinatingParticle.SetActive(false);
                    pollinatedParticle.SetActive(true);
                    hatchedParticle.SetActive(false);
                    caterpillarParticle.SetActive(false);
                    chrysalisParticle.SetActive(false);
                    butterflyParticle.SetActive(false);
                }
                break;
            case "hatched":
                if (GetComponent<Renderer>().material != material[3])
                    GetComponent<Renderer>().material = material[3];
                if (!hatchedParticle.activeInHierarchy)
                {
                    pollinatingParticle.SetActive(false);
                    pollinatedParticle.SetActive(false);
                    hatchedParticle.SetActive(true);
                    caterpillarParticle.SetActive(false);
                    chrysalisParticle.SetActive(false);
                    butterflyParticle.SetActive(false);
                }
                break;
            case "caterpillar":
                if (GetComponent<Renderer>().material != material[3])
                    GetComponent<Renderer>().material = material[3];
                if (!caterpillarParticle.activeInHierarchy)
                {
                    pollinatingParticle.SetActive(false);
                    pollinatedParticle.SetActive(false);
                    hatchedParticle.SetActive(false);
                    caterpillarParticle.SetActive(true);
                    chrysalisParticle.SetActive(false);
                    butterflyParticle.SetActive(false);
                }
                break;
            case "chrysalis":
                if (GetComponent<Renderer>().material != material[3])
                    GetComponent<Renderer>().material = material[3];
                if (!chrysalisParticle.activeInHierarchy)
                {
                    pollinatingParticle.SetActive(false);
                    pollinatedParticle.SetActive(false);
                    hatchedParticle.SetActive(false);
                    caterpillarParticle.SetActive(false);
                    chrysalisParticle.SetActive(true);
                    butterflyParticle.SetActive(false);
                }
                break;
            case "butterfly":
                if (GetComponent<Renderer>().material != material[3])
                    GetComponent<Renderer>().material = material[3];
                if(!butterflyParticle.activeInHierarchy)
                {
                    pollinatingParticle.SetActive(false);
                    pollinatedParticle.SetActive(false);
                    hatchedParticle.SetActive(false);
                    caterpillarParticle.SetActive(false);
                    chrysalisParticle.SetActive(false);
                    butterflyParticle.SetActive(true);
                }
                break;
            case "dead":
                if (GetComponent<Renderer>().material != material[4])
                    GetComponent<Renderer>().material = material[4];
                break;
        }

        if(dryTime < 0 || dryTime > 220)
        {
            if(!dryParticle.activeInHierarchy)
                dryParticle.SetActive(true);

            if (overHydratedParticle.activeInHierarchy)
                overHydratedParticle.SetActive(false);

            state = "dead";
        }
        else if (dryTime < 50)
        {
            if (!dryParticle.activeInHierarchy)
                dryParticle.SetActive(true);

            if (overHydratedParticle.activeInHierarchy)
                overHydratedParticle.SetActive(false);
        }
        else if (dryTime > 150)
        {
            if (dryParticle.activeInHierarchy)
                dryParticle.SetActive(false);

            if (!overHydratedParticle.activeInHierarchy)
                overHydratedParticle.SetActive(true);
        }
        else
        {
            if (dryParticle.activeInHierarchy)
                dryParticle.SetActive(false);

            if (overHydratedParticle.activeInHierarchy)
                overHydratedParticle.SetActive(false);
        }
    }

    private void OnMouseOver()
    {
        if(state == "normal")
        {
            state = "hovered";
        }    
        else if(state != "hovered" && state != "dead")
        {
            if (GetComponent<Renderer>().material != material[1])
                GetComponent<Renderer>().material = material[1];
        }

        if (GameObject.Find("InfoText") != null && Time.timeScale != 0)
        {
            GameObject.Find("InfoText").transform.position = Input.mousePosition + Vector3.up * 80;
            switch (ItemSystem.instance.itemNum)
            {
                case 1:
                    GameObject.Find("InfoText").GetComponent<Text>().text = "Watering: ";
                    GameObject.Find("LeftClick").GetComponent<Image>().enabled = true;
                    GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                    break;
                case 2:
                    GameObject.Find("InfoText").GetComponent<Text>().text = "Plant Seed: ";
                    GameObject.Find("LeftClick").GetComponent<Image>().enabled = true;
                    GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                    break;
                case 3:
                    GameObject.Find("InfoText").GetComponent<Text>().text = "Add Fertillizer: ";
                    GameObject.Find("LeftClick").GetComponent<Image>().enabled = true;
                    GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                    break;
                case 4:
                    GameObject.Find("InfoText").GetComponent<Text>().text = "Remove Plant: ";
                    GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClick").GetComponent<Image>().enabled = true;
                    GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                    break;
            }
        }

        if (GameObject.Find("PlantText") != null && Time.timeScale != 0 && state != "hovered" && state != "normal")
        {
            foreach (Image silder in plantText.GetComponentsInChildren<Image>())
                silder.enabled = true;
            plantText.gameObject.transform.position = Input.mousePosition + Vector3.up * 50;
            if (dryTime < 0 || dryTime > 220)
            {
                plantText.value = 100;
                plantText.gameObject.transform.Find("Fill Area").
                    Find("Fill").GetComponent<Image>().color = Color.red;
            }
            else
            {
                plantText.value = dryTime / 2.2f;
                plantText.gameObject.transform.Find("Fill Area").
                    Find("Fill").GetComponent<Image>().color = Color.blue;
            }


            foreach (Image silder in plantGrowText.GetComponentsInChildren<Image>())
                silder.enabled = true;
            plantGrowText.gameObject.transform.position = Input.mousePosition + Vector3.up * 10;
            if (growthTime < 0 || growthTime > currentMax)
            {
                plantGrowText.value = 100;
                plantGrowText.gameObject.transform.Find("Fill Area").
                    Find("Fill").GetComponent<Image>().color = Color.green;
            }
            else
            {
                plantGrowText.value = (currentMax - growthTime) / currentMax * 100;
                plantGrowText.gameObject.transform.Find("Fill Area").
                    Find("Fill").GetComponent<Image>().color = Color.green;
            }
        }
        else if(GameObject.Find("PlantText") != null)
        {
            foreach (Image silder in plantText.GetComponentsInChildren<Image>())
                silder.enabled = false;
            foreach (Image silder in plantGrowText.GetComponentsInChildren<Image>())
                silder.enabled = false;
        }
    }

    private void OnMouseExit()
    {
        if (state == "hovered")
        {
            state = "normal";
        }
        else if (state != "selected" && state != "normal" && state != "dead")
        {
            if (GetComponent<Renderer>().material != material[1])
                GetComponent<Renderer>().material = material[1];
        }
        
        if (GameObject.Find("InfoText") != null)
        {
            GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
            GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
            GameObject.Find("InfoText").GetComponent<Text>().text = "";
        }
        if (GameObject.Find("PlantText") != null)
        {
            foreach (Image silder in plantText.GetComponentsInChildren<Image>())
                silder.enabled = false;
        }
        if (GameObject.Find("PlantGrowText") != null)
        {
            foreach (Image silder in plantGrowText.GetComponentsInChildren<Image>())
                silder.enabled = false;
        }
    }
}
