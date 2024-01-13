using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseController : MonoBehaviour
{
    private Camera mainCamera;

    public GameObject waterParticle, fertilizeParticle;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
       
            if (Physics.Raycast(ray, out hit, 100))
            { 
                if (hit.transform.tag == "Dirt")
                {
                    SelectDirt(hit.transform.gameObject);
                }
                if (hit.transform.tag == "Seed")
                {
                    ItemSystem.instance.seed++;
                    ItemSystem.instance.itemText.text = "Seed: " + ItemSystem.instance.seed.ToString();
                    GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("InfoText").GetComponent<Text>().text = "";
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.tag == "fertillizer")
                {
                    GameObject.Find("LeftClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClick").GetComponent<Image>().enabled = false;
                    GameObject.Find("LeftClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("RightClickDown").GetComponent<Image>().enabled = false;
                    GameObject.Find("InfoText").GetComponent<Text>().text = "";
                    EventSettings.instance.questNameText.text = "Disappeared fertilizer ";
                    EventSettings.instance.questText.text = "Give trader the fertillizer.";
                    EventSettings.instance.quest = 4;
                    PlayerPrefs.SetInt("quest", EventSettings.instance.quest);
                    Destroy(hit.transform.gameObject);
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit2;
            Ray ray2 = mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray2, out hit2, 100))
            {
                if (hit2.transform.tag == "Dirt" && ItemSystem.instance.itemNum == 4)
                {
                    DeletedPlant(hit2.transform.gameObject);
                }
            }
        }
    }

    private void SelectDirt(GameObject dirt)
    {
        if (dirt.GetComponent<Dirt>().state == "hovered" && ItemSystem.instance.seed > 0 && ItemSystem.instance.itemNum == 2)
        {
            dirt.GetComponent<Dirt>().growthTime = Random.Range(238, 338);
            dirt.GetComponent<Dirt>().currentMax = dirt.GetComponent<Dirt>().growthTime;
            dirt.GetComponent<Dirt>().dryTime = Random.Range(100, 120);
            dirt.GetComponent<Dirt>().state = "selected";
            ItemSystem.instance.seed--;
            ItemSystem.instance.itemText.text = "Seed: " + ItemSystem.instance.seed.ToString();
        }
        else if (dirt.GetComponent<Dirt>().state == "pollinated" ||
            dirt.GetComponent<Dirt>().state == "butterfly")
        {
            dirt.GetComponent<Dirt>().pollinatingParticle.SetActive(false);
            dirt.GetComponent<Dirt>().pollinatedParticle.SetActive(false);
            dirt.GetComponent<Dirt>().hatchedParticle.SetActive(false);
            dirt.GetComponent<Dirt>().caterpillarParticle.SetActive(false);
            dirt.GetComponent<Dirt>().chrysalisParticle.SetActive(false);
            dirt.GetComponent<Dirt>().butterflyParticle.SetActive(false);
            if (dirt.GetComponent<Dirt>().state == "pollinated")
            {
                LevelSystem.instance.exp += 20;

                int randomEvent = Random.Range(1, 100);

                if (randomEvent <= 15 * (100 + DirtManager.instance.seedDropChance) / 100)
                {
                    ItemSystem.instance.seed++;
                    ItemSystem.instance.itemText.text = "Seed: " + ItemSystem.instance.seed.ToString();
                }
            }
            else if(dirt.GetComponent<Dirt>().state == "butterfly")
            {
                LevelSystem.instance.exp += 80;

                switch (dirt.GetComponent<Dirt>().types)
                {
                    case 0:
                        InformationSettings.instance.butterflyUnlock = true;
                        break;
                    case 1:
                        InformationSettings.instance.butterflyUnlock2 = true;
                        break;
                    case 2:
                        InformationSettings.instance.butterflyUnlock3 = true;
                        break;
                    case 3:
                        InformationSettings.instance.butterflyUnlock4 = true;
                        break;
                    case 4:
                        InformationSettings.instance.butterflyUnlock5 = true;
                        break;
                    case 5:
                        InformationSettings.instance.butterflyUnlock6 = true;
                        break;
                    default:
                        break;
                }

                if (dirt.GetComponent<Dirt>().butterflyParticle.GetComponent<MeshRenderer>() != null)
                    dirt.GetComponent<Dirt>().butterflyType[dirt.GetComponent<Dirt>().types].SetActive(false);
            }

            LevelSystem.instance.lvText.text = "Lv: " + LevelSystem.instance.lv.ToString();
            LevelSystem.instance.expText.text = "Exp: " + LevelSystem.instance.exp.ToString();

            if (dirt.GetComponent<Dirt>().growthTime <= 0)
            {
                dirt.GetComponent<Dirt>().growthTime = Random.Range(128, 228);
                dirt.GetComponent<Dirt>().currentMax = dirt.GetComponent<Dirt>().growthTime;
                dirt.GetComponent<Dirt>().state = "growth";

            }
        }
        else if(dirt.GetComponent<Dirt>().state != "dead" && 
            dirt.GetComponent<Dirt>().state != "normal" &&
            dirt.GetComponent<Dirt>().state != "hovered" && ItemSystem.instance.itemNum == 1)
        {
            dirt.GetComponent<Dirt>().dryTime += 30;
            Instantiate(waterParticle, dirt.transform.position, Quaternion.identity);
        }
        else if (dirt.GetComponent<Dirt>().state != "dead" &&
            dirt.GetComponent<Dirt>().state != "normal" &&
            dirt.GetComponent<Dirt>().state != "hovered" &&
            dirt.GetComponent<Dirt>().growthTime > 0 &&
            ItemSystem.instance.fertilizer > 0 && ItemSystem.instance.itemNum == 3)
        {
            ItemSystem.instance.fertilizer--;
            ItemSystem.instance.itemText2.text = "Manure: " + ItemSystem.instance.fertilizer.ToString();
            dirt.GetComponent<Dirt>().growthTime -= 30;
            Instantiate(fertilizeParticle, dirt.transform.position, Quaternion.identity);
        }
    }

    private void DeletedPlant(GameObject dirt)
    {
        dirt.GetComponent<Dirt>().pollinatingParticle.SetActive(false);
        dirt.GetComponent<Dirt>().pollinatedParticle.SetActive(false);
        dirt.GetComponent<Dirt>().hatchedParticle.SetActive(false);
        dirt.GetComponent<Dirt>().caterpillarParticle.SetActive(false);
        dirt.GetComponent<Dirt>().chrysalisParticle.SetActive(false);
        dirt.GetComponent<Dirt>().butterflyParticle.SetActive(false);
        dirt.GetComponent<Dirt>().state = "hovered";
        dirt.GetComponent<Dirt>().dryTime = Random.Range(100, 120);
    }
}
