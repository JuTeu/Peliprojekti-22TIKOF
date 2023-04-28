using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPaperCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text, textDropShadow, score;
    GameObject requiredPapersDisplay;
    private int total, collected = 0;
    
    public void SetTotalPapers (int input) {total = input; UpdateGraphic();}
    public void SetCollectedPapers (int input) {collected = input; UpdateGraphic();}
    public void CollectedPapers (int input) {collected += input; UpdateGraphic();}
    public void RevokedPapers (int input) {collected -= input; UpdateGraphic();}
    public void AddScore (int input)
    {
        GameManager.score += input;
        UpdateGraphic();
    }
    
    private void UpdateGraphic()
    {
        string displayedString = collected + "/" + total;
        text.text = displayedString;
        textDropShadow.text = displayedString;
        score.text = GameManager.score + "";
        requiredPapersDisplay = GameObject.Find("RequiredPapers");
        if (requiredPapersDisplay != null) requiredPapersDisplay.GetComponent<TextMeshPro>().text = displayedString;
        if (collected >= total) OpenPaperLocked();
    }

    private void OpenPaperLocked()
    {
        GameObject paperLock = GameObject.FindWithTag("PaperLocked");
        if (paperLock != null) paperLock.GetComponent<PaperLock>().Open();
    }
}
