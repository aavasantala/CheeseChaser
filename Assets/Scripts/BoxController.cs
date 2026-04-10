using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BoxController : MonoBehaviour, IInteractable
{
    public HelpText helpText;
    public MouseController mouseController;
    public bool Interactable
    {
        get => _interactable;
        set
        {
            _interactable = value;
           _rigidbody.isKinematic = !_interactable; 
        }  
    }

    private bool _interactable = true;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        Interactable = true;
    }
    
    // TODO: Implement interact here
    public void OnInteract()
    {
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && mouseController.isSmall)
        {
            helpText.BucketText();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            helpText.TextOut();
        }
    }
}
