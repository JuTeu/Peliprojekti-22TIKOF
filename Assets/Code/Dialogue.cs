using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float lineDelay;

    private int index;
    private bool isTyping;

    void Start()
    {
        StartDialogue();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                isTyping = false;
                textComponent.text = lines[index];
                StartCoroutine(DelayBeforeNextLine());
            }
            else if (textComponent.text == lines[index])
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false;
        yield return new WaitForSeconds(lineDelay);
        NextLine();
    }

    IEnumerator DelayBeforeNextLine()
    {
        yield return new WaitForSeconds(lineDelay - ((lines[index].Length - textComponent.text.Length) * textSpeed));
        NextLine();
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false);
            GameManager.CloseDialogue();
        }
    }


}


