using UnityEngine;

public class DamageControll : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            SaveDamage();
        }
    }

    void SaveDamage()
    {
        Player player = FindFirstObjectByType<PlayerControll>().Player;
        player.Vida -= 10;
    }
}
