using UnityEngine;

public class Buyer : MonoBehaviour, IBuyer
{
    [SerializeField] private int _orderPrice;
    [SerializeField] private int _amountItems;
    [SerializeField] private ItemData _itemData;
    [SerializeField] private BuyerZone _buyerZone;
    [SerializeField] private BuyerDisplay _buyerDisplay;
    [SerializeField] private Transform _receptionPoint;
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;

    private float _receiveDelay = 0.05f;
    private JumpData _jumpData = new JumpData(1, 4, 0.5f);

    private ColorsChanger _colorChanger = new ColorsChanger();
    private readonly string Carrying = "IsCarrying";
    private Animator _animator;
    private int _currentAmountItems = 0;
    private Coroutine _freeCoroutine;

    public Transform ThisTransform => transform;
    public float ReceiveDelay => _receiveDelay;
    public int OrderPrice => _orderPrice;
    public string OrderItemName => _itemData.ItemName;
    public JumpData JumpData => _jumpData;
    public int OrderAmountItems => _amountItems;

    private void OnEnable()
    {
        _buyerZone.ShopingCartEntered += OnShopingCartEntered;
        _buyerZone.ShopingCartExited += OnShopingCartExited;
    }

    private void OnDisable()
    {
        _buyerZone.ShopingCartEntered -= OnShopingCartEntered;
        _buyerZone.ShopingCartExited -= OnShopingCartExited;
    }

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }


    private void Start()
    {
        _buyerDisplay.Initialize(_orderPrice, _amountItems, _itemData.Icon);
        _meshRenderer.material.SetColor("_Color", _colorChanger.GetRandomColor());
    }

    public void OnItemReceiving(Item item)
    {
        if (_orderPrice == 0) return;

        _animator.SetBool(Carrying, true);
        _currentAmountItems++;
        _buyerDisplay.UpdateCountItems(_currentAmountItems);
        if (_currentAmountItems == _amountItems)
        {
            _buyerDisplay.ShowSmile();
        }
    }

    public void OnItemReceived(Item item)
    {

    }

    public Vector3 GetPosition()
    {
        return _receptionPoint.localPosition;
    }

    private void OnShopingCartEntered(ShopingCart cart)
    {
        if (_orderPrice == 0)
            _freeCoroutine = StartCoroutine(cart.TryGetItemForFree(this));
        else
            cart.TryGetItemForMoney(this);
    }

    private void OnShopingCartExited(ShopingCart cart)
    {
        if (_freeCoroutine != null)
            StopCoroutine(_freeCoroutine);
    }
}
