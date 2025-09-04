using System.Collections;
using UnityEngine;
using System;

public class MeuSave : MonoBehaviour
{

    private void Update()
    {

    }
    public void SalvarDados()
    {
        DataSave.Corrente = new DataSave();
        Vector3 p = transform.position;
        DataSave.Corrente.posHeroi = new posicao(p.x, p.y, p.z);
        SalvarGame.Salvar();
        print("Eu Salvei o jogo");

    }

    public void CarregarDadosSalvos()
    {
        SalvarGame.Load();
        //DataSave d = SalvarGame.saveGames(SalvarGame.saveGames.Count - 1);

        //transform.position = d.posHeroi.posicionamento + 5 * Vector3.up;


    }
}
