using UnityEngine;

public class TileInfinito : MonoBehaviour
{
    public Transform player;
    public float larguraTile = 32f; // Tamanho no eixo X
    public float alturaTile = 18f;  // Tamanho no eixo Y

    void Update()
    {
        if (player != null)
        {
            float diffX = player.position.x - transform.position.x;
            float diffY = player.position.y - transform.position.y;


            // Movimentação Horizontal (X)
            if (Mathf.Abs(diffX) > larguraTile * 1.5f)
            {
                float direcao = Mathf.Sign(diffX);
                transform.position += new Vector3(direcao * larguraTile * 3, 0, 0);
            }

            // Movimentação Vertical (Y)
            if (Mathf.Abs(diffY) > alturaTile * 1.5f)
            {
                float direcao = Mathf.Sign(diffY);
                transform.position += new Vector3(0, direcao * alturaTile * 3, 0);
            }
        }
    }
}

