using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class  Question 
{
    // Start is called before the first frame update
    public string question;
    public List<string> answers;
    public int correctAnswer;
}

[System.Serializable]
public class ListQuestions
{
   public  List<Question> lista;
}
