using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Opcoes : MonoBehaviour
{
    public Slider controleCena;

    public void mudouValor ()
    {
        float valorcena = controleCena.value;

        PlayerPrefs.SetFloat("controleCena", valorcena);



    }
}
