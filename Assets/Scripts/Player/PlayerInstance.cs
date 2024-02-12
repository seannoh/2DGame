using UnityEngine;
using UnityEngine.Events;

public class PlayerInstance : MonoBehaviour
{
    public PlayerInfo playerInfo;
    private InteractionPanel interactionPanel;
    private DescriptionController descriptionController;
    // Start is called before the first frame update
    void Awake()
    {
        playerInfo = ScriptableObject.CreateInstance<PlayerInfo>();
        playerInfo.updateNumFlowers(0);
        playerInfo.updateNumBushes(0); 
    }

    void Start()
    {
          
        interactionPanel = InteractionPanel.Instance();
        interactionPanel.Choice("Hello you are pig. What do you want to do?", 
                                new UnityAction(Choice1), "Look for a friend", 
                                new UnityAction(Choice2), "See how to move", 
                                new UnityAction(Choice3), "Quit", false);
        descriptionController = FindObjectOfType<DescriptionController>();
    }

    private void Choice1(){
        Debug.Log("Choice 1: Look for a friend");
        descriptionController.SendMessage("updateDescription", "You start looking for a friend. ");
        interactionPanel.ClosePanel();
    }

    private void Choice2(){
        Debug.Log("Choice 2: See how to move");
        descriptionController.SendMessage("updateDescription", "Use WASD to move");
        interactionPanel.Choice("Hello you are pig. What do you want to do?", 
                                new UnityAction(Choice1), "Look for a friend", 
                                new UnityAction(Choice2), "See how to move", 
                                new UnityAction(Choice3), "Quit", false);
    }

    private void Choice3(){
        Debug.Log("Choice 3: Quit");
        Application.Quit();
    }

}
