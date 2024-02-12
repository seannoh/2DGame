using UnityEngine;
using UnityEngine.SceneManagement;

public class Boar : Interactable
{
    private DescriptionController descriptionController;
    private PlayerInfo playerInfo;
    private int friendScore = 0;

    private int smellScore = 0;

    private void Start()
    {
        interactionPanel = InteractionPanel.Instance();
        descriptionController = FindObjectOfType<DescriptionController>();
    }

    public override void Interact()
    {
        playerInfo = FindObjectOfType<PlayerInstance>().playerInfo;
        smellScore = playerInfo.flowersSmelled - playerInfo.bushesSmelled;
        if(smellScore <= 0)
        {
            descriptionController.SendMessage("updateDescription", "The other pig doesn't like the way you smell. You need to smell better to be friends.");
            return;
        }
        interactableName = "Boar";
        description = "You see another pig. What do you want to do?";
        Choice1Text = $"Give them your flowers ({playerInfo.getNumFlowers()})";
        Choice2Text = $"Give them your bushes ({playerInfo.getNumBushes()})";
        Choice3Text = "Leave them alone";
        Choice1Action += Choice1;
        Choice2Action += Choice2;
        Choice3Action += Choice3;
        closePanel = false;
        base.Interact();
    }

    public override void Choice1()
    {
        Debug.Log("Choice 1: Give them your flowers");
        updateFriendScore(playerInfo.getNumFlowers());
        playerInfo.updateNumFlowers(0);
        if (friendScore > 5)
        {
            endGame();
            return;
        } else if (friendScore > 4){
            descriptionController.SendMessage("updateDescription", "The other pig is happy with your gift. You are almost friends!");
        } else if (friendScore > 3){
            descriptionController.SendMessage("updateDescription", "The other pig is happy with your gift. You are on your way to being friends!");
        } else if (friendScore > 2){
            descriptionController.SendMessage("updateDescription", "The other pig is happy with your gift. You are making progress!");
        } else if (friendScore > 1){
            descriptionController.SendMessage("updateDescription", "The other pig is happy with your gift. You are getting there!");
        } else {
            descriptionController.SendMessage("updateDescription", "The other pig is happy with your gift. You are off to a good start!");
        }
        Interact();
    }

    public override void Choice2()
    {
        Debug.Log("Choice 2: Give them your bushes");
        updateFriendScore(-playerInfo.getNumBushes());
        playerInfo.updateNumBushes(0);
        if (friendScore > 5)
        {
            endGame();
            return;
        } else if (friendScore > 4){
            descriptionController.SendMessage("updateDescription", "The other pig didn't like your gift.");
        } else if (friendScore > 3){
            descriptionController.SendMessage("updateDescription", "The other pig didn't like your gift.");
        } else if (friendScore > 2){
            descriptionController.SendMessage("updateDescription", "The other pig didn't like your gift.");
        } else if (friendScore > 1){
            descriptionController.SendMessage("updateDescription", "The other pig didn't like your gift.");
        } else {
            descriptionController.SendMessage("updateDescription", "The other pig didn't like your gift.");
        }
        Interact();
    }

    public override void Choice3()
    {
        Debug.Log("Choice 3: Leave them alone");
        descriptionController.SendMessage("updateDescription", "You leave the other pig alone.");
        interactionPanel.ClosePanel();
    }

    private void updateFriendScore(int score)
    {
        friendScore += score;
        if(friendScore < 0)
        {
            friendScore = 0;
        }
    }

    private void endGame()
    {
        description = "You have made a friend! You win!";
        Choice1Text = "Play again";
        Choice1Action += () => {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);};
        Choice2Text = "Quit";
        Choice2Action += () => {Application.Quit();};
        Choice3Text = "";
        Choice3Action += () => {};
        base.Interact();
    }
}
