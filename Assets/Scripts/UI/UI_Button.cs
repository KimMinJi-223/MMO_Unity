using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Button : MonoBehaviour
{
    [SerializeField]
    Text _text;

    int _score = 0;
    
    public void OnButtonClicked()//public 반드시 퍼블릭으로 해야한다.
    {
        _score++;
        _text.text = $"점수 : {_score}";
    }
}
