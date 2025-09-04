using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCube4 : MonoBehaviour
{

    public GameObject minigame;
    public GameObject carro;
    public GameObject camera;
    public GameObject carroinimii;

    private void OnTriggerEnter(Collider other)

    {

        minigame.gameObject.SetActive(true);
        carro.gameObject.SetActive(false);
        camera.gameObject.SetActive(false);
        carroinimii.gameObject.SetActive(false);





    }
}
