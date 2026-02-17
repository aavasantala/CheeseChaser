using UnityEngine;

public interface IInteractable
{
    public bool Interactable { get; set; }
    public void OnInteract();
    
}
