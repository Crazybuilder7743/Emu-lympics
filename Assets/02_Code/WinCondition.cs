using UnityEngine;

public class WinCondition : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var player =  other.transform.root.GetComponent<Player>();
        if (player.gameObject == InitLevel.player1obj.gameObject) 
        {
            InitLevel.Instance.Win(true);
        }
        else if (player.gameObject == InitLevel.player2obj.gameObject) 
        {
            InitLevel.Instance.Win(false);
        }
    }
}
