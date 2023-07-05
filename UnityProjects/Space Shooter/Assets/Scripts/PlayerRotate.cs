using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : NetworkBehaviour
{
    public float rotSpeed = 200f;

    float mx = 0;

    public override void FixedUpdateNetwork()
    {
        if (GameManager.gm.gState != GameManager.GameState.Run)
            return;

        float mouse_X = Input.GetAxis("Mouse X");

        mx += mouse_X * rotSpeed * Time.deltaTime;

        transform.eulerAngles = new Vector3(0, mx, 0);
    }
}
