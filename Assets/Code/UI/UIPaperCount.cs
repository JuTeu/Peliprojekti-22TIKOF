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
        GameManager.score += (int) (input * GameManager.scoreMultiplier);
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
        if (collected >= total && collected > 0 && GameManager.cameraMode == 0) Invoke("OpenPaperLocked", 1);
    }

    private void OpenPaperLocked()
    {
        GameObject paperLock = GameObject.FindWithTag("PaperLocked");
        if (paperLock != null)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerHealth>().Heal(15f);
            GameManager.cameraMode = 128;
        }
    }
}
