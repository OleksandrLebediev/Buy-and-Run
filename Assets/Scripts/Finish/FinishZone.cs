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

    private float _jumpSpeed = 1;
    private Player _player;
    private int _powerForce = 50;

    private ItemTransmitter _itemTransmitter = new ItemTransmitter();
    private CameraHendler _cameraHendler;

    public event UnityAction LevelCompleted;

    private void OnEnable()
    {
        _finishLine.Finished += OnPlayerFinished;
    }

    private void OnDisable()
    {
        _finishLine.Finished -= OnPlayerFinished;
    }

    public void Initialize(CameraHendler cameraHendler)
    {
        _cameraHendler = cameraHendler;
    }

    private void OnPlayerFinished(Player player)
    {
        _player = player;
        StartCoroutine(AwardScenario(player));
    }

    private IEnumerator AwardScenario(Player player)
    {
        player.PlayerMovement.DisableMovement();
        player.PlayerPriceDisplay.Hide();
        _cameraHendler.ChangeVirtualCamera();   

        yield return player.transform.DOMove(_breakpoint.position, 1).WaitForCompletion();
        player.PlayerAnimator.OnPushAnimation(false);

        yield return _itemTransmitter.MultiTransmittingCoroutine(player.ShopingCart, _box);
        yield return _box.CloseCoroutine();
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

        yield return new WaitForSeconds(2.5f);

        _boardRoad.ActivateConfetti();

        yield return new WaitForSeconds(2.5f);
        LevelCompleted?.Invoke();

    }


    private IEnumerator JupmAnimation(Player player, Vector3 target)
    {
        player.PlayerAnimator.OnJumping(false);
        player.PlayerAnimator.OnGrounded(false);

        player.transform.DOJump(target, 4, 1, _jumpSpeed);
        player.PlayerAnimator.OnJumping(true);

        yield return new WaitForSeconds(_jumpSpeed - 0.5f);

        player.PlayerAnimator.OnGrounded(true);
        player.PlayerAnimator.OnJumping(false);
    }
}
