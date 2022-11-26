using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : AB_Collectible
{
    protected override void ApplyEffect(GameObject playerObject)
    {
        playerObject.GetComponent<Collector>().addCoin();
        // Play coin sound
    }
}
