using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LancarPedra : MonoBehaviour
{

    public Pedra _pedra;
    public Transform localDeLancamento;
    public float forcaDeLancamento = 10;
    public static float numeroDeGranadas;
    public float tempoPorLancamento = 1f;
    //
    public float PodeLanca = 1f;
    float delaylacar;
    public static bool animlacar = false;
    float cronometroGranada = 0;
    bool lancouGranada = false;
    //-----------
    public Image alvo1;
    public Image alvo2;
    public Image alvo3;

    public LayerMask mask;


    void Update()
    {
        VerficarAlvo();


        if (Input.GetButtonDown("Fire1") && WeaponsPlayer.WeaponSelect == 2)//lançar a pedra
        {
            if (localDeLancamento && numeroDeGranadas > 0 && !lancouGranada)
            {
                numeroDeGranadas--;
                lancouGranada = true;
                
            }

        } 

        if (lancouGranada)
        {
            cronometroGranada += Time.deltaTime;
        }
        if (cronometroGranada >= tempoPorLancamento)
        {
            Pedra objGranada = Instantiate(_pedra, localDeLancamento.position, localDeLancamento.rotation) as Pedra;
            Rigidbody rb = objGranada.GetComponent<Rigidbody>();
            rb.AddForce(Camera.main.transform.forward * forcaDeLancamento, ForceMode.Impulse);
            //rb.AddForce(transform.forward * forcaDeLancamento, ForceMode.Impulse); //ForceMode.Impulse
            lancouGranada = false;
            cronometroGranada = 0;
            WeaponsPlayer.WeaponSelect = 0;

            StartCoroutine("FinishAnimationArremesso");

        }
    }
    IEnumerator FinishAnimationArremesso()
    {

        yield return new WaitForSeconds(0.5f);
        WeaponsPlayer.WeaponSelect = 0;
        PegarItem.Pegueipedra = false;
        ControlePlayer.arremesso = false;
    }

    void VerficarAlvo()
    {
        Ray ray=Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            if (hit.collider.GetComponent<VilaoController>())
            {
                alvo1.color = Color.red;
                alvo2.color = Color.red;
                alvo3.color = Color.red;

            }
            else
            {
                alvo1.color = Color.black;
                alvo2.color = Color.black;
                alvo3.color = Color.black;

            }

            if(Input.GetButton("Fire1") && WeaponsPlayer.WeaponSelect == 2){

            }
        }
        else
        {
            alvo1.color = Color.black;//so pra garantir que alvo voltara a ser preto
            alvo2.color = Color.black;
            alvo3.color = Color.black;
        }

    }
    


}