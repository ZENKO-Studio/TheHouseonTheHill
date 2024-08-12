using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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

    [Header("Activation Requrements")]

    [Tooltip("Howmany switches are required to activate it")]
    [SerializeField] int requiredSwitchActivation = 1;

    int activatedSwitches = 0;

    //[SerializeField] List<GameObject> lights;

    [Tooltip("Thsese ids will be checked to ensure item is present (Item Type Usables")] 
    [SerializeField] List<int> requiredItemIds = new List<int>();

    [Header("Sound Generation")]

    [SerializeField] int soundRange = 10;

    int c = 1;

    AudioSource aSource;

    [Header("What should happen once activated")]
    [Tooltip("Which Animation Sequence should be trigggered")]
    [SerializeField] AnimSequenceTrigger sequenceToTrigger;

    protected override void Start()
    {
        base.Start();

        aSource = GetComponent<AudioSource>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

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
        if (activatedSwitches < requiredSwitchActivation)
            return;

        if (requiredItemIds.Count > 0)
        {
            foreach (int itemId in requiredItemIds)
            {
                if (!InventoryHandler.Instance.HasUsableItem(itemId))
                    return;
            }

        }
        curState = GeneratorState.Usable;
    }

    protected override void OnTriggerStay(Collider other)
    {
        if (curState == GeneratorState.Unusable || !other.CompareTag("Player"))
            return;

        if (playerRef.bInteracting && curState == GeneratorState.Identified)
        {
            GameManager.Instance.playerHud.UpdateDialogueText(linesWhenUnusable[Random.Range(0, linesWhenUnusable.Count)], 3);
            curState = GeneratorState.Unusable;
            return;
        }

        base.OnTriggerStay(other);

        //Make Sound when generator is being Activated
        if(interactedTime > 0f)
        {
            if(!aSource.isPlaying)
            {
                aSource.Play();
            }
        }
        else
        {
            if(aSource.isPlaying) 
                aSource.Stop();
        }
    
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
        //Stuff that should happen after generator is activated
        if (sequenceToTrigger != null)
            sequenceToTrigger.TriggerSequence();

        curState = GeneratorState.Used;

        base.OnInteractionComplete();
    }

    internal void ActivateSwitch()
    {
        activatedSwitches++;
    }
}
