using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MobDanoHpPlayer : MonoBehaviour
{

    public bool atk;


    public float tempoDeAtk;


    private void Start()
    {

        atk = true;

    }

    private void Update()
    {
        if (VilaoController.ataca == true)
        {
            Collider[] colisor = Physics.OverlapSphere(transform.position, 0.5f);
            foreach (Collider colisor_ in colisor){
                if (colisor_.tag == "Player")
                {
                    ControlePlayer p = colisor_.GetComponent<ControlePlayer>();
                    if(p != null && atk == true)
                    {
                        p.vidaPlayer.value -= VilaoController.danoAtkMob;
                        atk = false;
                        StartCoroutine("TempAtk");
                    }
                }
                
            }
        }
    }
    //public void DanoNoPlayer(float dano)
    //{
    //    vidaPlayer.value -= VilaoController.danoPorret;
    //}
    //private void OnTriggerEnter(Collider PorreteAtaque)
    //{

    //    if (PorreteAtaque.tag == "Player" && atk == true && VilaoController.ataca == true)
    //    {
    //        atk = false;
    //        vidaPlayer.value -= danoAtkMob;
    //        StartCoroutine("TempAtk");
    //    }

    //}

    //public void DanoInimigo(float dano)
    //{
    //    vidaPlayer.value -= dano;

    //    if (vidaPlayer.value <= vidaPlayer.minValue)
    //    {
    //        Destroy(this.gameObject);

    //    }
    //}
    IEnumerator TempAtk()
    {
        yield return new WaitForSeconds(1.5f);
        atk = true;
    }
}
