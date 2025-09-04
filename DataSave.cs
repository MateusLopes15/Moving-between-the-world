using System.Collections;
using UnityEngine;

[System.Serializable]
public class DataSave
{
    public static DataSave Corrente;
    public posicao posHeroi;

    public DataSave()
    {

    }

}

[System.Serializable]
public struct posicao
{
    public float X, Y, Z;

    public posicao(float posX, float posY, float posZ)
    {
        X = posX;
        Y = posY;
        Z = posZ;

    }

    public Vector3 posicionamento
    {
        private set { }
        get { return new Vector3(X, Y, Z); }

    }
}