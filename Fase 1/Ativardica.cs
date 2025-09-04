using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ativardica : MonoBehaviour
{



    public bool podePegarFLor = false;
    public static bool pegueiFlor = false;
    void Start()
    {
        pegueiFlor = false;
        podePegarFLor = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && podePegarFLor)
        {

            ControlePlayer.coletarObj = true;
            StartCoroutine("pegandoFlor");
            pegueiFlor = true;

        }
    }

    private void OnTriggerEnter(Collider other2)
    {
        if (other2.gameObject.tag == "Player")
        {//Se Objeto com tag Player colidir com o objeto no cenario, ira ativar o item pra usar ...

            podePegarFLor = true;

        }
    }
    IEnumerator pegandoFlor()
    {
        
        yield return new WaitForSeconds(3f);
        pegueiFlor = false;
        this.gameObject.SetActive(false);
        ControlePlayer.coletarObj = false;
        pegueiFlor = false;

    }
}
