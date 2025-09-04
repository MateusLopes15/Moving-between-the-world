using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AnswerScript : MonoBehaviour
{
    public bool isCorrect = false;
    public QuizManager quizManager;


    public Color startColor;

    private void Start()
    {
        startColor = GetComponent<Image>().color;
            
    }
    public void Answers()
    {
        if (isCorrect)
        {
            GetComponent<Image>().color = Color.green;
            StartCoroutine("ResetarCorBotao");
            Debug.Log("Resposta Correta");
            quizManager.correct();
        }
        else
        {
            GetComponent<Image>().color = Color.red;
            StartCoroutine("ResetarCorBotao");
            Debug.Log("Resposta Errada");
            quizManager.wrong();

        }
    }
    IEnumerator ResetarCorBotao()
    {
        yield return new WaitForSeconds(0.2f);
        GetComponent<Image>().color = startColor;

    }
}
