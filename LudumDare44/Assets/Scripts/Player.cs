using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public const int MAX_HEALTH = 20;
    public const float DISTANCE_TO_INTERACT = 1.5f;
    public List<InventoryItem> Inventory = new List<InventoryItem>();

    
    private InteractableItem _currentSelected;

    public Camera PlayerCamera;
    public FPSController FpsController;
    
    //UI
    public Slider HealthBar;

    public Slider HungryBar;
    public Slider ThirstyBar;
    public Slider TemperatureBar;


    private int _health;
    private int _hungry;
    private int _thirsty;
    private int _temperature;


    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, MAX_HEALTH);
            HealthBar.value = _health;
        }
    }

    public int Hungry
    {
        get { return _hungry; }
        set
        {
            _hungry = Mathf.Clamp(value, 0, 100);
            HungryBar.value = _hungry;
        }
    }

    public int Thirsty
    {
        get { return _thirsty; }
        set
        {
            _thirsty = Mathf.Clamp(value, 0, 100);
            ThirstyBar.value = _thirsty;
        }
    }

    public int Temperature
    {
        get { return _temperature; }
        set
        {
            _temperature = Mathf.Clamp(value, 0, 100);
            TemperatureBar.value = _temperature;
        }
    }

    
    public void Update()
    {
        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit))
        {
            InteractableItem item = hit.transform.GetComponent<InteractableItem>();
         
            if (item != null && Vector3.Distance(transform.position, hit.point) <= DISTANCE_TO_INTERACT)
            {
                if (_currentSelected != item)
                {
                    _currentSelected = item;
                    _currentSelected.InteractionStarted += OnInteractionStarted;
                    _currentSelected.InteractionComplete += OnInteractionComplete;
                    item.Select();
                }
            }
            else
            {
                if (_currentSelected != null)
                {
                    _currentSelected.InteractionStarted -= OnInteractionStarted;
                    _currentSelected.InteractionComplete -= OnInteractionComplete;
                    
                    _currentSelected.Deselect();
                    _currentSelected = null;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentSelected?.Interact();
        }
    }

    public void OnInteractionStarted()
    {
        FpsController.SetControl(false);
    }
    
    public void OnInteractionComplete()
    {
        
        FpsController.SetControl(true);
    }
}