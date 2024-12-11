using UnityEngine;
using Unity.Netcode;

public class OwnerComponentManager : NetworkBehaviour
{
    [SerializeField] private Camera _camera; // This is your camera, assign it in the prefab
    [SerializeField] private PlayerView _playerView;
    [SerializeField] private AudioListener _audioListener;
    [SerializeField] private Canvas _canvas;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        if (!IsOwner) { return; } // ALL players will read this method, only player owner will execute past this line
        _camera.enabled = true; // only enable YOUR PLAYER'S camera, all others will stay disabled
        _playerView.enabled = true;
        _audioListener.enabled = true;
        _canvas.enabled = true;
    }
}