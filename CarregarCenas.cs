using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarregarCenas : MonoBehaviour
{

    public bool podeMudaCena;
    public string nomeDaCena;

    // Start is called before the first frame update
    void Start()
    {
        podeMudaCena = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && podeMudaCena)
        {

            SceneManager.LoadScene(nomeDaCena);


        }
    }
    private void OnTriggerEnter(Collider ceninha)
    {
        if (ceninha.gameObject.tag == "Player")
        {
            podeMudaCena = true;

        }

    }

    private void OnTriggerExit(Collider ceninha)
    {
        if (ceninha.gameObject.tag == "Player")
        {
            podeMudaCena = false;

        }
    }





}
