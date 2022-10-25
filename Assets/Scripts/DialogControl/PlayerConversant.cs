using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using RPG.Attributes;
using RPG.Core;
using RPG.Movement;
using RPG.Quests;


namespace RPG.DialogueControl
{
    public class PlayerConversant : MonoBehaviour, IAction
    {
        [SerializeField] float conversationRange = 2f;

        Dialogue currentDialog;
        DialogueNode currentNode = null;
        bool isChoosing = false;
        AIConversant currentConversant;
        public event Action onConversationUpdated;

        string conversantName;
        Sprite conversantPortrait;
        GameConsole gameConsole = null;

        bool dialogStarted;

        bool isNextDialogueQueued = false;
        AIConversant nextDialogueConversant = null;
        Dialogue nextDialgueDialogue = null;


        public string ConversantName
        {
            get { return conversantName; }
        }

        public Sprite ConversantPortrait
        {
            get { return conversantPortrait; }
        }

        private void Start()
        {
            gameConsole = FindObjectOfType<GameConsole>();

            CharacterSheet characterSheet = GetComponent<CharacterSheet>();
            if (characterSheet == null)
            {
                conversantName = "unknown";
                conversantPortrait = null;
            }
            else
            {
                conversantName = characterSheet.CharacterName;
                conversantPortrait = characterSheet.Portrait;
            }
        }


        void Update()
        {
            Mover mover = GetComponent<Mover>();

            if (currentConversant != null)
            {
                mover.MoveTo(currentConversant.transform.position, 1f); ;
                if (GetIsInRange())
                {
                    mover.Cancel();
                    if (!dialogStarted)
                    {
                        DialogBehaviour();
                    }

                }
            }
        }

        private void DialogBehaviour()
        {
            dialogStarted = true;
            DialogueNode[] rootNodes = FilterOnCondition(currentDialog.GetRootNodes()).ToArray();
            currentNode = rootNodes[0];
            WriteToConsole(currentNode);
            TriggerEnterAction();
            if (onConversationUpdated != null)
            {
                onConversationUpdated();
            }

        }

        public void StartDialogue(AIConversant newConverstant, Dialogue newDialogue)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            currentConversant = newConverstant;
            currentDialog = newDialogue;
            dialogStarted = false;
        }

        public void Quit()
        {
            currentDialog = null;
            TriggerExitAction();
            currentConversant = null;
            currentNode = null;
            isChoosing = false;
            onConversationUpdated();
            if (isNextDialogueQueued)
            {
                isNextDialogueQueued = false;
                StartDialogue(nextDialogueConversant, nextDialgueDialogue);
            }
        }

        public bool IsActive()
        {
            return currentDialog != null;
        }

        public bool IsChoosing()
        {
            return isChoosing;
        }

        public string GetText()
        {
            if (currentNode == null)
            {
                return "";
            }

            return currentNode.DialogueText;
        }

        internal string GetCurrentConversantName()
        {
            try
            {
                return currentConversant.ConversantName;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            
        }

        internal Sprite GetCurrentConversantPortrait()
        {
            return currentConversant.ConversantPortrait;
        }

        internal string GetPlayerConversantName()
        {
            return conversantName;
        }

        internal Sprite GetPlayerConversantPortrait()
        {
            return conversantPortrait;
        }

        public IEnumerable<DialogueNode> GetChoices()
        {
            return FilterOnCondition(currentDialog.GetPlayerChildren(currentNode));
        }

        public void SelectChoice(DialogueNode chosenNode)
        {
            currentNode = chosenNode;
            WriteToConsole(currentNode);
            TriggerEnterAction();
            isChoosing = false;
            Next();
        }

        public void Next()
        {
            int numPlayerResponses = FilterOnCondition(currentDialog.GetPlayerChildren(currentNode)).Count();
            if (numPlayerResponses > 0)
            {
                isChoosing = true;
                TriggerExitAction();
                onConversationUpdated();
                WriteToConsole(currentNode);
                return;
            }

            isChoosing = false;

            DialogueNode[] childNodes = currentDialog.GetAIChildren(currentNode).ToArray();
            TriggerExitAction();
            currentNode = childNodes[0];
            TriggerEnterAction();
            onConversationUpdated();
            WriteToConsole(currentNode);
        }

        public bool HasNext()
        {
            if (currentDialog.GetAllChildren(currentNode).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool HasPlayerChoicesNext()
        {
            if (FilterOnCondition(currentDialog.GetPlayerChildren(currentNode)).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void TriggerEnterAction()
        {
            if (currentNode == null) return;
            if (currentNode.OnEnterAction == DialogueNodeAction.None) return;
            if (currentNode.OnEnterAction == DialogueNodeAction.GiveQuest)
            {
                TriggerAction(currentNode.OnEnterQuest);
            }
            else if (currentNode.OnEnterAction == DialogueNodeAction.CompleteQuestObjective)
            {
                TriggerAction(currentNode.OnEnterQuest, currentNode.OnEnterQuestObective);
            }
            else if (currentNode.OnEnterAction == DialogueNodeAction.TriggerDialogue)
            {
                TriggerAction(currentNode.OnTriggerDialogueWithTag);
            }
        }

        private void TriggerExitAction()
        {
            if (currentNode == null) return;
            if (currentNode.OnExitAction == DialogueNodeAction.None) return;
            if (currentNode.OnExitAction == DialogueNodeAction.GiveQuest)
            {
                TriggerAction(currentNode.OnExitQuest);
            }
            else if(currentNode.OnExitAction == DialogueNodeAction.CompleteQuestObjective)
            {
                TriggerAction(currentNode.OnExitQuest, currentNode.OnExitQuestObjective);
            }
            else if(currentNode.OnExitAction== DialogueNodeAction.TriggerDialogue)
            {
                TriggerAction(currentNode.OnTriggerDialogueWithTag);
            }
        }

        private void TriggerAction(Quest quest)
        {
            QuestList questList = FindObjectOfType<QuestList>();
            if (questList == null) return;            
            questList.AddQuest(quest);
        }

        private void TriggerAction(Quest quest, string questObjective)
        {
            QuestList questList = FindObjectOfType<QuestList>();
            if (questList == null) return;
            questList.CompleteQuestObjective(quest, questObjective);
        }

        private void TriggerAction(string triggerDialogWithTag)
        {
            GameObject triggerDialogWithObject = GameObject.FindGameObjectWithTag(triggerDialogWithTag);
            if (triggerDialogWithObject == null) return;
            AIConversant aIConversant = triggerDialogWithObject.GetComponent<AIConversant>();
            if (aIConversant == null) return;
            QueueNextDialog(aIConversant, aIConversant.Dialogue);
        }

        private void QueueNextDialog(AIConversant aIConversant, Dialogue dialogue)
        {
            nextDialogueConversant = aIConversant;
            nextDialgueDialogue = dialogue;
            isNextDialogueQueued = true;
        }

        private void WriteToConsole(DialogueNode node)
        {
            if (gameConsole == null) return;
            string speaker;
            if (node.IsPlayerSpeaking)
            {
                speaker = conversantName;
            }
            else
            {
                speaker = GetCurrentConversantName();
            }
            gameConsole.AddNewLine(speaker + ": " + node.DialogueText);
        }

        private bool GetIsInRange()
        {
            return conversationRange >= Vector3.Distance(currentConversant.transform.position, transform.position);
        }

        private IEnumerable<DialogueNode> FilterOnCondition(IEnumerable<DialogueNode> inputNode)
        {
            foreach (var node  in inputNode)
            {
                if (node.CheckCondition(GetEvaluators()))
                {
                    yield return node;
                }
            }
        }

        private IEnumerable<IPredicateEvaluator> GetEvaluators()
        {
            var evaluators =  GetComponents<IPredicateEvaluator>();
            return evaluators;
        }

        public void Cancel()
        {
            currentConversant = null;
            GetComponent<Mover>().Cancel();
        }
    }


}


