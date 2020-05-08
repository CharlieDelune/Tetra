﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIBase : MonoBehaviour
{
    [SerializeField]
    protected Board board;
    [SerializeField]
    protected Player player;

    public void SetPlayer(Player p)
    {
        player = p;
    }

    public void SetBoard(Board b)
    {
        board = b;
    }

    public abstract bool TakeTurn();
}
