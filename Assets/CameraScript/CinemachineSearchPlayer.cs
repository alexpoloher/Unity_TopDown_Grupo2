using Unity.Cinemachine;
using UnityEngine;

public class CinemachineSearchPlayer : MonoBehaviour
{
    private CinemachineCamera vcam;

    void Start()
    {
        vcam = GetComponent<CinemachineCamera>();

        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null && vcam != null)
        {
            vcam.Follow = player.transform;
            //vcam.LookAt = player.transform;
        }
    }
}
