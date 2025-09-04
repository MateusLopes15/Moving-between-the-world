using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotacaoCamera : MonoBehaviour
{
    public static bool areadefala;
    public GameObject controleCameraa;
    public GameObject controleCameraa2;



    // Start is called before the first frame update
    void Start()
    {
        areadefala = false;
        controleCameraa.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        //Travando Camera no dialogo
        if (areadefala == true)
        {
            controleCameraa.gameObject.SetActive(false);
            controleCameraa2.gameObject.SetActive(true);
            GetComponent<MSCameraController>().enabled = false;

        }
        else
        {
            GetComponent<MSCameraController>().enabled = true;
            controleCameraa.gameObject.SetActive(true);
            controleCameraa2.gameObject.SetActive(false);
        }
    }
}
