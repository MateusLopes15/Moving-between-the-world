using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SistemaHpMiniBoss : MonoBehaviour
{
    public bool fase3C;
    public bool fase3Inco;
    public GameObject passarfase3;

    public Slider BarraVidaMini;

    public float vidaMaxima = 100;
    public static float contagemDeadMob;

    public GameObject dead;


    public string cena;

    public bool mobscena;
    private void Start() {

        mobscena = false;
        contagemDeadMob = 0;
        BarraVidaMini.minValue = 0;
        BarraVidaMini.maxValue = vidaMaxima;
        BarraVidaMini.value = vidaMaxima;

    }
    private void Update()
    {
        ControleDeVidaMini();
        if (mobscena)
        {

        }


    }

    void ProxFase()
    {
        FindObjectOfType<Cutscenes>().StartCenas();

    }
    private void ControleDeVidaMini()
    {
        if (BarraVidaMini.value >= vidaMaxima)
        {
            BarraVidaMini.value = vidaMaxima;
        }
        if (BarraVidaMini.value <= BarraVidaMini.minValue)
        {
            BarraVidaMini.value = BarraVidaMini.minValue;
            //Instantiate(dead, transform.position, transform.rotation);
            //contagemDeadMob++;
            //Destroy(this.gameObject);
        }
    }

    public void DanoPedra(float dano)
    {
        BarraVidaMini.value -= dano;

        if (BarraVidaMini.value <= BarraVidaMini.minValue){
            //Instantiate(dead, transform.position, transform.rotation);
            contagemDeadMob++;
            Destroy(this.gameObject);
      
        }
        if (BarraVidaMini.value <= BarraVidaMini.minValue && fase3Inco)
        {
            SceneManager.LoadScene(cena);

        }
    }
 
}
