using System.Drawing;
using UnityEngine;

public class PushScript : MonoBehaviour
{

    public Rigidbody rb;
    public bool Pushable;
    public int sizeMouse;
    



   

// Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Pushable = false;
        rb.isKinematic = true;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Pushable == false)
        {
            Pushable = true;
            rb.isKinematic = false;
        }
        else
        {
            Pushable = false;
            rb.isKinematic = true;
        }
    }
    
    void OnMouseChangeSize()
    {

    }
    
    
        
    
}
