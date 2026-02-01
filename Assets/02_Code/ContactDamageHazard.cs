using UnityEngine;
[RequireComponent(typeof(Collider))]

public class ContactDamageHazard : MonoBehaviour
{
    [SerializeField] private float contactDamage = 25;
    private void OnTriggerEnter(Collider other)
    {
        other.transform.root.GetComponent<Player>()?.healthSystem.TakeDamage(contactDamage);
    }
}
