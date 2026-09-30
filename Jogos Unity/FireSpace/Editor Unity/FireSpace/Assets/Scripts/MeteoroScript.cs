using UnityEngine;

public class MeteoroScript : MonoBehaviour
{
    Rigidbody2D rb;
    NaveScript ns;
    public GameObject mt1;
    public GameObject mt2;
    public GameObject mt3;
    public GameObject fumaca;
    public float Velocidade  = 200f;
    public float direcao;
    public float velo;
    Vector3 mtd1 = new Vector3(0.5f, 0.0f, 0.0f);
    Vector3 mtd2 = new Vector3(0.0f, 0.5f, 0.0f);
    Vector3 mtd3 = new Vector3(0.0f, -0.5f, 0.0f);


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ns = FindFirstObjectByType<NaveScript>();
         rb.AddRelativeForce(Vector2.up * Velocidade);
        direcao = Random.value;
        velo = Random.Range(10, 500);
    }

    void Update()
    {
            if (direcao > 0.5f)
            {
                transform.Rotate(0, 0, velo * Time.deltaTime);
            }
            else
            {
                transform.Rotate(0, 0, -velo * Time.deltaTime);
            }
    }

    public void Explodir()
    {
        ns.Score += 10;
        Instantiate(fumaca, transform.position, Quaternion.identity);
        gameObject.SetActive(false);
    }   

    public void OnTriggerEnter2D(Collider2D collision)
    {
      if (collision.gameObject.CompareTag("Bala"))
        {
            Explodir();
            Instantiate(mt1, transform.position + mtd1, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            Instantiate(mt2, transform.position + mtd2, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
            Instantiate(mt3, transform.position + mtd3, Quaternion.Euler(0, 0, Random.Range(0f, 360f)));
        }
    }
}
