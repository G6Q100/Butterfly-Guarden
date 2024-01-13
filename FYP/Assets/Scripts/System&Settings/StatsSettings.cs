using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsSettings : MonoBehaviour
{

    public Text lvText, expText, lvUpText, skillPointText,
        waterLostSpeedText, growthSpeedText, seedDropChanceText;

    public Button waterLostSpeedButton, growthSpeedButton, seedDropChanceButton;
    public Image waterLostSpeedImage, growthSpeedImage, seedDropChanceImage;

    // Update is called once per frame
    void Update()
    {
        lvText.text = "Lv: " + LevelSystem.instance.lv.ToString();
        expText.text = "Exp: " + LevelSystem.instance.exp.ToString();
        lvUpText.text = "Next Lv Needs: " + LevelSystem.instance.lvUpExp.ToString();
        skillPointText.text = "Skill Point: " + LevelSystem.instance.skillPoint.ToString();

        waterLostSpeedText.text = "Water Loss Speed:" +
            (100 * 100 / (100 + DirtManager.instance.waterLossLv)).ToString() + "%";

        growthSpeedText.text = "Growth Speed:" +
            (100 + DirtManager.instance.growthSpeedLv).ToString() + "%";

        seedDropChanceText.text = "Seed Drop Chance:" +
            (15 * (100 + DirtManager.instance.seedDropChance) / 100).ToString() + "%";

        if (LevelSystem.instance.skillPoint > 0)
        {
            waterLostSpeedButton.enabled = true;
            growthSpeedButton.enabled = true;
            seedDropChanceButton.enabled = true;

            waterLostSpeedImage.color = new Color(1, 1, 1, 1);
            growthSpeedImage.color = new Color(1, 1, 1, 1);
            seedDropChanceImage.color = new Color(1, 1, 1, 1);
        }
        else
        {
            waterLostSpeedImage.color = new Color(1, 1, 1, 0.5f);
            growthSpeedImage.color = new Color(1, 1, 1, 0.5f);
            seedDropChanceImage.color = new Color(1, 1, 1, 0.5f);

            waterLostSpeedButton.enabled = false;
            growthSpeedButton.enabled = false;
            seedDropChanceButton.enabled = false;
        }
    }

    public void WaterLossSpeed()
    {
        if (LevelSystem.instance.skillPoint > 0)
        {
            LevelSystem.instance.skillPoint--;
            DirtManager.instance.waterLossLv += 1;
        }
    }

    public void GrowthSpeed()
    {
        if (LevelSystem.instance.skillPoint > 0)
        {
            LevelSystem.instance.skillPoint--;
            DirtManager.instance.growthSpeedLv += 1;
        }
    }

    public void SeedDropChance()
    {
        if (LevelSystem.instance.skillPoint > 0)
        {
            LevelSystem.instance.skillPoint--;
            DirtManager.instance.seedDropChance += 5;
        }
    }
}
