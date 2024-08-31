using UnityEngine;


public class GameManager : MonoSingleton<GameManager>
{
    /// <summary>
    ///  游戏状态枚举
    /// </summary>
    public enum GameState
    {
        None,
        Start,
        Playing,
        Pause,
        GameOver
    }







    // 当前游戏状态
    [SerializeField]
    private GameState _state = GameState.None;

    private void Update()
    {
        switch (_state)
        {
            case GameState.Start:
                // TODO: 游戏开始逻辑
                break;
            case GameState.Playing:
                // TODO: 游戏进行逻辑
                break;
            case GameState.Pause:
                // TODO: 游戏暂停逻辑
                break;
            case GameState.GameOver:
                // TODO: 游戏结束逻辑
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        _state = state;
    }
}


