using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class PlayerBaker : Baker<Player>
{
    public override void Bake(Player authoring)
    {
        Debug.Log("hello baker");
    }
}
