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

    private int[] randomIndexes;
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
                textComponent.text = lines[randomIndexes[index]];
                StartCoroutine(DelayBeforeNextLine());
            }
            else if (textComponent.text == lines[randomIndexes[index]])
            {
                NextLine();
            }
        }
    }

    void StartDialogue()
    {
        randomIndexes = GetRandomIndexes(2, lines.Length);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        Debug.Log("Typing line " + (index + 1) + ": " + lines[randomIndexes[index]]);
        isTyping = true;
        textComponent.text = "";
        foreach (char c in lines[randomIndexes[index]].ToCharArray())
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
        Debug.Log("Delaying before next line...");
        yield return new WaitForSeconds(lineDelay - ((lines[randomIndexes[index]].Length - textComponent.text.Length) * textSpeed));
        NextLine();
    }

void NextLine()
{
    if (isTyping)
    {
        return;
    }

    if (index < randomIndexes.Length - 1)
    {
        index++;
        textComponent.text = string.Empty;
        Debug.Log("Moving to line " + (index + 1) + ": " + lines[randomIndexes[index]]);
        StartCoroutine(TypeLine());
    }
    else
    {
        Debug.Log("Dialogue completed.");
        gameObject.SetActive(false);
    }
}


    private int[] GetRandomIndexes(int count, int max)
    {
        HashSet<int> indexes = new HashSet<int>();
        while (indexes.Count < count)
        {
            int index = Random.Range(0, max);
            indexes.Add(index);
        }
        return new List<int>(indexes).ToArray();
    }
}
