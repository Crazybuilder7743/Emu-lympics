using UnityEngine;

public class CheckpointZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var c = other.GetComponentInParent<SplineSpeedController>();
        c?.SaveCheckpoint();
    }
}
