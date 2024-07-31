using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Interactable;
using UnityEngine;
using UnityEngine.Events;

public class Combination : MonoBehaviour
{
    public Dial[] dials; // Array of dials
    public string correctCombination; // Correct combination
    private char[] currentCombination;
    private Animator animate;
    public OpenThings _OpenThings;

    public UnityEvent onEnter;

    private void Start()
    {

        animate = GetComponent<Animator>();
        
        currentCombination = new char[dials.Length];
        
        
        for (int i = 0; i < dials.Length; i++)
        {
            int index = i; // Local copy to avoid closure issues
            dials[i].OnDialRotated.AddListener((letter) => UpdateCombination(index, letter));
        }
    }

    private void UpdateCombination(int index, char letter)
    {
        currentCombination[index] = letter;
        Debug.Log("Current Combination: " + new string(currentCombination));
        CheckCombination();
    }

    private void CheckCombination()
    {
        if (new string(currentCombination) == correctCombination)
        {
            Debug.Log("Combination is correct!");
           animate.SetTrigger("Solved");
           _OpenThings.OpenDoor();
        
           OnEnter();
           Destroy(gameObject);
           
            // Trigger success event or actions here
        }
    }

    private void OnEnter()
    {
        onEnter?.Invoke();
    }
}
