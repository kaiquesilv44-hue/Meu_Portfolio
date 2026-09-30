using UnityEngine;

public class BotoesScript : MonoBehaviour
{
    NaveScript Player;
    GameControllerScript Controller;

    void Start()
    {
        Player = FindFirstObjectByType<NaveScript>();
        Controller = FindFirstObjectByType<GameControllerScript>();
    }
    void Update()
    {
        if (!Player.Vivo || Time.timeScale == 0f)
        {
            gameObject.SetActive(false);
        }
    }
}
