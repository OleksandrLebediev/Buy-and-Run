using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierBoardRoad : MonoBehaviour
{
    [SerializeField] private MultiplierBoard _multiplierBoard;
    private ColorsChanger _colorsChanger = new ColorsChanger();
    private List<MultiplierBoard> _multiplierBoards = new List<MultiplierBoard>();
    private MultiplierBoard _currentBoard;

    private Color _nextColor;
    private float _nextMultiplier;
    private int _startAmountBoard = 30;
    private float _centerRoad;
    private int _height;
    private int _scaleOffset = 2;
    private float _colorChangeStep = 10;
    private Transform _target;

    private void Start()
    {
        _nextColor = _colorsChanger.GetColor(Color.blue, 3);
        GenerationBoard(_startAmountBoard);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void GenerationBoard(int count)
    {
        float offsetY = _multiplierBoard.transform.localScale.y;

        for (int i = 0; i < count; i++)
        {
            MultiplierBoard board = Instantiate(_multiplierBoard);
            SetBoardSettings(board);
            _multiplierBoards.Add(board);
        }

        _centerRoad = _multiplierBoards[_multiplierBoards.Count / 2].transform.position.y;
    }

    private Vector3 GetNextBoardPosition()
    {
        return new Vector3(0, _scaleOffset * _height, 0);
    }

    private void FixedUpdate()
    {
        if (_target == null) return;

        if (_target.position.y >= _centerRoad)
        {
            MultiplierBoard board = _multiplierBoards[0];
            SetBoardSettings(board);

            _multiplierBoards.Remove(board);
            _multiplierBoards.Add(board);
            _centerRoad += _scaleOffset;
        }
    }

    private void SetBoardSettings(MultiplierBoard board)
    {
        board.SetPosition(GetNextBoardPosition());
        board.SetColor(_nextColor);
        board.SetDisplayMultiplier(_nextMultiplier);
        board.transform.SetParent(transform, false);
        _nextColor = _colorsChanger.GetColor(_nextColor, _colorChangeStep);
        _nextMultiplier += 0.1f;
        _height++;
    }

    public void ActivateBoard(float positionY)
    {
        int center = _multiplierBoards.Count / 2;
        if (_multiplierBoards[center].transform.localPosition.y > positionY)
        {
            center = _multiplierBoards.FindIndex(x => x.transform.localPosition.y >= positionY);
        }
        center -= 2;
        if (center < 0) center = 0;
        _currentBoard = _multiplierBoards[center];
        _currentBoard.ActivatePlace();
    }

    public float GetMultiplier()
    {
        return _currentBoard.MultiplierValue;
    }

    public void ActivateConfetti()
    {
        _currentBoard.ActivateConfetti();
    }
}
