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

    public bool IsBedBuilded;
    public bool IsFarmBuilded;
    public bool IsFilterBuilded;
    public int LampsCount;
    public bool IsFurnaceBuilded;
    public bool IsElectroFurnaceBuilded;

    public void AddItem(InventoryItem item)
    {
        InventoryItems.Add(item);
    }

    public void RemoveItem(InventoryItem item)
    {
        InventoryItems.Remove(item);
    }

    public void RemoveItemFromInventory(string itemName)
    {
        InventoryItems.Remove(InventoryItems.FirstOrDefault(x => x.Name == itemName));
    }

    public bool HasItem(string itemName)
    {
        return InventoryItems.FirstOrDefault(x => x.Name == "itemName") != null;
    }

    public void Reset()
    {
        Energy = 0;
        Temperature = 0;
    }

    public void NextDay()
    {
        //  Energy -= LampsCount
    }
}