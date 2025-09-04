using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DhaliaController : MonoBehaviour
{
    public static bool dhaliaFalar;
    public static bool chamarDh;
    public float missaoDhf1;

    public GameObject dhaliaf1;
    public GameObject dhaliaf1_Completa;
    public GameObject portal;
    public GameObject ativarLoca;
    public GameObject desativarmiss;



    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        dhaliaFalar = false;
        chamarDh = false;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>(); //Atribuindo o componente a variável antes da inicialização
    }
    // Update is called once per frame
    void Update()
    {

        if (SistemaHpMiniBoss.contagemDeadMob >= missaoDhf1)
        {
            //dhaliaf1.SetActive(false);
            Instantiate(dhaliaf1_Completa, transform.position, transform.rotation);
            dhaliaf1_Completa.SetActive(true);
            portal.SetActive(true);
            Destroy(this.gameObject);


        }

        anim.SetBool("ChamarDh", chamarDh);
        anim.SetBool("DhaliaFalar", dhaliaFalar);

    }

    public void TestarFase()
    {
        StartCoroutine("definirTest");
        if (SistemaHpMiniBoss.contagemDeadMob >= missaoDhf1)
        {
            //dhaliaf1.SetActive(false);
            Instantiate(dhaliaf1_Completa, transform.position, transform.rotation);
            //dhaliaf1_Completa.SetActive(true);
            portal.SetActive(true);
            Destroy(this.gameObject);


        }
    }
    void definirTest()
    {
        SistemaHpMiniBoss.contagemDeadMob = 4;
    }
    private void OnTriggerEnter(Collider falarDeusa)
    {
        if (falarDeusa.gameObject.tag == "Player")
        {//Se Objeto com tag Player colidir com o objeto no cenario, ira ativar ...

            dhaliaFalar = true;
            chamarDh = true;
            desativarmiss.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider falarDeusa)
    {
        if (falarDeusa.gameObject.tag == "Player")
        {
            dhaliaFalar = false;
            ativarLoca.SetActive(true);

        }
    }

}