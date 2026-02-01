using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TickDamageHazard : MonoBehaviour
{
    [SerializeField] private float damagePerS;
    Player player;
    private void OnTriggerStay(Collider other)
    {
        if(player.gameObject != other.gameObject) 
        {
            player = other.transform.root.GetComponent<Player>();
        }

        player.healthSystem.TakeDamage (damagePerS*Time.deltaTime);
    }
}
