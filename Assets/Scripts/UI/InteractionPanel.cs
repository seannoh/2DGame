using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class InteractionPanel : MonoBehaviour
{
    public TextMeshProUGUI descriptionText, choice1Text, choice2Text, choice3Text;
    public Button choice1, choice2, choice3;
    public GameObject interactionPanelObject;

    public UnityEvent<bool> interactionEvent;

    private static InteractionPanel interactionPanel;

    public static InteractionPanel Instance()
    {
        if (!interactionPanel)
        {
            interactionPanel = FindObjectOfType(typeof(InteractionPanel)) as InteractionPanel;
            if (!interactionPanel)
                Debug.LogError("There needs to be one active InteractionPanel script on a GameObject in your scene.");
        }

        return interactionPanel;
    }

    public void Choice(string description, 
                        UnityAction choice1Action, string choice1Desc, 
                        UnityAction choice2Action, string choice2Desc, 
                        UnityAction choice3Action, string choice3Desc, bool closePanel = true)
    {
        // trigger event to indicate that interaction panel is open, set object active
        interactionEvent.Invoke(true);
        interactionPanelObject.SetActive(true);

        // if action/desc is not null, add listener to button, else set button inactive
        choice1.onClick.RemoveAllListeners();
        if(choice1Action != null && choice1Desc != null) {
            choice1.onClick.AddListener(choice1Action);
            choice1Text.SetText(choice1Desc);
            choice1.gameObject.SetActive(true);
        } else {
            choice1.gameObject.SetActive(false);
        }

        choice2.onClick.RemoveAllListeners();
        if(choice2Action != null && choice2Desc != null) {
            choice2.onClick.AddListener(choice2Action);
            choice2Text.SetText(choice2Desc);
            choice2.gameObject.SetActive(true);
        } else {
            choice2.gameObject.SetActive(false);
        }

        choice3.onClick.RemoveAllListeners();
        if(choice3Action != null && choice3Desc != null) {
            choice3.onClick.AddListener(choice3Action);
            choice3Text.SetText(choice3Desc);
            choice3.gameObject.SetActive(true);
        } else {
            choice3.gameObject.SetActive(false);
        }

        // if closePanel flag is true, clicking button will close panel
        if(closePanel)
        {
            choice1.onClick.AddListener(ClosePanel);
            choice2.onClick.AddListener(ClosePanel);
            choice3.onClick.AddListener(ClosePanel);
        }

        descriptionText.SetText(description);

    }

    public void ClosePanel()
    {
        interactionPanelObject.SetActive(false);
        interactionEvent.Invoke(false);
    }

}
