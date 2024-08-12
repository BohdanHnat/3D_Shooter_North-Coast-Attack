using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerBag
{
    int ammoInStock { get; }
    int startingAmmoAmount { get; }
    public void OnAmmoSet(int amount);
    public void OnAmmoIncrease(int amount);
    public void OnAmmoDecrease(int amount);
    public void OnStartingAmmoSet(int amount);
}
public class PlayerBag : IPlayerBag
{
    public int ammoInStock { get; private set; }
    public int startingAmmoAmount { get; private set; }
    public void OnAmmoSet(int amount)
    {
        ammoInStock = amount;
    }
    public void OnAmmoIncrease(int amount)
    {
        ammoInStock += amount;
    }
    public void OnAmmoDecrease(int amount)
    {
        ammoInStock -= amount;
    }
    public void OnStartingAmmoSet(int amount)
    {
        startingAmmoAmount = amount;
        ammoInStock = amount;
    }
}
