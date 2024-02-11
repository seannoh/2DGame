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
        interactionEvent.Invoke(true);
        interactionPanelObject.SetActive(true);

        choice1.onClick.RemoveAllListeners();
        choice1.onClick.AddListener(choice1Action);
        choice1Text.SetText(choice1Desc);

        choice2.onClick.RemoveAllListeners();
        choice2.onClick.AddListener(choice2Action);
        choice2Text.SetText(choice2Desc);

        choice3.onClick.RemoveAllListeners();
        choice3.onClick.AddListener(choice3Action);
        choice3Text.SetText(choice3Desc);

        if(closePanel)
        {
            choice1.onClick.AddListener(ClosePanel);
            choice2.onClick.AddListener(ClosePanel);
            choice3.onClick.AddListener(ClosePanel);
        }

        descriptionText.SetText(description);
        choice1.gameObject.SetActive(true);
        choice2.gameObject.SetActive(true);
        choice3.gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        interactionPanelObject.SetActive(false);
        interactionEvent.Invoke(false);
    }

}
