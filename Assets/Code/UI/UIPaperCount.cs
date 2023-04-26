using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIPaperCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text, textDropShadow;
    GameObject requiredPapersDisplay;
    private int total, collected = 0;
    
    public void SetTotalPapers (int input) {total = input; UpdateGraphic();}
    public void SetCollectedPapers (int input) {collected = input; UpdateGraphic();}
    public void CollectedPapers (int input) {collected += input; UpdateGraphic();}
    public void RevokedPapers (int input) {collected -= input; UpdateGraphic();}
    
    private void UpdateGraphic()
    {
        string displayedString = collected + "/" + total;
        text.text = displayedString;
        textDropShadow.text = displayedString;
        requiredPapersDisplay = GameObject.Find("RequiredPapers");
        if (requiredPapersDisplay != null) requiredPapersDisplay.GetComponent<TextMeshPro>().text = displayedString;
    }
}
