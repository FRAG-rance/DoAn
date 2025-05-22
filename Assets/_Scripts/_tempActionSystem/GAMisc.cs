using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAMisc 
{
    
}

public class SpendEconGA : GameAction
{
    public int Amount { get; set; }
    public SpendEconGA(int amount)
    {
        Amount = amount;
    }
}
