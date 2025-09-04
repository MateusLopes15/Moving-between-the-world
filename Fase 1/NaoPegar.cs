using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NaoPegar : MonoBehaviour
{
    bool PodePegar;
    public Slider vidaPlayer;
    public float vidaMaxima = 100;
    public float _dano;


    // Start is called before the first frame update
    void Start()
    {
        vidaPlayer.maxValue = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PodePegar && !PegarItem.Pegueipedra && DhaliaController.chamarDh)
        {

            ControlePlayer.objPesado = true;
            PodePegar = false;
            ControlePlayer.pegandoItem = true;
            FindObjectOfType<DialogueManager>().EndDialogue();

            StartCoroutine("NaoPegaPedra");
            
        }
        if (Input.GetKeyDown(KeyCode.F) && PodePegar && !PegarItem.Pegueipedra && AtivarFase2.afase2)
        {
            ControlePlayer.objPesado = true;
            PodePegar = false;
            ControlePlayer.pegandoItem = true;
            FindObjectOfType<DialogueManager>().EndDialogue();

            StartCoroutine("NaoPegaPedra");

        }

    }
    private void OnTriggerEnter(Collider _pedrinhaa)
    {
        if (_pedrinhaa.gameObject.tag == "Player")
        {
            PodePegar = true;
            Debug.Log("Area para pegar pedra");

        }

    }

    private void OnTriggerExit(Collider _pedrinhaa)
    {
        if (_pedrinhaa.gameObject.tag == "Player")
        {
            PodePegar = false;

        }
    }
    IEnumerator NaoPegaPedra()
    {
        vidaPlayer.value -= _dano;
        yield return new WaitForSeconds(8f);
        Debug.Log("Ops! Pedra errada");
        ControlePlayer.objPesado = false;
        PodePegar = false;
        ControlePlayer.pegandoItem = false;

    }

}
