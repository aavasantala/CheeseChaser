using UnityEngine;

public class PushBox : MonoBehaviour
{

    [SerializeField] private CharacterController CC;
    private float pushPower = 4;
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body=hit.collider.attachedRigidbody;
        if(body == null || body.isKinematic)
        {
            return;
        }
        if (hit.moveDirection.y<-0.3f)
        {
            return;
        }
        Vector3 pushDir=new Vector3(hit.moveDirection.x,0,hit.moveDirection.z);
        body.linearVelocity = pushDir * pushPower;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
