
public class Rock : Interactable
{
    private DescriptionController descriptionController;

    private void Start()
    {
        interactableName = "Rock";
        
        descriptionController = FindObjectOfType<DescriptionController>();
    }

    public override void Interact()
    {
        descriptionController.SendMessage("updateDescription", "You see a rock. It's just a rock.");
    }
}
