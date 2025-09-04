using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossDano : MonoBehaviour
{
    public Slider BarraHpMob;
    public float vidaMaxima = 100;
    public float dano;

    private void Start()
    {
        BarraHpMob.minValue = 0;
        BarraHpMob.maxValue = vidaMaxima;
        BarraHpMob.value = vidaMaxima;
    }
    public void DanoNoPlayer(float dano)
    {
        BarraHpMob.value -= dano;
    }
    private void OnTriggerEnter(Collider other)// aplicar animãção do ataque
    {
        if (other.tag == "Inimigo")
        {
            BarraHpMob.value -= dano;
        }

    }
    public void DanoInimigo(float dano)
    {
        BarraHpMob.value -= dano;

        if (BarraHpMob.value <= BarraHpMob.minValue)
        {
            Destroy(this.gameObject);

        }
    }

}
