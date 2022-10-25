using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.DialogueControl;
using TMPro;
using UnityEngine.UI;

namespace RPG.UI
{

    public class DialogueUI : MonoBehaviour
    {
        PlayerConversant playerConversant;
        [SerializeField] TextMeshProUGUI AIText;
        [SerializeField] Button nextButton;
        [SerializeField] GameObject AIResponse;
        [SerializeField] Transform choiceRoot;
        [SerializeField] GameObject choicePrefab;
        [SerializeField] Button quitButton;
        [SerializeField] TextMeshProUGUI aiSpeakerName;
        [SerializeField] Image aiSpeakerPortrait;
        [SerializeField] TextMeshProUGUI playerSpeakerName;
        [SerializeField] Image playerSpeakerPortrait;



        // Start is called before the first frame update
        void Start()
        {
            playerConversant = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerConversant>();
            playerConversant.onConversationUpdated += UpdateUI;
            nextButton.onClick.AddListener(Next);
            quitButton.onClick.AddListener(() => playerConversant.Quit());
            UpdateUI();
        }

        private void Next()
        {
            playerConversant.Next();
        }

        private void UpdateUI()
        {
            gameObject.SetActive(playerConversant.IsActive());

            if (!playerConversant.IsActive())
            {
                return;
            }

            aiSpeakerName.text = playerConversant.GetCurrentConversantName();
            aiSpeakerPortrait.sprite = playerConversant.GetCurrentConversantPortrait();
            playerSpeakerName.text = playerConversant.ConversantName;
            playerSpeakerPortrait.sprite = playerConversant.ConversantPortrait;
            
            AIResponse.SetActive(!playerConversant.IsChoosing());
            choiceRoot.gameObject.SetActive(playerConversant.HasPlayerChoicesNext());
            AIText.text = playerConversant.GetText();

            if (playerConversant.HasPlayerChoicesNext())
            {
                BuildChoiceList();
            }

            nextButton.gameObject.SetActive(!playerConversant.HasPlayerChoicesNext() && playerConversant.HasNext());
            quitButton.gameObject.SetActive(!playerConversant.HasNext());


        }

        private void BuildChoiceList()
        {
            foreach (Transform item in choiceRoot)
            {
                Destroy(item.gameObject);
            }
            foreach (DialogueNode choice in playerConversant.GetChoices())
            {
                GameObject choiceButton = GameObject.Instantiate(choicePrefab, choiceRoot);
                var textComp = choiceButton.GetComponentInChildren<TextMeshProUGUI>();
                textComp.SetText(choice.DialogueText);
                Button button = choiceButton.GetComponentInChildren<Button>();
                button.onClick.AddListener(() => {
                    playerConversant.SelectChoice(choice);
                });
            }
        }
    }
}


