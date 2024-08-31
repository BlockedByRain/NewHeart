using UnityEngine;


public class GameManager : MonoSingleton<GameManager>
{
    /// <summary>
    ///  ��Ϸ״̬ö��
    /// </summary>
    public enum GameState
    {
        None,
        Start,
        Playing,
        Pause,
        GameOver
    }







    // ��ǰ��Ϸ״̬
    [SerializeField]
    private GameState _state = GameState.None;

    private void Update()
    {
        switch (_state)
        {
            case GameState.Start:
                // TODO: ��Ϸ��ʼ�߼�
                break;
            case GameState.Playing:
                // TODO: ��Ϸ�����߼�
                break;
            case GameState.Pause:
                // TODO: ��Ϸ��ͣ�߼�
                break;
            case GameState.GameOver:
                // TODO: ��Ϸ�����߼�
                break;
        }
    }

    public void SetGameState(GameState state)
    {
        _state = state;
    }
}


