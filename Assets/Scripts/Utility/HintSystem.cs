using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HintSystem : MonoBehaviour
{
    public Text hintText;

    [SerializeField]
    private int hintSectionIndex;

    [SerializeField]
    private int hintIndex;
    private List<string[]> hints;

    void Start()
    {
        Initialize();
    }

    public void Initialize() {
        hintIndex = 0;
        hintSectionIndex = 0;
    }

    public void ConfigureHints(List<string[]> hintList) {
        hints = hintList;
    }

    public List<string[]> Hints() {
        return hints;
    }

    public int HintSectionIndex() {
        return hintSectionIndex;
    }

    public void SetHintIndex(int index) {
        if(hintSectionIndex == index) return;
        
        hintSectionIndex = index;
        hintIndex = 0;
    }

    public void ShowHint() {
        if(hints[hintSectionIndex].Length <= hintIndex) return;

        if(hintText != null) hintText.text = hints[hintSectionIndex][hintIndex];

        hintIndex++;
    }

    public string CurrentHint() {
        return hints[hintSectionIndex][hintIndex-1];
    }
}