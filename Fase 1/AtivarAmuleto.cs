using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtivarAmuleto : MonoBehaviour
{
    public GameObject anel1;
    public GameObject dhaliaf1;
    public GameObject mob1;
    public GameObject mob2;
    public GameObject mob3;
    public GameObject mob4;
    public GameObject DesativarLoc;
    public GameObject ativarLoc;



    public bool pegItem = false;
    public static bool anelF1 = false;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && pegItem)
        {
            ControlePlayer.coletarObj = true;
            StartCoroutine("pegandoItem");

        }
    }
    //Ativar Anel 1 ao tocar no item presente no cenario
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {//Se Objeto com tag Player colidir com o objeto no cenario, ira ativar o item pra usar ...

            pegItem = true;
            DesativarLoc.SetActive(false);

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pegItem = false;
            DesativarLoc.SetActive(true);

        }
    }
    IEnumerator pegandoItem()
    {

        yield return new WaitForSeconds(5.5f);
        anel1.SetActive(true);
        dhaliaf1.SetActive(true);
        mob1.SetActive(true);
        mob2.SetActive(true);
        mob3.SetActive(true);
        mob4.SetActive(true);
        anelF1 = true;
        ControlePlayer.coletarObj = false;
        ativarLoc.SetActive(true);

        this.gameObject.SetActive(false);

    }
}
