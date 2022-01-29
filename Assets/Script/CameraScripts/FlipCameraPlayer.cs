using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipCameraPlayer : MonoBehaviour
{
    [SerializeField] private CameraFollow scFollow;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform player;

    public bool reversKey = false;

    public void Flip()
    {
        if (scFollow.offset == new Vector3(0, 12, -10))
        {
            scFollow.offset = new Vector3(0, 12, 10);
            mainCamera.transform.eulerAngles = new Vector3(45, 180, 0);

            player.eulerAngles = new Vector3(0, 180, 0);

            reversKey = true;
        }
        else
        {
            scFollow.offset = new Vector3(0, 12, -10);
            mainCamera.transform.eulerAngles = new Vector3(45, 0, 0);

            player.eulerAngles = new Vector3(0, 0, 0);

            reversKey = false;
        }
    }
}
