using System.Collections;
using UnityEngine;

public class ItemControll : MonoBehaviour
{
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SaveItem();
            Destroy(this.gameObject);
        }
    }

    void SaveItem()
    {
        Player player = FindFirstObjectByType<PlayerControll>().Player;
        player.QuantidadeItens++;
    }
}
