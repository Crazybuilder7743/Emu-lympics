using UnityEngine;
using UnityEngine.VFX;

public class LaserVFX : MonoBehaviour
{
    
    [SerializeField] VisualEffect vfx;
    [SerializeField] LayerMask destructableMask;
    private void Update()
    {
        if(Physics.Raycast(transform.position,transform.forward,out RaycastHit hitInfo)) 
        {
            if (hitInfo.collider.gameObject.layer == destructableMask) 
            {
                hitInfo.collider.gameObject.SetActive(false);
            }

            vfx.SetVector3("EndPoint",hitInfo.point);
        }
        else 
        {
        
            vfx.SetVector3("EndPoint",transform.position + (transform.forward*10));
        }
    }
}
