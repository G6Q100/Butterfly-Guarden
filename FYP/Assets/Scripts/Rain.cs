using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rain : MonoBehaviour
{
    public int rainType = 1;

    private ParticleSystem particle;

    private GameObject player;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        switch (rainType)
        {        
            case 1:
                var emission3 = particle.emission;
                emission3.rateOverTime = 0;
                break;
            case 2:
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    emission3 = particle.emission;
                    emission3.rateOverTime = 0;
                }
                else
                {
                    if (player != null)
                        transform.position = player.transform.position + (Vector3.up * 10);
                    else
                        player = GameObject.FindGameObjectWithTag("Player");

                    transform.rotation = Quaternion.Euler(90, 0, 30);
                    var emission1 = particle.emission;
                    emission1.rateOverTime = 100;
                }
                break;
            case 3:
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    emission3 = particle.emission;
                    emission3.rateOverTime = 0;
                }
                else
                {
                    if (player != null)
                        transform.position = player.transform.position + (Vector3.up * 10);
                    else
                        player = GameObject.FindGameObjectWithTag("Player");

                    transform.rotation = Quaternion.Euler(120, 90, 30);
                    var emission2 = particle.emission;
                    emission2.rateOverTime = 350;
                }
                break;
        }
    }
}
