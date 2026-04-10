using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class CanController : MonoBehaviour
{
    public UIDocument Fader;
    public GameObject can;
    Collider m_ObjectCollider;
    public Collider cheaterCollider;
    public Image fade;
    public MouseController mouseController;

    void Start()
    {
        m_ObjectCollider = GetComponent<Collider>();
        var root = Fader.rootVisualElement;
        fade = root.Q<Image>("fade");
        
        
        
    }
    private void OnTriggerEnter(Collider other)
    {

        
        if (other.CompareTag("Player") && !mouseController.isSmall)
        {
            
            Debug.Log("can has been touched");
            MakeCanFall();
        }


    }

    void MakeCanFall()
    {

        
        
        
        

        StartCoroutine(waiter(1f));
        
        
        m_ObjectCollider.isTrigger = false;
        cheaterCollider.enabled = false;

        
        
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
        yield return new WaitForSeconds(1f);
        fade.style.opacity = 0;
        yield return new WaitForSeconds(1f);

        


    }
}
