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
            
            Debug.Log("can has been touched");
            mouseController.enabled = false;
            MakeCanFall();
            moved = true;
        }


    }

    void MakeCanFall()
    {

        
        
        
        

        StartCoroutine(waiter(1f));
        
        
        

        
        
    }
    IEnumerator waiter(float duration)
    {
        
        fade.style.opacity = 0;
        yield return null;

        fade.style.opacity = 1;
        yield return new WaitForSeconds(1f);
        Vector3 canPos = Vector3.zero;
        Vector3 canRot = Vector3.zero;
        canPos.x = 8.727f; canPos.y = 1.842f; canPos.z = 7.27f;
        canRot.x = 0f; canRot.y = 0f; canRot.z = 90f;

        this.transform.position = canPos;
        this.transform.Rotate(canRot);
        m_ObjectCollider.isTrigger = false;
        cheaterCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        fade.style.opacity = 0;
        mouseController.enabled = true;
        yield return new WaitForSeconds(1f);

        


    }
}
