using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPlayer : MonoBehaviour
{
    public static int WeaponSelect;

    public GameObject Porrete;
    public GameObject Pedra;
    public static bool usaPorrete;
    public bool usaPorrete2;


    // Start is called before the first frame update
    void Start()
    {
        WeaponSelect = 0;
        Porrete.SetActive(false);
        Pedra.SetActive(false);
        usaPorrete = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Selecionando arma
        if (Input.GetKeyDown(KeyCode.Alpha1) && usaPorrete)
        {
            Porrete.SetActive(true);
            WeaponSelect = 1;
            Pedra.SetActive(false);


        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && usaPorrete2)
        {
            Porrete.SetActive(true);
            WeaponSelect = 1;
            Pedra.SetActive(false);


        }

        if (PegarItem.Pegueipedra)
        {
            WeaponSelect = 2; 
            Porrete.SetActive(false);
            Pedra.SetActive(true);

        }


        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    WeaponSelect = 3;
        //    Porrete.SetActive(false);
        //    Pedra.SetActive(false);

        //}

        if (Input.GetKeyDown("x"))
        {
            WeaponSelect = 0;
            Porrete.SetActive(false);
            Pedra.SetActive(false);

        }
        if (WeaponSelect == 0)
        {
            Porrete.SetActive(false);
            Pedra.SetActive(false);
        }

            //if (!PegarItem.Pegueipedra)
            //{
            //    WeaponSelect = 0;
            //}

        }
}
