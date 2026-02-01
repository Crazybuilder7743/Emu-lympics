using UnityEngine;

public class ResetZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var c = other.GetComponentInParent<SplineSpeedController>();
        c?.ResetToLastCheckpoint();
    }
}
