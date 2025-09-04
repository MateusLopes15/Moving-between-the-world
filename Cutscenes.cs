using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscenes : MonoBehaviour
{
    [Header("Nome da cena a ser carregada")]
    public string cenaACarregar;
    [Space(20)]
    public Texture texturaFundos;
    public Texture barraDeProgresso;
    public string textoLoad = "Progresso do carregamento:";
    public Color corDoTexto = Color.white;
    public Font Fonte;
    [Space(20)]
    [Range(0.5f, 3.0f)]
    public float tamanhoDoTexto = 1.5f;
    [Range(1, 10)]
    public int larguraDaBarra = 8;
    [Range(1, 10)]
    public int alturaDaBarra = 2;
    [Range(-4.5f, 4.5f)]
    public float deslocarBarra = 4;
    [Range(-8, 4)]
    public float deslocarTextoX = 2;
    [Range(-4.5f, 4.5f)]
    public float deslocarTextoY = 3;

    private bool mostrarCarregamento = false;
    private int progresso = 0;

    //public string proximaCena;
    public float cronometro;
    public float tempoVideo;
    public static bool acabouVideo;
    public bool mobscena;

    private void start()
    {
        acabouVideo = false;

    }
    void Update()
    {
        if (!acabouVideo)
        {
            cronometro = cronometro + Time.deltaTime;

        }
        if (cronometro >= tempoVideo)
        {
            acabouVideo = true;


        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    StartCoroutine(CenaDeCarregamento(cenaACarregar));
        //}

        if (acabouVideo)
        { 
            StartCenas();
        }

    }
    public void StartCenas()
    {
        cronometro = 0;
        acabouVideo = false;
        StartCoroutine(CenaDeCarregamento(cenaACarregar));
        GetComponent<AudioListener>().enabled = false;

    }


    public void StartLevel(string sceneIndex)
    {
        GetComponent<AudioListener>().enabled = false;

        StartCoroutine(CenaDeCarregamento(sceneIndex));
    }

    IEnumerator CenaDeCarregamento(string sceneIndex)
    {
        mostrarCarregamento = true;
        GetComponent<AudioListener>().enabled = false;
        AsyncOperation carregamento = SceneManager.LoadSceneAsync(sceneIndex);

        //AsyncOperation carregamento = Application.LoadLevelAsync(cena);
        while (!carregamento.isDone)
        {
            progresso = (int)(carregamento.progress * 100);

            yield return null;
        }
    }

    void OnGUI()
    {
        if (mostrarCarregamento == true)
        {
            GUI.contentColor = corDoTexto;
            GUI.skin.font = Fonte;
            GUI.skin.label.fontSize = (int)(Screen.height / 50 * tamanhoDoTexto);
            //TEXTURA DE FUNDO
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texturaFundos);

            //TEXTO DE CARREGAMENTO
            float deslocXText = (Screen.height / 10) * deslocarTextoX;
            float deslocYText = (Screen.height / 10) * deslocarTextoY;
            GUI.Label(new Rect(Screen.width / 2 + deslocXText, Screen.height / 2 + deslocYText, Screen.width, Screen.height), textoLoad + " " + progresso + "%");

            //BARRA DE PROGRESSO
            float largura = Screen.width * (larguraDaBarra / 10.0f);
            float altura = Screen.height / 50 * alturaDaBarra;
            float deslocYBar = (Screen.height / 10) * deslocarBarra;
            GUI.DrawTexture(new Rect(Screen.width / 2 - largura / 2, Screen.height / 2 - (altura / 2) + deslocYBar, largura * (progresso / 100.0f), altura), barraDeProgresso);
        }
    }
}