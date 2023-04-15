using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class CameraAndAudioDisabler :  NetworkBehaviour, IPlayerLeft
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Object.HasInputAuthority)
        {
            Camera localCamera = GetComponentInChildren<Camera>();

            localCamera.enabled = false;

            AudioListener localListener = GetComponentInChildren<AudioListener>();
            localListener.enabled = false;
        }
    }
    public void PlayerLeft(PlayerRef player)
    {
        if (player == Object.InputAuthority)
            Runner.Despawn(Object);
    }

}
