using UnityEngine;

public class MeteoritoScipt : MonoBehaviour
{
    Rigidbody2D rb;
    NaveScript ns;
    public float direcao;
    public float velocidade;
    public GameObject fumaca;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ns = FindFirstObjectByType<NaveScript>();
        rb.AddRelativeForce(Vector2.up * 200f);
        direcao = Random.value;
        velocidade = Random.Range(10, 500);
    }

    void Update()
    {
            if (direcao > 0.5f)
            {
                transform.Rotate(0, 0, velocidade * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, -velocidade * Time.deltaTime);
            }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bala"))
        {
            ns.Score += 5;
            Instantiate(fumaca, transform.position, Quaternion.identity);
            Explodir();
        }
    }

    public void Explodir()
    {

        Destroy(gameObject);
    }
}
