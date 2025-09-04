using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PegarItem : MonoBehaviour
{
    public static bool Pegueipedra; // Usar para travar ao pegar uma pedra, uma por vez
    bool PodePegar;
    public GameObject peguePedra;


    // Start is called before the first frame update
    void Start()
    {
        GameObject peguePedra = this.gameObject;
        Pegueipedra = false;
        PodePegar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && PodePegar && !Pegueipedra && DhaliaController.chamarDh)
        {
            ControlePlayer.objLeve = true;
            PodePegar = false;
            ControlePlayer.pegandoItem = true;
            StartCoroutine("PegandoPedra");
            FindObjectOfType<DialogueManager>().EndDialogue();

            peguePedra.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.F) && PodePegar && !Pegueipedra && AtivarFase2.afase2)
        {
            ControlePlayer.objLeve = true;
            PodePegar = false;
            ControlePlayer.pegandoItem = true;
            StartCoroutine("PegandoPedra");
            FindObjectOfType<DialogueManager>().EndDialogue();

            peguePedra.SetActive(true);
        }
    }
    private void OnTriggerEnter(Collider pedrin)
    {
        if (pedrin.gameObject.tag == "Player")
        {
            PodePegar = true;
            Debug.Log("Area para pegar pedra");
        }

    }

    private void OnTriggerExit(Collider pedrin)
    {
        if (pedrin.gameObject.tag == "Player")
        {
            PodePegar = false;

        }
    }


    IEnumerator PegandoPedra()
    {

        yield return new WaitForSeconds(5f);
        Pegueipedra = true;
        LancarPedra.numeroDeGranadas++;
        PodePegar = false;
        ControlePlayer.objLeve = false;
        peguePedra.SetActive(false);
        ControlePlayer.pegandoItem = false;
        gameObject.SetActive(false);

    }
}
