using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FeedbackScript : MonoBehaviour { 

    public GameObject[] questionGroupArr;
    public QAClass[] answersArr;

    // Start is called before the first frame update
    void Start()
    {
        answersArr = new QAClass[questionGroupArr.Length];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Called when user clicks the submit button
    public void SubmitAnswer()
    {
        for(int i = 0; i < answersArr.Length; i++){
            answersArr[i] = ReadQA(questionGroupArr[i]);
        }
    }

    public QAClass ReadQA(GameObject questionGroup)
    {
        QAClass result = new QAClass();
        GameObject a = questionGroup.transform.Find("Answer").gameObject;

        if(a.GetComponent<ToggleGroup>() != null) { 
            for(int i = 0; i < a.transform.childCount; i++)
            {
                if (a.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    result.Answer = (i-1).ToString();
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
        public string Answer;
    }
}
