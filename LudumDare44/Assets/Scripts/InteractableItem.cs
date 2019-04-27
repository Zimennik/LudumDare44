using System;
using System.Collections;
using cakeslice;
using TMPro;
using UnityEngine;

//[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Collider))]
public abstract class InteractableItem : MonoBehaviour
{
    private Outline _outline;
    public float TimeToInteract = 0f;
    public TextMeshPro InteractText;
    public TextMeshPro InteractProgress;

    public event Action InteractionStarted;
    public event Action InteractionComplete;

    private string defaultText = "Press E to ";
    public string ActionText = "Interact";
    public string InteractingText = "Interacting";
    
    private Coroutine _interactCoroutine;

    public bool IsBroken = false;

    public void Start()
    {
        InteractProgress.gameObject.SetActive(false);
        _outline = GetComponent<Outline>();
        if (_outline != null)
        {
            _outline.enabled = false;
        }
    }

    public virtual void Interact()
    {
        if(_interactCoroutine!=null)return;
        _interactCoroutine = StartCoroutine(Interacting());
    }

    public virtual void OnInteractFinished()
    {
    }

    public virtual void Select()
    {
        if (_outline != null)
        {
            _outline.enabled = true;
        }
        InteractText.gameObject.SetActive(true);
        InteractText.text = defaultText + (IsBroken ? "repair" : ActionText);
    }

    public virtual void Deselect()
    {
        if (_outline != null)
        {
            _outline.enabled = false;
        }
        InteractText.gameObject.SetActive(false);
    }


    IEnumerator Interacting()
    {
        if (InteractionStarted != null) InteractionStarted();
        InteractProgress.gameObject.SetActive(true);
        
        
        DateTime startTime = DateTime.Now;
        DateTime finishTime = startTime.AddSeconds(TimeToInteract);

        while (DateTime.Now < finishTime)
        {
            InteractProgress.text = InteractingText+": "+(finishTime - DateTime.Now).TotalSeconds.ToString("0") + "s";
            yield return new WaitForSeconds(0.1f);
        }
        InteractProgress.gameObject.SetActive(false);
        _interactCoroutine = null;
        OnInteractFinished();
        if (InteractionComplete != null) InteractionComplete();
    }
}