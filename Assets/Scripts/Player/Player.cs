using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour
{
    [SerializeField] private ShopingCart _shopingCart;
    [SerializeField] private PlayerPriceDisplay _playerPriceDisplay;
    [SerializeField] private PlayerAudio _audio;
    [SerializeField] private Transform _body;


    private PlayerMovement _movement;
    private CapsuleCollider _collider;
    private Rigidbody _rigidbody;
    private PlayerAnimator _playerAnimator;
    private PlayerWallet _wallet;
    private AddedMoneyEffect _moneyEffect;
    private BoostSpeedEffect _boostEffect;
    private int _priceAllItems;

    public PlayerMovement PlayerMovement { get { return _movement; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }   
    public ShopingCart ShopingCart { get { return _shopingCart; } }
    public PlayerAnimator PlayerAnimator { get { return _playerAnimator; } }
    public PlayerPriceDisplay PriceDisplay { get { return _playerPriceDisplay; } }
    public int PriceAllItems => _priceAllItems;

    public event UnityAction Died;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }

    public void Initialize(TouchHandler touchHandler, BoostSpeedEffect speedEffect,
        AddedMoneyEffect addedMoneyEffect, PlayerWallet wallet)
    {
        _movement.Initialize(touchHandler);
        _moneyEffect = addedMoneyEffect;
        _wallet = wallet;
        _boostEffect = speedEffect;

        _shopingCart.ItemAdded += OnItemAdded;
        _shopingCart.ItemRemoved += OnItemRemoved;
        _shopingCart.ItemLost += OnItemLost;
        _shopingCart.ItemSold += OnItemSold;
        _shopingCart.SpeedBoosted += OnSpeedBoosted;
        _shopingCart.Cash—ollected += OnCash—ollected;
        _shopingCart.Crashed += OnCartCrashed;
        _moneyEffect.Effect—ompleted += OneEffect—ompleted;
        _movement.StartMoving += OnStartMoving;
    }


    private void OnDestroy()
    {
        _shopingCart.ItemAdded -= OnItemAdded;
        _shopingCart.ItemRemoved -= OnItemRemoved;
        _shopingCart.ItemSold -= OnItemSold;
        _shopingCart.SpeedBoosted -= OnSpeedBoosted;
        _shopingCart.Cash—ollected -= OnCash—ollected;
        _moneyEffect.Effect—ompleted -= OneEffect—ompleted;
        _movement.StartMoving -= OnStartMoving;
    }

    public void OnFinishEntered()
    {
        _movement.DisableMovement();
        OffSpeedBoosted();
    }

    private void OnStartMoving()
    {
       _playerAnimator.OnPushAnimation(true);
    }

    private void OnSpeedBoosted()
    {
        _movement.SetSpeedBoost();
        _playerAnimator.SetSpeedAnimation(2);
        _boostEffect.Show();
    }

    private void OffSpeedBoosted()
    {
        _movement.SetSpeed();
        _playerAnimator.SetSpeedAnimation(1);
        _boostEffect.Hide();
    }

    private void OnItemAdded(int price)
    {
        _priceAllItems += price;
        _playerPriceDisplay.UpdatePrice(_priceAllItems);
        _audio.PlayCollectedItemClip();
    }

    private void OnItemRemoved(int price)
    {
        _priceAllItems -= price;
        _playerPriceDisplay.UpdatePrice(_priceAllItems);
        _audio.PlayCollectedItemClip();
    }
    private void OnItemLost(int price)
    {
        _priceAllItems -= price;
        _playerPriceDisplay.UpdatePrice(_priceAllItems);
        _audio.PlayLostClip();
    }

    private void OnItemSold(int price)
    {
        _moneyEffect.Play(price);
        _audio.PlaySoldClip();
    }

    private void OnCash—ollected(int cash)
    {
        _moneyEffect.Play(cash);
        _audio.PlayCollectedMoeyClip();
    }

    private void OnCartCrashed()
    {
        _movement.DisableMovement();
        _playerAnimator.OnSad(true);
        Time.timeScale = 0.4f;
        DOVirtual.DelayedCall(1, () => Time.timeScale = 1);
        DOVirtual.DelayedCall(2, () => Died?.Invoke());
    }

    private void OneEffect—ompleted(int cash)
    {
        _wallet.AddMoney(cash);
    }

    public void EnablePhysics()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.useGravity = true;
        _collider.height = 0.8f;
    }

    public void Tern()
    {
        _body.DOLocalRotate(new Vector3(0, -180f, 0), 0.5f);
    }

    public void ForceUP(float force)
    {
        _rigidbody.AddForce(transform.up * force, ForceMode.Impulse);
    }
}
