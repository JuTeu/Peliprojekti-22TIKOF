using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionUI : MonoBehaviour
{
    [SerializeField] private QuestionManager questionManager;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<Button> options;
    [SerializeField] private Color correctCol, wrongCol, normalCol;

    private Question question;
    private bool answered;

    // Start is called before the first frame update
    void Awake()
    {
        for(int i = 0; i < options.Count; i++)
        {
            Button localBtn = options[i];
            localBtn.onClick.AddListener(() => OnClick(localBtn));
        }
    }

    public void SetQuestion(Question question)
    {
        this.question = question;
        questionText.text = question.questionInfo;

        List<string> answerList = ShuffleList.ShuffleListItems<string>(question.options);

        for(int i = 0; i < options.Count; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerList[i];
            options[i].name = answerList[i];
            options[i].image.color = normalCol;
        }
        answered = false;
    }

    private void OnClick(Button btn)
    {
        if (!answered)
        {
            answered=true;
            bool val = questionManager.Answer(btn.name);

            if(val)
            {
                btn.image.color = correctCol;
            }
            else
            {
                btn.image.color = wrongCol;
            }
        }
    }
}
