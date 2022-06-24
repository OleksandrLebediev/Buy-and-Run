using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FinishZone : MonoBehaviour
{
    [SerializeField] private FinishLine _finishLine;
    [SerializeField] private Transform _breakpoint;
    [SerializeField] private MultiplierBoardRoad _boardRoad;
    [SerializeField] private Box _box;
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private AudioClip _jumpClip;
    [SerializeField] private AudioClip _fallingClip;
    [SerializeField] private AudioClip _winClip;
    [SerializeField] private AudioClip _fallenClip;

    private float _jumpSpeed = 1;
    private Player _player;
    private int _powerForce;

    private ItemTransmitter _itemTransmitter = new ItemTransmitter();
    private CameraHendler _cameraHendler;
    private PlayerWallet _playerWallet;

    public event UnityAction LevelCompleted;

    private void OnEnable()
    {
        _finishLine.Finished += OnPlayerFinished;
    }

    private void OnDisable()
    {
        _finishLine.Finished -= OnPlayerFinished;
    }

    public void Initialize(CameraHendler cameraHendler, PlayerWallet wallet)
    {
        _cameraHendler = cameraHendler;
        _playerWallet = wallet;
    }

    private void OnPlayerFinished(Player player)
    {
        _player = player;
        _powerForce = player.PriceAllItems;
        _player.OnFinishEntered();
        _boardRoad.SetTarget(player.transform);
        //StartCoroutine(_boardRoad.BuildRoadRealTimeCoroutine(player.transform));
        StartCoroutine(AwardScenario(player));
    }

    private IEnumerator AwardScenario(Player player)
    {
        _cameraHendler.ChangeVirtualCamera();   

        yield return player.transform.DOMove(_breakpoint.position, 1).WaitForCompletion();
        player.PlayerAnimator.OnPushAnimation(false);

        yield return _itemTransmitter.MultiTransmittingCoroutine(player.ShopingCart, _box);
        yield return _box.CloseCoroutine();
        player.PriceDisplay.Hide();
        player.ShopingCart.Hide();
        Vector3 targetJump = _box.transform.position;
        targetJump.y += 2;

        player.Tern();



        yield return JupmAnimation(player, targetJump);
        yield return new WaitForSeconds(0.5f);

        yield return JupmAnimation(player, targetJump);

        yield return new WaitForSeconds(0.5f);

        yield return JupmAnimation(player, targetJump);

        yield return new WaitForSeconds(0.5f);

        yield return _box.OpenCoroutine();

        _box.ItemsExplosion(_powerForce);
        _player.EnablePhysics();
        _player.ForceUP(_powerForce);
        _player.PlayerAnimator.OnFalling(true);
        _audioSource.PlayOneShot(_fallingClip);


        while (true)
        {
            if (_player.Rigidbody.velocity.y < 0)
            {
                _boardRoad.ActivateBoard(_player.transform.position.y);
                break;
            }
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);
        player.PlayerAnimator.OnFlatImpact(true);
        yield return new WaitForSeconds(0.15f);
        _audioSource.PlayOneShot(_fallenClip);

        yield return new WaitForSeconds(2.5f);

        _boardRoad.ActivateConfetti();
        _audioSource.PlayOneShot(_winClip);

        yield return new WaitForSeconds(2.5f);

        _playerWallet.MultiplyMoney(_boardRoad.GetMultiplier());
        LevelCompleted?.Invoke();
    }


    private IEnumerator JupmAnimation(Player player, Vector3 target)
    {
        player.PlayerAnimator.OnJumping(false);
        player.PlayerAnimator.OnGrounded(false);

        player.transform.DOJump(target, 4, 1, _jumpSpeed);
        player.PlayerAnimator.OnJumping(true);

        _audioSource.PlayOneShot(_jumpClip);

        yield return new WaitForSeconds(_jumpSpeed - 0.5f);

        player.PlayerAnimator.OnGrounded(true);
        player.PlayerAnimator.OnJumping(false);
    }
}
