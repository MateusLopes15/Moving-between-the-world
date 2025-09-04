using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoPorrete : MonoBehaviour
{
    public float dano;
    public static bool aplicDanoPorrete;


    private void OnTriggerEnter(Collider porreteD)//Adicionar o delay de dano por ataque
    {
        SistemaHpMiniBoss Porrete = porreteD.GetComponent<SistemaHpMiniBoss>();

        if (Porrete != null && ControlePlayer.ataqueBasico == true)
        {
            Porrete.DanoPedra(dano/2);
        }
        if (porreteD.gameObject.tag == "Inimigo" && ControlePlayer.ataqueBasico == true)
        {
        }
    }
}
