using UnityEngine;

public class Lobbylogica : MonoBehaviour
{
    public GameManager GameManager;

    public void OnAddPlayerButton()
    {
        GameManager.AddPlayer();
    }

    public void OnStartGameButton()
    {
        GameManager.StartGame();
    }

    public void PlayerJoined()
    {
        Debug.Log("speler is gejoind");
    }


}
