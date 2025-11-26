using TMPro;
using UnityEngine;

public class Coin : Item
{
    public int coinAmount = 0;
    public AudioClip SoundCoin;

    public TMP_Text uiCoinAmount;
    public override void OnCollect(Player player)
    {
        base.OnCollect(player);

        GameManager.Instance.AddCoin(1);

        AudioSource playerSource = player.GetComponent<AudioSource>();
        playerSource.PlayOneShot(SoundCoin);

        Destroy(gameObject);
    }
}
