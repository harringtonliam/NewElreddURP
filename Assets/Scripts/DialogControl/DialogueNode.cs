using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RPG.Quests;

using System;
using RPG.Core;

namespace RPG.DialogueControl
{

    public class DialogueNode : ScriptableObject
    {
        [SerializeField]
        bool isPlayerSpeaking = false;
        [SerializeField]
        private string dialogueText;
        [SerializeField]
        private List<string> children = new List<string>();
        [SerializeField]
        private Rect rect = new Rect(0, 0, 200, 100);
        [SerializeField] DialogueNodeAction onEnterAction;
        [SerializeField] Quest onEnterQuest;
        [SerializeField] DialogueNodeAction onExitAction;
        [SerializeField] Quest onExitQuest;
        [SerializeField] string onEnterQuestObjective;
        [SerializeField] string onExitQuestObjective;
        [SerializeField] string onTriggerDialogueWithTag;
        [SerializeField] Condition condition;


        public string DialogueText
        {
            get
            {
                return dialogueText;
            }
        }

        public List<string> Children
        {
            get
            {
                return children;
            }
        }

        public Rect DialogRect
        {
            get
            {
                return rect;
            }
        }
        public bool IsPlayerSpeaking
        {
            get { return isPlayerSpeaking; }
        }

        public DialogueNodeAction OnEnterAction
        {
            get
            {
                return onEnterAction;
            }
        }

        public DialogueNodeAction  OnExitAction
        {
            get
            {
                return onExitAction;
            }
        }

        public Quest OnExitQuest
        {
            get
            {
                return onExitQuest;
            }
        }

        public Quest OnEnterQuest
        {
            get
            {
                return onEnterQuest;
            }
        }

        public string OnEnterQuestObective
        {
            get { return onEnterQuestObjective; }
        }

        public string OnExitQuestObjective
        {
            get { return onExitQuestObjective; }
        }

        public bool CheckCondition(IEnumerable<IPredicateEvaluator> evaluators)
        {
            return condition.Check(evaluators);
        }

        public string OnTriggerDialogueWithTag
        {
            get { return onTriggerDialogueWithTag; }
        }


#if UNITY_EDITOR

        public void SetDialogueText(string newText)
        {
            if (dialogueText != newText)
            {
                Undo.RecordObject(this, "Update Dialog Text");
                dialogueText = newText;
                EditorUtility.SetDirty(this);
            }
        }

        public void SetPosition(Vector2 newPosition)
        {
            Undo.RecordObject(this, "Move Dialog Node");
            rect.position = newPosition;
            EditorUtility.SetDirty(this);
        }

        public void AddChild(string childId)
        {
            Undo.RecordObject(this, "Add Dialog Link");
            children.Add(childId);
            EditorUtility.SetDirty(this);
        }

        public void RemoveChild(string childId)
        {
            Undo.RecordObject(this, "Remove Dialog Link");
            children.Remove(childId);
            EditorUtility.SetDirty(this);
        }

        public void SetIsPlayerSpeaking(bool newIsPlayerSpeaking)
        {
            Undo.RecordObject(this, "Change Dialog Speaker");
            isPlayerSpeaking = newIsPlayerSpeaking;
            EditorUtility.SetDirty(this);
        }


#endif
    }
}

