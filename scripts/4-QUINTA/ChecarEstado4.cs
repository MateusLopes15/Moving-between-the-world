using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChecarEstado4 : MonoBehaviour
{

    public bool completou;
    float cronometro;
    public GameObject minigamee;
    public GameObject carroo;
    public GameObject cameraa;

    public GameObject carroinimi;

    MoveObject2D5[] objetos;

    void Start()
    {
        cronometro = 0;
        completou = false;
        objetos = FindObjectsOfType<MoveObject2D5>();
    }

    void Update()
    {
        cronometro += Time.deltaTime;
        if (cronometro >= 0.2f)
        { //5Hz
            cronometro = 0;
            int soma = 0;
            for (int x = 0; x < objetos.Length; x++)
            {
                if (objetos[x].estaConectado)
                {
                    soma++;
                }
            }
            if (soma >= objetos.Length)
            {
                minigamee.gameObject.SetActive(false);
                carroo.gameObject.SetActive(true);
                cameraa.gameObject.SetActive(true);
                carroinimi.gameObject.SetActive(true);
                completou = true;
                Destroy(GetComponent<ChecarEstado4>());
                Destroy(GetComponent<TriggerCube4>());



            }

        }

    }


}

