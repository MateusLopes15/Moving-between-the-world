using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MensagemPedra : MonoBehaviour
{
    public Text texto;
    private GameObject Jogador;
    [Range(1f,10f)]public float distancia = 3;


    void Start()
    {
        texto.enabled = false;
        Jogador = GameObject.FindWithTag ("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position,Jogador.transform.position) < distancia)
        {
            texto.enabled = true;
        }
        else
        {
            texto.enabled = false;
        }

    }
}
