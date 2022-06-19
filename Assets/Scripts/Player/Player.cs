using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;


[RequireComponent(typeof(PlayerAnimator))]
public class Player : MonoBehaviour, IPlayerEvents
{
    [SerializeField] private TouchHandler _touchHandler;
    [SerializeField] private ShopingCart _shopingCart;
    [SerializeField] private PlayerPriceDisplay _playerPriceDisplay;
    [SerializeField] private AddedMoneyEffect _moneyEffect;
    [SerializeField] private CapsuleCollider _collider;
    [SerializeField] private Transform _body;

    private PlayerMovement _movement;
    private Rigidbody _rigidbody;
    private PlayerAnimator _playerAnimator;
    private PlayerWallet _wallet;
    private AudioSource _audioSource;
    private int _priceAllItems;

    public PlayerMovement PlayerMovement { get { return _movement; } }
    public Rigidbody Rigidbody { get { return _rigidbody; } }   
    public PlayerWallet Wallet { get { return _wallet; } }
    public ShopingCart ShopingCart { get { return _shopingCart; } }
    public PlayerAnimator PlayerAnimator { get { return _playerAnimator; } }
    public PlayerPriceDisplay PlayerPriceDisplay { get { return _playerPriceDisplay; } }

    public event UnityAction CubeDestroyed;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _playerAnimator = GetComponent<PlayerAnimator>();
        _audioSource = GetComponent<AudioSource>();
        _wallet = GetComponent<PlayerWallet>();
        _rigidbody = GetComponent<Rigidbody>();
        _collider = GetComponent<CapsuleCollider>();
    }

    private void Start()
    {
        Initialize(_touchHandler);
    }

    public void Initialize(TouchHandler touchHandler)
    {
        _movement.Initialize(touchHandler);

        _shopingCart.ItemAdded += OnItemAdded;
        _shopingCart.ItemRemoved += OnItemRemoved;
        _shopingCart.ItemSold += OnItemSold;
        _moneyEffect.Effect—ompleted += OneEffect—ompleted;
        _movement.StartMoving += OnStartMoving;
    }

    private void OnDestroy()
    {
        _shopingCart.ItemAdded -= OnItemAdded;
        _shopingCart.ItemRemoved -= OnItemRemoved;
        _shopingCart.ItemSold -= OnItemSold;
        _moneyEffect.Effect—ompleted -= OneEffect—ompleted;
        _movement.StartMoving -= OnStartMoving;
    }

    private void OnStartMoving()
    {
       _playerAnimator.OnPushAnimation(true);
    }

    private void OnItemAdded(int price)
    {
        _priceAllItems += price;
        _playerPriceDisplay.UpdatePrice(_priceAllItems);
        _audioSource.Play();
    }

    private void OnItemRemoved(int price)
    {
        _priceAllItems -= price;
        _playerPriceDisplay.UpdatePrice(_priceAllItems);
        _audioSource.Play();
    }

    private void OnItemSold(int price)
    {
        _moneyEffect.Play(price);
    }

    private void OneEffect—ompleted()
    {
        _wallet.AddMoney(5);
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
