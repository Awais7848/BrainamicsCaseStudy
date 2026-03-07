using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Option : MonoBehaviour
{
    [SerializeField] Button next, previous;
    [SerializeField] TMP_Text optionName;
    int optionIndex=0;
    public List<string> options = new List<string>();

      UnityAction<int> OnValueChanged;
 



   public void Initalize(UnityAction<int> @onValueChanged)
    {
        next.onClick.AddListener(Next);
        previous.onClick.AddListener(Previous);
        OnValueChanged=@onValueChanged;
    }

    void Next()
    {
        optionIndex++;

        if (optionIndex >=options.Count) optionIndex = 0;

        optionName.text = options[optionIndex];
        OnValueChanged.Invoke(optionIndex);
    }

    void Previous()
    {
        optionIndex--;
        if( optionIndex < 0 ) optionIndex = options.Count-1;

        optionName.text = options[optionIndex]; 
        OnValueChanged.Invoke(optionIndex);
    }




}
