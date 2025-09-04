using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Carregar : MonoBehaviour
{

    bool podeInteragir = false;
    public static bool completaTarefa = false;
    public GameObject Jogador;
    public string CenaACarregar;

    void Update()
    {
        if (podeInteragir)
        {
            Jogador.GetComponent<SalvarPosit>().SalvarLocalizacao();
            SceneManager.LoadScene(CenaACarregar);
        }
    }

    public void CompletouATarefa()
    {
        Jogador.GetComponent<SalvarPosit>().SalvarLocalizacao();
        SceneManager.LoadScene(CenaACarregar);
    }
    void OnTriggerEnter()
    {

            podeInteragir = true;

    }

    void OnTriggerExit()
    {

            podeInteragir = false;

    }

    //void OnGUI()
    //{
    //    if (podeInteragir == true)
    //    {
    //        GUIStyle stylez = new GUIStyle();
    //        stylez.alignment = TextAnchor.MiddleCenter;
    //        GUI.skin.label.fontSize = 20;
    //        GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 50, 200, 30), "Pressione 'E'");
    //    }
    //}
}
