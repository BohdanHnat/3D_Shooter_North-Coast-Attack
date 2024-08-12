using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoKit : MonoBehaviour
{
    [SerializeField] private int ammoToAdd;
    private IPlayerBag playerBag;
    private void Start()
    {
        playerBag = Context.Instance.PlayerBag;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (playerBag.startingAmmoAmount >= (playerBag.ammoInStock + ammoToAdd)) playerBag.OnAmmoIncrease(ammoToAdd);
            else playerBag.OnAmmoSet(playerBag.startingAmmoAmount);

            Context.Instance.AudioSystem.PlaySFX(new AudioData("467610__triqystudio__pickupitem", volume: 0.4f));
            
            Destroy(gameObject);
        }
    }
}
