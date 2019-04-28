using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public const int MAX_HEALTH = 20;
    public const float DISTANCE_TO_INTERACT = 1.5f;
    public List<InventoryItem> Inventory = new List<InventoryItem>();


    public InteractableItem _currentSelected;

    public Camera PlayerCamera;
    public FirstPersonAIO FpsController;

    //UI
    public Image HealthBar;

    public Image HealthBackground;

    public Slider HungryBar;
    public Slider ThirstyBar;
    public Slider TemperatureBar;


    private int _health;
    private int _hungry;
    private int _thirsty;
    private int _temperature;

    public LayerMask LayerMask;

    public Image InvItem1;
    public Image InvItem2;
    public Image InvItem3;

    public bool CanInteract = true;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = Mathf.Clamp(value, 0, MAX_HEALTH);
            HealthBar.fillAmount = (float) _health / (float) MAX_HEALTH;

            HealthBar.rectTransform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.3f, 1, 0.3f);
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

    public void AddItem(InventoryItem item)
    {
        Inventory.Add(item);

        if (Inventory.Count == 1)
        {
            InvItem1.gameObject.SetActive(true);
            InvItem1.sprite = item.Texture;
        }
        if (Inventory.Count == 2)
        {
            InvItem2.gameObject.SetActive(true);
            InvItem2.sprite = item.Texture;
        }
        if (Inventory.Count == 3)
        {
            InvItem3.gameObject.SetActive(true);
            InvItem3.sprite = item.Texture;
        }
    }

    public void RemoveItem(InventoryItem item)
    {
        Inventory.Remove(item);
    }

    public void MoveItemToShelter(InventoryItem item)
    {
        GameManager.Instance.Shelter.AddItem(item);
        RemoveItem(item);
    }

    public void Reset()
    {
        Health = MAX_HEALTH;
        Hungry = 100;
        Thirsty = 100;
        Inventory.Clear();
        GoInside();
    }

    public void NextDay()
    {
        Health += GameManager.Instance.Shelter.Temperature>1 ? 10 : 3;
        Hungry -= 32;    //every 4 days
        Thirsty -= 48;   //every 2 days
    }


    private Coroutine loosingHealth;

    public void GoInside()
    {
        if (loosingHealth != null)
        {
            StopCoroutine(loosingHealth);
            loosingHealth = null;
        }

        while (Inventory.Count > 0)
        {
            MoveItemToShelter(Inventory.Last());
        }
        InvItem1.gameObject.SetActive(false);
        InvItem2.gameObject.SetActive(false);
        InvItem3.gameObject.SetActive(false);
    }

    public void GoOutside()
    {
        if (loosingHealth == null)
        {
            loosingHealth = StartCoroutine(LoosingHealth());
        }
    }

    IEnumerator LoosingHealth()
    {
        while (Health > 0)
        {
            yield return new WaitForSeconds(1);
            Health--;
        }
    }

    public void Update()
    {
        DeselectCurrent();

        if (!CanInteract) return;

        Ray ray = PlayerCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000, LayerMask))
        {
            InteractableItem item = hit.transform.GetComponent<InteractableItem>();

            if (item != null && Vector3.Distance(transform.position, hit.point) <= DISTANCE_TO_INTERACT)
            {
                if (_currentSelected != item)
                {
                    _currentSelected = item;
                    _currentSelected.InteractionStarted += OnInteractionStarted;
                    _currentSelected.InteractionComplete += OnInteractionComplete;
                    _currentSelected.ItemDestroy += OnItemWasDestroyed;
                    item.Select();
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            _currentSelected?.Interact();
        }
    }

    private void DeselectCurrent()
    {
        if (_currentSelected != null)
        {
            _currentSelected.InteractionStarted -= OnInteractionStarted;
            _currentSelected.InteractionComplete -= OnInteractionComplete;
            _currentSelected.ItemDestroy -= OnItemWasDestroyed;

            _currentSelected.Deselect();
            _currentSelected = null;
        }
    }

    public void OnItemWasDestroyed()
    {
        DeselectCurrent();
    }

    public void OnInteractionStarted(InteractableItem item)
    {
        if (item.DontStopPlayer) return;
        FpsController.enableCameraMovement = false;
        FpsController.playerCanMove = false;
    }

    public void OnInteractionComplete(InteractableItem item)
    {
        if (item.DontStopPlayer) return;
        FpsController.enableCameraMovement = true;
        FpsController.playerCanMove = true;
    }
}