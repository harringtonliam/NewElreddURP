using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using RPG.Saving;

namespace RPG.DialogueControl
{
    public class DialogueTrigger : MonoBehaviour, ISaveable
    {
        [SerializeField] bool hasBeenTriggered = false;
        [SerializeField] AIConversant dialogueToTrigger;



        public void OnTriggerEnter(Collider other)
        {
            if (hasBeenTriggered) return;

            if (dialogueToTrigger == null) return;

            PlayerConversant playerConversant = other.GetComponent<PlayerConversant>();

            if (playerConversant == null) return;

            playerConversant.StartDialogue(dialogueToTrigger, dialogueToTrigger.Dialogue);
            hasBeenTriggered = true;


        }

        public object CaptureState()
        {
            return hasBeenTriggered.ToString();
        }

        public void RestoreState(object state)
        {
            if (state.ToString() == "True")
            {
                hasBeenTriggered = true;
            }
        }
    }

}


