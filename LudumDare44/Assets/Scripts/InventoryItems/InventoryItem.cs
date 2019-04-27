using UnityEngine;


    [CreateAssetMenu(fileName = "New Inventory Item", menuName = "Create Inventory Item", order = 51)]
    public class InventoryItem : ScriptableObject
    {
        public string Name;
        public Texture2D Texture;
        
    }