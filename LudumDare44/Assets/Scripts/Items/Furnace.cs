using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Furnace : InteractableItem
{
    public const int WOOD_PER_DAY = 1;

    public GameObject Burning;

    private bool _isActive;

    public bool IsActive
    {
        get { return _isActive; }
        set
        {
            _isActive = value;
            Burning.SetActive(value);
        }
    }

    public override void Interact()
    {
        if (GameManager.Instance.Shelter.InventoryItems.Count(x => x.Name == "Wood") >= WOOD_PER_DAY)
        {
            base.Interact();
        }
        else
        {
            MessageBox.Instance.ShowText("You need at least 1 wood");
        }
    }


    public override void OnInteractFinished()
    {
        base.OnInteractFinished();
        var shelter = GameManager.Instance.Shelter;

        for (int i = 0; i < WOOD_PER_DAY; i++)
        {
            var wood = shelter.InventoryItems.FirstOrDefault(x => x.Name == "Wood");
            if (wood != null)
            {
                shelter.RemoveItem(wood);
            }
        }

        GameManager.Instance.Shelter.Temperature += 100;
    }
}