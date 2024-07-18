using System;
using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interactable;
using TMPro;
using UnityEngine;

public class Keypad : MonoBehaviour
{

    [SerializeField]private TextMeshProUGUI ans;
    [SerializeField] private string _answer;
    [SerializeField] private string _incorrect;
    [SerializeField]private OpenThings _door;

    public void Number(int num)
    {
        ans.text += num.ToString();
    }

    public void Execute()
    {
        if (ans.text == _answer)
        {
            ans.text = "Correct";
            _door.OpenDoor();

        }
        else
        {

            ans.text = _incorrect;

        }


    }

    public void Reset()
    {
        ans.text = "";
    }
}
