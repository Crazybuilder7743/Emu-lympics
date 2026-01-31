using UnityEngine;

public class ContactDamageHazard : MonoBehaviour
{
    [SerializeField] private float contactDamage = 25;
    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<PlayerHealth>()?.TakeDamage(contactDamage);
    }
}
