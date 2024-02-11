using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    public string description;
    public string interactableName;

    public string Choice1Text, Choice2Text, Choice3Text;

    public InteractionPanel interactionPanel;

    protected UnityAction Choice1Action, Choice2Action, Choice3Action;

    protected bool closePanel = true;

    private void Start()
    {
        interactionPanel = InteractionPanel.Instance();
    }
    
    public virtual void Interact()
    {
        Debug.Log("Interacting with " + interactableName);


        interactionPanel.Choice(description, Choice1Action, Choice1Text, Choice2Action, Choice2Text, Choice3Action, Choice3Text, closePanel);
    }

    public virtual void Choice1(){}
    public virtual void Choice2(){}
    public virtual void Choice3(){}
}
