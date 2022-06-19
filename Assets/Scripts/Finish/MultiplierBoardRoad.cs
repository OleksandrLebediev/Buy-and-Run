using System.Collections.Generic;
using UnityEngine;

public class MultiplierBoardRoad : MonoBehaviour
{
    [SerializeField] private MultiplierBoard _multiplierBoard;
    private ColorsChanger _colorsChanger = new ColorsChanger();
    private List<MultiplierBoard> _multiplierBoards = new List<MultiplierBoard>();
    private Color _nextColor;
    private float _multiplier;
    private MultiplierBoard _currentBoard;

    private void Start()
    {
        _nextColor = _colorsChanger.GetColor(Color.blue, 3);
        GenerationBoard(200);
    }

    public void GenerationBoard(int count)
    {
        float offsetY = _multiplierBoard.transform.localScale.y;

        for (int i = 0; i < count; i++)
        {
            Vector3 position = new Vector3(0, 2 * i, 0);
            MultiplierBoard board = Instantiate(_multiplierBoard, position, Quaternion.identity);
            _multiplier += 0.1f;
            board.transform.SetParent(transform, false);
            board.SetColor(_nextColor);
            board.SetDisplayMultiplier(_multiplier);
            _multiplierBoards.Add(board);
            _nextColor = _colorsChanger.GetColor(_nextColor, 10);
        }
    }

    public void ActivateBoard(float positionY)
    {
        int inex = (int)(positionY / 2) - 1;
        _currentBoard = _multiplierBoards[inex];
        _currentBoard.ActivatePlace();
    }

    public void ActivateConfetti()
    {
        _currentBoard.ActivateConfetti();
    }
}
