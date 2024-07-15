using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboLock : MonoBehaviour
{

    [Header("Elements")] [SerializeField] private Dial[] dials;

    [Header("Settings")] [SerializeField] private string combination;

    [Header("Events")] [SerializeField] private UnityEvent onComboFound;

    public void CheckCombo(Dial dial)
    {

        for (int i = 0; i < dials.Length; i++)
        {
            int combinationNum = int.Parse(combination[i].ToString());

            if (combinationNum != dials[i].GetNumber())
            {
                dial.Unlock();
                return;
                
            }

        }

        CorrectCombo();

    }

    private void CorrectCombo()
    {
        foreach (var t in dials)
            t.Lock();

        onComboFound?.Invoke();
    }

}
