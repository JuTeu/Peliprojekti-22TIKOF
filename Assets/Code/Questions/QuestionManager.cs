using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private QuestionUI questionUI;
    [SerializeField] private QuestionData questionData;
    private List<Question> questions;
    private Question selectedQuestion;
    private bool newQuestion = true;

    // Start is called before the first frame update
    void Start()
    {
        questions = questionData.questions;
        SelectQuestion();
    }

    void SelectQuestion()
    {
        int val = Random.Range(0, questions.Count);
        selectedQuestion = questions[val];

        questionUI.SetQuestion(selectedQuestion);
        if (newQuestion)
        {
            newQuestion = false;
        }
        else
        {
            GameManager.CloseQuestionMenu();
        }
    }

    public bool Answer(string answered)
    {
        bool correctAns = false;

        if(answered == selectedQuestion.correctAns)
        {
            correctAns = true;
            GameManager.correctAnswers++;
            GameObject.Find("PaperCount").GetComponent<UIPaperCount>().AddScore(1000);
        }
        else
        {

        }
        GameManager.questionsAnswered++;
        GameManager.questionsAnsweredTotal++;
        GameObject.Find("PaperCount").GetComponent<UIPaperCount>().CollectedPapers(1);

        Invoke("SelectQuestion", 0.4f);

        return correctAns;
    }
}

[System.Serializable]

public class Question
{
    public string questionInfo;
    public List<string> options;
    public string correctAns;
}
