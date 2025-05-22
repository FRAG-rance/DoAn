using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameAction
{
    public List<GameAction> PreActions {  get; private set; } = new List<GameAction>();
    public List<GameAction> PerformActions { get; private set; } = new List<GameAction>();
    public List<GameAction> PostActions { get; private set; } = new List<GameAction>();

}
