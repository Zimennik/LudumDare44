using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Shelter : MonoBehaviour
{
    public List<InventoryItem> InventoryItems = new List<InventoryItem>();


    public Slider TemperatureBar;
    public Slider EnergyBar;
    private int _temperature;

    public int Temperature
    {
        get { return _temperature; }
        set
        {
            _temperature = Mathf.Clamp(value, 0, 100);
            TemperatureBar.value = _temperature;
        }
    }

    private int _energy;

    public int Energy
    {
        get { return _energy; }
        set
        {
            _energy = Mathf.Clamp(value, 0, 100);
            EnergyBar.value = _energy;
        }
    }


    public InventoryItem FoodPrefab;

    
    //Buildings
    public Lamp Lamp;

    public Farm Farm;
    
    
    public void Start()
    {
        AddItem(FoodPrefab);
    }


    public void BuildLamp()
    {
        var lamp = InventoryItems.FirstOrDefault(x => x.Name == "Lightbulb");
        if (lamp != null)
        {
            RemoveItem(lamp);
           Lamp.gameObject.SetActive(true);
        }
        else
        {
            MessageBox.Instance.ShowText("You don't have lightbulb. How did you do this?");
        }
    }

    public ItemHolder FoodShelf;
    public ItemHolder WaterShelf;
    public ItemHolder FuelShelf;
    public ItemHolder WoodShelf;
    public ItemHolder LightBulbsShelf;
    public ItemHolder ToolboxShelf;
    public ItemHolder NailsShelf;
    public ItemHolder WiresShelf;

    //Buildable items
    public ItemHolder BedHolder;

    public ItemHolder FarmHolder;
    public ItemHolder WaterFilterHolder;
    public ItemHolder LampsHolder;
    public ItemHolder FurnaceHolder;
    public ItemHolder HeaterHolder;




    //
    public InteractableItem WaterDispenser;


    public void AddItem(InventoryItem item)
    {
        InventoryItems.Add(item);


        RefreshHolders(item.Name);
    }

    public void RemoveItem(InventoryItem item)
    {
        InventoryItems.Remove(item);


        RefreshHolders(item.Name);
    }


    public void RefreshHolders(string itemName)
    {
        switch (itemName)
        {
            case "Food":
                FoodShelf.SetCount(InventoryItems.Count(x => x.Name == "Food"));
                break;
            case "Water":
                WaterShelf.SetCount(InventoryItems.Count(x => x.Name == "Water"));
                break;
            case "Fuel":
                FuelShelf.SetCount(InventoryItems.Count(x => x.Name == "Fuel"));
                break;
            case "Wood":
                WoodShelf.SetCount(InventoryItems.Count(x => x.Name == "Wood"));
                break;
            case "Lightbulb":
                LightBulbsShelf.SetCount(InventoryItems.Count(x => x.Name == "Lightbulb"));
                break;
            case "Toolbox":
                ToolboxShelf.SetCount(InventoryItems.Count(x => x.Name == "Toolbox"));
                break;
        }
    }

    public bool HasItem(string itemName)
    {
        return InventoryItems.FirstOrDefault(x => x.Name == "itemName") != null;
    }

    public void Reset()
    {
        Energy = 0;
        Temperature = 0;

        RefreshHolders("Food");
        RefreshHolders("Water");
        RefreshHolders("Fuel");
        RefreshHolders("Wood");
        //  RefreshHolders("Lightbulb");
        RefreshHolders("Toolbox");
    }

    public void NextDay()
    {
        //var waterFilter = InventoryItems.FirstOrDefault(x => x.Name == "WaterFilter");
        // if (waterFilter != null)
        {
            WaterDispenser.NextDay();
        }

        if (Lamp.isActiveAndEnabled && Lamp.IsOn)
        {
            Lamp.NextDay();
        }

        if (Farm.isActiveAndEnabled)
        {
            Farm.NextDay();
        }
        

        Temperature -= 33;

        //  Energy -= LampsCount
    }


    public void Update()
    {
        //test
        if (!Lamp.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                BuildLamp();
            }
        }
    }
}