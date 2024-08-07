using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : HoldInteractable
{
    enum GeneratorState
    {
        Start,
        Identified,
        Unusable,
        Usable,
        Used
    }

    GeneratorState curState = GeneratorState.Start;

    [Header("Dialogue Setup")]

    [Tooltip("Dialogue that will be played once when player enters the trigger for the first time")]
    [SerializeField] List<string> initialLines = new List<string>();
    
    [Tooltip("Dialogue that will be played once when player enters the trigger and do not have required items")]
    [SerializeField] List<string> linesWhenUnusable = new List<string>();
    
    [Tooltip("Dialogue that will be played once when player enters the trigger have all the items")]
    [SerializeField] List<string> linesWhenUsable = new List<string>();

    [SerializeField] List<GameObject> lights;

    [Tooltip("Thsese ids will be checked to ensure item is present (Item Type Usables")] 
    [SerializeField] List<int> requiredItemIds = new List<int>();

    [SerializeField] int soundRange = 10;

    int c = 1;

    protected override void Start()
    {
        base.Start();

        //Our Code
        foreach (GameObject light in lights)
        {
            light.SetActive(false);
        }
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);

        if (curState == GeneratorState.Start)
        {
            GameManager.Instance.playerHud.UpdateDialogueText(initialLines[Random.Range(0, initialLines.Count)], 3);
            curState = GeneratorState.Identified;
        }
        else
        {
            CheckIfUsable();

            if(curState == GeneratorState.Unusable)
                GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUnusable[Random.Range(0, linesWhenUnusable.Count)], 3);
            else if(curState == GeneratorState.Usable)
                GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUsable[Random.Range(0, linesWhenUnusable.Count)], 3);

        }
    }

    private void CheckIfUsable()
    {
        if (requiredItemIds.Count > 0)
        {
            foreach (int itemId in requiredItemIds)
            {
                if (!InventoryHandler.Instance.HasUsableItem(itemId))
                    return;
            }

            curState = GeneratorState.Usable;

        }
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (curState == GeneratorState.Unusable)
            return;

        if (playerRef.bInteracting && curState == GeneratorState.Identified)
        {
            GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUnusable[Random.Range(0, linesWhenUnusable.Count)], 3);
            curState = GeneratorState.Unusable;
            return;
        }

        base.OnTriggerStay(other);
    
        //Every One Second Make Sound
        if(interactedTime > c)
        {
            var sound = new Sound(transform.position, soundRange);
            Sounds.MakeSound(sound);
            c++;
        }
    }

    protected override void OnInteractionComplete()
    {
        //Our Code
        foreach (GameObject light in lights)
        {
            light.SetActive(true);
        }

        base.OnInteractionComplete();
    }
}
