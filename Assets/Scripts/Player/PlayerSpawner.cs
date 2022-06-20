using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSpawner : MonoBehaviour, IPlayerEvents
{
    [SerializeField] private Player _playerTemplate;
    [SerializeField] private Player _player;

    public event UnityAction PlayerDied;

    public Player Spawn()
    {
        if (_player != null)
            RemovePlayer(_player);

        _player = Instantiate(_playerTemplate);
        _player.Died += OnPlayerDied;
        return _player;
    }

    private void OnPlayerDied()
    {
        PlayerDied?.Invoke();
    }

    private void RemovePlayer(Player player)
    {
        Destroy(player.gameObject);
    }
}
