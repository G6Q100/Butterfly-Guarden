using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Snow : MonoBehaviour
{
    public int snowType = 1;

    private ParticleSystem particle;

    private GameObject player;

    private void Start()
    {
        particle = GetComponent<ParticleSystem>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        switch (snowType)
        {        
            case 1:
                var mainParticle3 = particle.main;
                var emission3 = particle.emission;
                mainParticle3.startLifetime = 0;
                emission3.rateOverTime = 0;
                break;
            case 2:
                if(SceneManager.GetActiveScene().buildIndex == 3)
                {
                    mainParticle3 = particle.main;
                    emission3 = particle.emission;
                    mainParticle3.startLifetime = 0;
                    emission3.rateOverTime = 0;
                }
                else
                {
                    if (player != null)
                        transform.position = player.transform.position + Vector3.up * 5;
                    else
                        player = GameObject.FindGameObjectWithTag("Player");

                    transform.rotation = Quaternion.Euler(90, 0, 30);
                    var mainParticle1 = particle.main;
                    var emission1 = particle.emission;
                    mainParticle1.startLifetime = 5;
                    mainParticle1.startSpeed = 5;
                    emission1.rateOverTime = 20;
                }
                break;
            case 3:
                if (SceneManager.GetActiveScene().buildIndex == 3)
                {
                    mainParticle3 = particle.main;
                    emission3 = particle.emission;
                    mainParticle3.startLifetime = 0;
                    emission3.rateOverTime = 0;
                }
                else
                {
                    if (player != null)
                        transform.position = player.transform.position + Vector3.up * 5 + Vector3.right * 15;
                    else
                        player = GameObject.FindGameObjectWithTag("Player");

                    transform.rotation = Quaternion.Euler(160, 90, 30);
                    var mainParticle2 = particle.main;
                    var emission2 = particle.emission;
                    mainParticle2.startLifetime = 0.6f;
                    mainParticle2.startSpeed = 50;
                    emission2.rateOverTime = 250;
                }
                break;
        }
    }
}
