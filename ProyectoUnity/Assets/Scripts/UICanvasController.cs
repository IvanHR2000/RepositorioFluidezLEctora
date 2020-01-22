using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UICanvasController : UIScreenBaseController
{
    // Start is called before the first frame update
    public UIButtonAnswer[] answerButtons;
    private ListQuestions listQuestions;
    private AudioSource audio1;
    public Text txtQuestion;
    public Text txtNumberOfQuestion;
    public Text txtResultado;
    int numberQuestion = -1;
    int numberCorrectAnswer = 0;


    protected override void LocalAwake()
    {
        audio1 = GetComponent<AudioSource>();
    }
    public void NextQuestion(int i)
    {
      
            if (i == listQuestions.lista[numberQuestion].correctAnswer)
                numberCorrectAnswer++;
        if (numberQuestion < 4)
        {
            HideUIComponents(0, LoadNextQuestion);
        }
        else
        {
            if (numberCorrectAnswer >= 3)
                txtResultado.text = "Has respondido bien " + numberCorrectAnswer.ToString() + " preguntas";
            else
                txtResultado.text = "Intentalo nuevamente";
            int panelToShow = 3;
            if (numberCorrectAnswer == 5)
                panelToShow = 1;
            else if (numberCorrectAnswer >= 3)
                panelToShow = 2;
            HideAndShowUIComponet(0, panelToShow, ShowMesg);
    
        }
        audio1.Play();
    }
    public void SetQuestionsList(ListQuestions list)
    {
        listQuestions = list;
    }

    public void SetQuestion()
    {
        numberQuestion++;
        txtQuestion.text = listQuestions.lista[numberQuestion].question;
        for (int i= 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].SetText(listQuestions.lista[numberQuestion].answers[i]);
        }
        txtNumberOfQuestion.text = (numberQuestion + 1).ToString() + " de 5"; 
    }

    public void ShowMesg(object sender, EventArgs e)
    {
        ShowUIComponents(4);
    }

        public void LoadNextQuestion(object sender, EventArgs e)
    {
        ShowUIComponents(0);
        SetQuestion();


    }
    // Update is called once per frame
}
