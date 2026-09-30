using UnityEngine;

public class CamScript : MonoBehaviour
{
    public Transform target; // alvo para seguir

    public void Update()
    {
        if (target != null)
        {
            transform.position = target.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f); // mantém a câmera a uma distância fixa do alvo
        }
    }
}
