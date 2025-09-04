using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    public List<QuestaoERespostas> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject minigame;
    public GameObject carro;
    public GameObject camera;
    public GameObject carroinimii;




    public Text QuestionTxt;


    int totalQuestions = 0;

    private void Start()
    {
        totalQuestions = QnA.Count;
        generateQuestion();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    void GameOver()
    {

    }
    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();


    }
    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestion();
    }
    void SetAnswers()//Perguntas
    {
        for (int i = 0; i < options.Length; i++)
        {
            //options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i+1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;
            }

        }


    }

    void generateQuestion()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            QuestionTxt.text = QnA[currentQuestion].Question;
            SetAnswers();
        }
        else
        {
            Debug.Log("Acabou as questões");
            GameOver();
            Destroy(minigame);
           
            carro.gameObject.SetActive(true);
            camera.gameObject.SetActive(true);
            carroinimii.gameObject.SetActive(true);
        }
    }

    IEnumerator DelayQuestao()
    {
        yield return new WaitForSeconds(1f);
       

    }

}
