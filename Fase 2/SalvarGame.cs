using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SalvarGame
{
    public static List<DataSave> saveGames = new List<DataSave> ();

    public static void Salvar()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/saveDoMeuJogo.save");
        saveGames.Add(DataSave.Corrente);
        bf.Serialize(file, SalvarGame.saveGames);
        file.Close();
        Debug.Log("Jogo Salvo");
    }

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/saveDoMeuJogo.save"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/saveDoMeuJogo.save", FileMode.Open);
            saveGames = (List<DataSave>)bf.Deserialize(file);
            file.Close();
        }
    }







}