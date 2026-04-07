using System;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BoxDetector : MonoBehaviour
{
    [SerializeField] private BoxController boxToDetect;
    
    private BoxController _currentBoxController;
    private Sequence _sequence;
    
    private void OnTriggerEnter(Collider other)
    {
        //if (!other.CompareTag("Interactable")) return;
        
        //if (!other.TryGetComponent(out BoxController boxController)) return;
        //if(boxController != boxToDetect) return;
        
        //_currentBoxController = boxController;
        
        // Box move to place animation
        //_sequence = DOTween.Sequence();
        //_sequence.Join(_currentBoxController.transform.DOMove(transform.position, 0.3f));
        //_sequence.Join(_currentBoxController.transform.DORotate(transform.rotation.eulerAngles, 0.3f));
        
        // Set box interact to false so player cannot move it out of the way after 
        //boxController.Interactable = false;
        //LevelManager.Instance.onBoxInRightPlace?.Invoke();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        if (boxToDetect != null)
        {
            Gizmos.DrawSphere(boxToDetect.transform.position, 0.3f);
        }
    }
}
