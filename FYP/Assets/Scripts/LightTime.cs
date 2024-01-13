using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTime : MonoBehaviour
{
    public int lightType = 1;

    private Light lt; 

    private void Start()
    {
        lt = GetComponent<Light>();
    }

    private void Update()
    {
        switch (lightType)
        {
            case 1:
                transform.rotation = Quaternion.Euler(50, -30, 0);
                lt.color = Color.white;
                break;
            case 2:
                transform.rotation = Quaternion.Euler(150, -30, 0);
                lt.color = new Vector4(1, 0.6f, 0, 1);
                break;
            case 3:
                transform.rotation = Quaternion.Euler(100, -30, 0);
                lt.color = new Vector4(0.5f, 0.5f, 1, 1);
                break;
        }
    }
}
