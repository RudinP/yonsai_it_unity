using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public GameObject cursor;
    public PlayerController playerCtrl;

    private void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            cursor.transform.position = new Vector3(hit.point.x, 0.5f, hit.point.z);

            if (playerCtrl.playerState != PlayerState.Dead)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Vector3 target = new Vector3(hit.point.x, 0.5f, hit.point.z);
                    playerCtrl.lookDirection = target - playerCtrl.transform.position;
                    playerCtrl.LookUpdate(true);
                    playerCtrl.StartCoroutine(nameof(playerCtrl.Shot));
                }
                if (Input.GetMouseButtonDown(1))
                {
                    Vector3 target = new Vector3(
                        hit.point.x,
                        transform.position.y,
                        hit.point.z);
                    playerCtrl.lookDirection = target - playerCtrl.transform.position;
                    playerCtrl.lookDirection.y = 0;

                    playerCtrl.dest = target;

                    playerCtrl.speed = playerCtrl.walkSpeed;
                    playerCtrl.playerState = PlayerState.Run;
                    playerCtrl.goDest = true;
                }
            }
        }
    }
}
