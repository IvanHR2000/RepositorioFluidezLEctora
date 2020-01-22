using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TriviaController : BaseSceneController
{
    // Start is called before the first frame update

    public UICanvasController uiCanvasController;

    void Start()
    {
        string fileTrivia = "Trivia/PeterPan";
        TextAsset textFile = (TextAsset)Resources.Load(fileTrivia, typeof(TextAsset));
        MemoryStream txtSream = GetStream(textFile.text);
        StreamReader reader = new StreamReader(txtSream, System.Text.Encoding.UTF8);
       
        string data = reader.ReadToEnd();
        ListQuestions listQuetions;
        listQuetions = (ListQuestions)JsonUtility.FromJson<ListQuestions>(data);
        uiCanvasController.SetQuestionsList(listQuetions);
        uiCanvasController.SetQuestion();


    }


    private MemoryStream GetStream(string text)
    {
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);
        writer.Write(text);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }
    private void DD()
    {
        List<Question> questions;
        questions = new List<Question>();
        Question question;
        question = new Question();
        question.question = "Preguta 1";
        question.answers = new List<string>();
        question.answers.Add("respeusta 1");
        question.answers.Add("respeusta 2");
        question.answers.Add("respeusta 3");
        question.answers.Add("respeusta 1");
        question.correctAnswer = 1;
        questions.Add(question);
        question = new Question();
        question.question = "Preguta 2";
        question.answers = new List<string>();
        question.answers.Add("respeusta 1");
        question.answers.Add("respeusta 2");
        question.answers.Add("respeusta 3");
        question.answers.Add("respeusta 1");
        question.correctAnswer = 2;
        questions.Add(question);
        ListQuestions listQuest = new ListQuestions();
        listQuest.lista = questions;
        string data = JsonUtility.ToJson(listQuest);
        string path = Application.persistentDataPath + "/data.txt";
        File.AppendAllText(path, data);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
