using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

enum PlayerButtons
{
    Jump = 0,
}

public struct NetworkInputData : INetworkInput
{
    public Vector3 dir;
    public NetworkButtons Buttons;
}
