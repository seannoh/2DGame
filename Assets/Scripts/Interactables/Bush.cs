using UnityEngine;

public class Bush : Interactable
{
    private DescriptionController descriptionController;
    private PlayerInfo playerInfo;

    private void Start()
    {
        interactableName = "Bush";
        description = "You see a bush. What do you want to do?";
        Choice1Text = "Pick the bush";
        Choice2Text = "Smell the bush";
        Choice3Text = "Leave the bush";
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
        Debug.Log("Choice 1: Pick the bush");
        playerInfo.updateNumBushes(playerInfo.getNumBushes() + 1);
        descriptionController.SendMessage("updateDescription", $"You picked the bush. You now have {playerInfo.numBushes} bush(es).");
        gameObject.SetActive(false);
    }

    public override void Choice2()
    {
        Debug.Log("Choice 2: Smell the bush");
        descriptionController.SendMessage("updateDescription", "You smell the bush. It smells stinky.");
        playerInfo.updateBushesSmelled(playerInfo.bushesSmelled + 1);
    }

    public override void Choice3()
    {
        Debug.Log("Choice 3: Leave the bush");
        descriptionController.SendMessage("updateDescription", "You leave the bush alone.");
    }
}
