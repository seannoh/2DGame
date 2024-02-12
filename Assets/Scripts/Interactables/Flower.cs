using UnityEngine;

public class Flower : Interactable
{
    private DescriptionController descriptionController;
    private PlayerInfo playerInfo;

    private void Start()
    {
        interactableName = "Flower";
        description = "You see a flower. What do you want to do?";
        Choice1Text = "Pick the flower";
        Choice2Text = "Smell the flower";
        Choice3Text = "Leave the flower";
        Choice1Action += Choice1;
        Choice2Action += Choice2;
        Choice3Action += Choice3;
        playerInfo = FindObjectOfType<PlayerInstance>().playerInfo;
        
        interactionPanel = InteractionPanel.Instance();
        descriptionController = FindObjectOfType<DescriptionController>();
    }

    public override void Interact()
    {
        base.Interact();
    }

    public override void Choice1()
    {
        Debug.Log("Choice 1: Pick the flower");
        playerInfo.updateNumFlowers(playerInfo.getNumFlowers() + 1);
        descriptionController.SendMessage("updateDescription", $"You picked the flower. You now have {playerInfo.numFlowers} flower(s).");
        gameObject.SetActive(false);
    }

    public override void Choice2()
    {
        Debug.Log("Choice 2: Smell the flower");
        descriptionController.SendMessage("updateDescription", "You smell the flower. It smells nice.");
        playerInfo.updateFlowersSmelled(playerInfo.flowersSmelled + 1);
    }

    public override void Choice3()
    {
        Debug.Log("Choice 3: Leave the flower");
        descriptionController.SendMessage("updateDescription", "You leave the flower alone.");
    }
}
