using System;
using System.Collections;
using cakeslice;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Outline))]
[RequireComponent(typeof(Collider))]
public abstract class InteractableItem : MonoBehaviour
{
    private Outline _outline;
    public float TimeToInteract = 0f;
    public GameObject InteractText;
    public TextMeshPro InteractProgress;

    public event Action InteractionStarted;
    public event Action InteractionComplete;

    private Coroutine _interactCoroutine;

    public void Start()
    {
        InteractProgress.gameObject.SetActive(false);
        _outline = GetComponent<Outline>();
        _outline.enabled = false;
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
        if (_outline == null) return;
        _outline.enabled = true;
        InteractText.SetActive(true);
    }

    public virtual void Deselect()
    {
        if (_outline == null) return;
        _outline.enabled = false;
        InteractText.SetActive(false);
    }


    IEnumerator Interacting()
    {
        if (InteractionStarted != null) InteractionStarted();
        InteractProgress.gameObject.SetActive(true);
        
        DateTime startTime = DateTime.Now;
        DateTime finishTime = startTime.AddSeconds(TimeToInteract);

        while (DateTime.Now < finishTime)
        {
            InteractProgress.text = (finishTime - DateTime.Now).TotalSeconds.ToString("0") + "s";
            yield return new WaitForSeconds(0.1f);
        }
        InteractProgress.gameObject.SetActive(false);
        _interactCoroutine = null;
        OnInteractFinished();
        if (InteractionComplete != null) InteractionComplete();
    }
}