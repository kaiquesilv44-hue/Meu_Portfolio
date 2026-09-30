using UnityEngine;

public class PausaControleScript : MonoBehaviour
{
   public GameObject Pausa;
    GameControllerScript controller;

    private void Start()
    {
        controller = FindFirstObjectByType<GameControllerScript>();
    }

    private void Update()
    {
        if (controller.Pausado == true)
        {
            Pausa.SetActive(true);
        }
        else
        {
            Pausa.SetActive(false);
        }
    }
}
