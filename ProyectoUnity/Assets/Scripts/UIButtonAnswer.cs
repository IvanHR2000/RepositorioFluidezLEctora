using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonAnswer : MonoBehaviour
{
    // Start is called before the first frame update
    Text txtAnswer;
    void Awake()
    {
        txtAnswer = transform.Find("txtQuestion").GetComponent<Text>();

    }
    public void SetText(string value)
    {
        txtAnswer.text = value;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
