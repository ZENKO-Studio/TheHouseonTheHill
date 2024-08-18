using System.Collections;
using System.Collections.Generic;
using PixelCrushers.DialogueSystem;
using UnityEngine;

//public class TestCommand : MonoBehaviour
//{
//    public Collider targetCollider;
//    public string questID = "YourQuestID"; // Replace with your actual quest ID

//    void OnEnable()
//    {
//        Subscribe to the event for quest state changes
//       DialogueManager.OnQuestEntryStateChange += HandleQuestEntryStateChange;
//    }

//    void OnDisable()
//    {
//        Unsubscribe to avoid memory leaks
//        DialogueManager.OnQuestEntryStateChange -= HandleQuestEntryStateChange;
//    }

//    private void HandleQuestEntryStateChange(QuestEntryArgs args)
//    {
//        Check if this is the quest entry we're interested in
//        if (args.questName == questID)
//        {
//            Retrieve the quest state through a custom method or property
//            Quest quest = GetQuestByID(questID);
//            if (quest != null && quest.state == QuestState.Success) // Check if quest state is Success
//            {
//                Disable the collider if the quest state is Success
//                if (targetCollider != null)
//                {
//                    targetCollider.enabled = false;
//                    Debug.Log("Collider disabled because quest state changed to Success.");
//                }
//            }
//        }
//    }

//    Custom method to get a quest by ID
//    private QuestLog GetQuestBy(string id)
//    {
//        Use Dialogue System's methods or data to find the quest
//         This is a placeholder; replace with actual implementation
//        foreach (var quest in DialogueManager.DatabaseManager.MasterDatabase.variables.Equals())
//        {
//            if (quest.ID == id)
//            {
//                return quest;
//            }
//        }

//    }
//}
