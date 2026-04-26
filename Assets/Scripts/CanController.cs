using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CanController : MonoBehaviour
{
    public Boolean moved = false;
    public UIDocument Fader;
    public GameObject can;
    Collider m_ObjectCollider;
    public Collider cheaterCollider;
    public Image fade;
    public MouseController mouseController;
    public HelpText helpText;

    void Start()
    {
        m_ObjectCollider = GetComponent<Collider>();
        var root = Fader.rootVisualElement;
        fade = root.Q<Image>("fade"); 
    }

    private void Update()
    {
        //if can hasn't been moved and mouse is small, the can acts as a full collider instead of a trigger, else -> it is a trigger
        if (!moved)
        {
            if (mouseController.isSmall)
            {
                m_ObjectCollider.isTrigger = false;
            }
            else
            {
                m_ObjectCollider.isTrigger = true;
            }
        }  
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !mouseController.isSmall)
        {
            //disables player input
            Debug.Log("can has been touched");
            helpText.canMoved = true;
            moved = true;
            mouseController.enabled = false;
            //this could be removed, just put the startcoroutine here
            MakeCanFall();

            
            
        }
    }

    void MakeCanFall()
    {
        //this triggers a fade out, fade in where the can moves
        StartCoroutine(waiter(1f));
    }
    IEnumerator waiter(float duration)
    {
        //fade is thing that makes the fade
        fade.style.opacity = 0;
        yield return null;

        //turn screen black
        fade.style.opacity = 1;
        yield return new WaitForSeconds(1f);

        //og position of the cereal box
        //4.151 5.585 2.146   rot  -.178.455 46.97 90   scale  40.77999

        //new
        //1 1.87 -2.15  rot  -178.455 6.93 90 scale 36.3 48.6 58

        //change position
        Vector3 canPos = Vector3.zero;
        Vector3 canRot = Vector3.zero;
        canPos.x = 1f; canPos.y = 1.87f; canPos.z = -2.15f;
        canRot.x = -178.455f; canRot.y = 6.93f; canRot.z = 90f;
        this.transform.localPosition = canPos;
        this.transform.localEulerAngles = canRot;

        //disable the trigger for can & turn off the invisible wall over the crack
        m_ObjectCollider.isTrigger = false;
        cheaterCollider.enabled = false;
        yield return new WaitForSeconds(1f);

        //screen back to normal, player inputs are back
        fade.style.opacity = 0;
        mouseController.enabled = true;
        yield return new WaitForSeconds(1f);
    }
}
