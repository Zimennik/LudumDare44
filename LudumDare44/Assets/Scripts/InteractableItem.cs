using System;
using System.Collections;
using System.Linq;
using cakeslice;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class InteractableItem : MonoBehaviour
{
    private Outline _outline;
    public float TimeToInteract = 0f;
    public float TimeToRepair = 5;
    public TextMeshPro InteractText;
    public TextMeshPro InteractProgress;

    public GameObject BrokenIndicator;

    public event Action<InteractableItem> InteractionStarted;
    public event Action<InteractableItem> InteractionComplete;

    private string defaultText = "Press E to ";
    public string ActionText = "Interact";
    public string InteractingText = "Interacting";

    public string Description = "";
    public bool DontStopPlayer = false;

    private Coroutine _interactCoroutine;
    // public GameObject MeshToOutline;

    private bool _isBroken = false;

    public event Action ItemDestroy;

    public bool IsBroken
    {
        get { return _isBroken; }
        set
        {
            _isBroken = value;
            BrokenIndicator?.SetActive(value);
        }
    }

    public void Start()
    {
        // InteractProgress.gameObject.SetActive(false);
        _outline = GetComponent<Outline>();
        if (_outline != null)
        {
            _outline.enabled = false;
        }
    }

    public virtual void Interact()
    {
        if (IsBroken && GameManager.Instance.Shelter.InventoryItems.FirstOrDefault(x => x.Name == "Toolbox") == null)
        {
            MessageBox.Instance.ShowText("You need Toolbox to repair this item");
            return;
        }


        if (_interactCoroutine != null) return;
        _interactCoroutine = StartCoroutine(Interacting());
    }

    public virtual void OnInteractFinished()
    {
    }

    public void OnRepairFinished()
    {
        IsBroken = false;
        //change Model or animation 
    }

    public virtual void Select()
    {
        if (_outline != null)
        {
            _outline.enabled = true;
        }

        string text = (string.IsNullOrEmpty(Description) ? "" : "\n" + Description + "\n") +defaultText+
                      (IsBroken ? "repair" : ActionText);
        MessageBox.Instance.ShowInteractText(text);

        //InteractText.gameObject.SetActive(true);
        //InteractText.text = defaultText + (IsBroken ? "repair" : ActionText);
    }

    public virtual void Deselect()
    {
        if (_outline != null)
        {
            _outline.enabled = false;
        }
        MessageBox.Instance.HideInteractText();
        // InteractText.gameObject.SetActive(false);
    }

    public void Refresh()
    {
        //test
        Select();
    }

    public virtual void NextDay()
    {
    }

    IEnumerator Interacting()
    {
        if (InteractionStarted != null) InteractionStarted(this);
        //  InteractProgress.gameObject.SetActive(true);


        DateTime startTime = DateTime.Now;
        DateTime finishTime = startTime.AddSeconds(IsBroken ? TimeToRepair : TimeToInteract);

        while (DateTime.Now < finishTime)
        {
            MessageBox.Instance.ShowProgress((IsBroken ? "Repairing" : InteractingText) + ": " +
                                             (finishTime - DateTime.Now).TotalSeconds.ToString("0") + "s");

            //    InteractProgress.text = (IsBroken ? "Repairing" : InteractingText) + ": " +
            //                            (finishTime - DateTime.Now).TotalSeconds.ToString("0") + "s";
            yield return new WaitForSeconds(0.1f);
        }
        // InteractProgress.gameObject.SetActive(false);

        MessageBox.Instance.HideProgress();
        _interactCoroutine = null;

        if (!IsBroken)
        {
            OnInteractFinished();
        }
        else
        {
            OnRepairFinished();
            Refresh();
        }

        if (InteractionComplete != null) InteractionComplete(this);
    }

    public void OnDestroy()
    {
        if (ItemDestroy != null) ItemDestroy();
    }
}