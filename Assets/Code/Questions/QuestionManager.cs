using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private QuestionUI questionUI;
    [SerializeField] private QuestionData questionData;
    private List<Question> questions;
    private Question selectedQuestion;
    private int questionId = 0;
    private bool newQuestion = true;

    // Start is called before the first frame update
    void Start()
    {
        questions = questionData.questions;
        if (GameManager.alreadyAnsweredQuestions == null) GameManager.alreadyAnsweredQuestions = new bool[questions.Count];
        SelectQuestion();
    }

    void SelectQuestion()
    {
        bool validQuestion = false;
        int tries = 0;
        while (!validQuestion)
        {
            questionId = Random.Range(0, questions.Count);
            tries++;
            if (tries > 1000) GameManager.alreadyAnsweredQuestions = new bool[questions.Count];
            validQuestion = !GameManager.alreadyAnsweredQuestions[questionId];
        }
        selectedQuestion = questions[questionId];

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
            GameManager.alreadyAnsweredQuestions[questionId] = true;
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
