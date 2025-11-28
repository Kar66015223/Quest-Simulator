using TMPro;
using UnityEngine;

public class Diamond : Item
{
    public AudioClip collectSound;

    public override void OnCollect(Player player)
    {
        base.OnCollect(player);

        GameManager.Instance.AddDiamond(1);

        AudioSource playerSource = player.GetComponent<AudioSource>();
        playerSource.PlayOneShot(collectSound);

        Destroy(gameObject);
    }
}
