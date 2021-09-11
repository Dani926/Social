using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchingScript : MonoBehaviour
{

    public GameObject[] questionGroups;
    public QAClass[] qaArray;

    // Start is called before the first frame update
    void Start()
    {
        qaArray = new QAClass[questionGroups.Length];
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Called when user clicks the submit button
    public void SubmitAnswer()
    {
        for (int i = 0; i < qaArray.Length; i++)
        {
            qaArray[i] = ReadQA(questionGroups[i]);
        }
    }

    public QAClass ReadQA(GameObject questionGroup)
    {
        QAClass result = new QAClass();
        GameObject q = questionGroup.transform.Find("Question").gameObject;
        GameObject a = questionGroup.transform.Find("Answer").gameObject;

        result.Question = q.GetComponent<Text>().text;

        if (a.GetComponent<ToggleGroup>() != null)
        {
            for (int i = 0; i < a.transform.childCount; i++)
            {
                if (a.transform.GetChild(i).GetComponent<Toggle>().isOn)
                {
                    result.Answer = a.transform.GetChild(i).Find("Label").GetComponent<Text>().text;
                    break;
                }
            }

        }
        else if (a.GetComponent<InputField>() != null)
        {
            result.Answer = a.transform.Find("Text").GetComponent<Text>().text;
        }
        else if (a.GetComponent<Dropdown>() != null)
        {

            result.Answer = a.transform.GetComponent<Dropdown>().options[GetComponent<Dropdown>().value].text;
        }

        Debug.Log(result.Answer);
        return result;
    }

    [System.Serializable]
    public class QAClass
    {
        public string Question;
        public string Answer;
    }
}

