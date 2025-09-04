using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DanoemBoss : MonoBehaviour
{
    public string tagObjects = "Inimigo";
    public Slider vidaPlayer;
    public float vidaMaxima = 100;
    public float dano;

    private void Start()
    {
        vidaPlayer.minValue = 0;
        vidaPlayer.maxValue = vidaMaxima;
        vidaPlayer.value = vidaMaxima;
    }
    public void DanoNoPlayer(float dano)
    {
        vidaPlayer.value -= dano;
    }
    private void OnTriggerEnter(Collider other)// aplicar animãção do ataque
    {
        if (other.tag == tagObjects)
        {
            vidaPlayer.value -= dano;
        }

    }
    public void DanoInimigo(float dano)
    {
        vidaPlayer.value -= dano;

        if (vidaPlayer.value <= vidaPlayer.minValue)
        {
            Destroy(this.gameObject);

        }
    }

}
