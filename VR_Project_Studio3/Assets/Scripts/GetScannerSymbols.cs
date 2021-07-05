using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScannerSymbols : MonoBehaviour
{
    public void SymbolGet()
    {
        switch (name)
        {
            case "HandScanner 1":
                {
                    Debug.Log("Switch1");
                    GetComponentInParent<CorrectSymbolCheck>().temp = GetComponentInParent<RandomiseSymbols>().symbols[0].GetComponentInChildren<Image>().sprite;
                    break;
                }
            case "HandScanner 2":
                {
                    Debug.Log("Switch2");
                    GetComponentInParent<CorrectSymbolCheck>().temp = GetComponentInParent<RandomiseSymbols>().symbols[1].GetComponentInChildren<Image>().sprite;
                    break;
                }
            case "HandScanner 3":
                {
                    Debug.Log("Switch3");
                    GetComponentInParent<CorrectSymbolCheck>().temp = GetComponentInParent<RandomiseSymbols>().symbols[2].GetComponentInChildren<Image>().sprite;
                    break;
                }
            case "HandScanner 4":
                {
                    Debug.Log("Switch4");
                    GetComponentInParent<CorrectSymbolCheck>().temp = GetComponentInParent<RandomiseSymbols>().symbols[3].GetComponentInChildren<Image>().sprite;
                    break;
                }
        }
    }
}
