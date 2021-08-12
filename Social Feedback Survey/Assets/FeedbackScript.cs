using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackScript : MonoBehaviour { 

    public GameObject[] questionGroupArr;
    public QAClass[] qaArr;

    // Start is called before the first frame update
    void Start()
    {
        qaArr = new QAClass[questionGroupArr.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called when user clicks the submit button
    public void SubmitAnswer()
    {
        for(int i = 0; i < qaArr.Length; i++){
            qaArr[i] = ReadQA(questionGroupArr[i]);
        }
    }

    QAClass ReadQA(GameObject questionGroup)
    {
        QAClass result = new QAClass();
        GameObject q = questionGroup.transform.Find("Question").gameObject;
        GameObject a = questionGroup.transform.Find("Answer").gameObject;

        result.Question = q.GetComponent<Text>().text;

        if(a.GetComponent<ToggleGroup>() != null) { 
            for(int i = 0; i < a.transform.childCount; i++)
            {
                if (a.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    result.Answer = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    break;
                }
            }

        } else if(a.GetComponent<InputField>() != null)
        {
            result.Answer = a.transform.Find("Text").GetComponent<Text>().text;
        }

        return result;
    }

    [System.Serializable]
    public class QAClass
    {
        public string Question = "";
        public string Answer = "";
    }
}
