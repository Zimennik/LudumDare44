using System.Linq;

public class Generator : InteractableItem
{
    public override void Interact()
    {
        if (GameManager.Instance.Shelter.InventoryItems.Count(x => x.Name == "Fuel") >= 1)
        {
            base.Interact();
        }
        else
        {
            MessageBox.Instance.ShowText("You need at least 1 fuel canister");
        }
    }


    public override void OnInteractFinished()
    {
        base.OnInteractFinished();
        var shelter = GameManager.Instance.Shelter;

        
            var fuel = shelter.InventoryItems.FirstOrDefault(x => x.Name == "Fuel");
            if (fuel != null)
            {
                shelter.RemoveItem(fuel);
                GameManager.Instance.Shelter.Energy += 50;
            }

        
    }
    
}
