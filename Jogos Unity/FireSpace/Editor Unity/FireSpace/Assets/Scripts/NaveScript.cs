using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NaveScript : MonoBehaviour
{
    Rigidbody2D rb;
    BalaPoolScript bp;
    Animator anim;
    GameControllerScript controller;
    public Text ScoreText;
    public Button Reiniciar;
    public Button Menu;
    public AudioClip fp;
    public AudioClip fa;
    public AudioClip batida;
    public AudioClip tiro;
    public AudioSource ad;
    public AudioSource ad2;
    private bool Boost = true;
    public bool Vivo = true;
    public bool pp = false;
    public bool direitando = false;
    public bool esquerdando = false;
    public bool andando = false;
    private bool podebater = true;
    public float Speed = 2.8f;
    public float Rotacao = 200.0f;
    public float BoostForce = 6f;
    public float Resetboost = 5f;
    public float maxSpeed = 25f;
    public int Vida = 3;  
    public int Score = 0;

    public GameObject Vida1;
    public GameObject Vida2;
    public GameObject Vida3;

    public GameObject[] objetosParaAtivar;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bp = FindFirstObjectByType<BalaPoolScript>();
        anim = GetComponent<Animator>();
        controller = FindFirstObjectByType<GameControllerScript>();       
    }

    public void reiniciar()
    {
        controller.Pausado = false;
        SceneManager.LoadScene("MainScene");
    }

    public void menu()
    {
        controller.Pausado = false;
        controller.StopAllCoroutines();
        SceneManager.LoadScene("Menu");
    }


      void Update()
      {
        if(Time.timeScale == 1f && Vivo)
        {
            AtivarTudo();
        }


            if (!Vivo)
            {
                transform.Rotate(0, 0, 140f * Time.deltaTime);
            }


            if (Vida == 2)
            {
                Vida3.SetActive(false);
            }
            if (Vida == 1)
            {
                Vida2.SetActive(false);
            }
            if (Vida == 0)
            {
                Vida1.SetActive(false);
                controller.salvar();
                anim.SetBool("Vivo", false);
                Reiniciar.gameObject.SetActive(true);
                Menu.gameObject.SetActive(true);
                Vivo = false;
            }

            if (Vivo)
            {
                if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(0, 0, -Rotacao * Time.deltaTime);
                }
            if (direitando)
            {
                transform.Rotate(0, 0, -Rotacao * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(0, 0, Rotacao * Time.deltaTime);
                }
            if (esquerdando)
            {
                transform.Rotate(0, 0, Rotacao * Time.deltaTime);
            }


            if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                if (controller.Pausado == false)
                {
                    Atirar();
                }
                }
                if (Input.GetKeyDown(KeyCode.Space) && Boost)
                {
                if (controller.Pausado == false)
                {
                    ad.Play();
                    rb.AddRelativeForce(Vector2.up * BoostForce);
                    Boost = false;
                    if (anim != null) anim.SetBool("Boost", true);
                    StartCoroutine(ResetBoost());
                }
                }

            
            ScoreText.text = "Score: " + Score;

                if (Input.GetKeyUp(KeyCode.W))
                {
                if (controller.Pausado == false)
                {
                    ad2.Stop();
                    ad.PlayOneShot(fp);
                }
                }
            }
      }


    public void Atirar()
    {
        if (!controller.Pausado && Vivo)
        {
            var Bala = bp.Getbala();
            if (Bala == null) return;
            Bala.transform.position = transform.position;
            Bala.transform.rotation = transform.rotation;
            Bala.SetActive(true);
            ad.PlayOneShot(tiro);
        }
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Asteroide") && podebater)
        {
            Vida--;
            podebater = false;
            ad.PlayOneShot(batida);
            StartCoroutine(tomardano());
        }
    }

    public void FimBoost()
    {
        if (anim != null) anim.SetBool("Boost", false);
        ad.Stop();
        ad.PlayOneShot(fp);
    }

    public void FixedUpdate()
    {
        if (Vivo)
        {
            if (Input.GetKey(KeyCode.W) || (andando))
            {
               andar();
            }
            else
            {
                if (anim != null) anim.SetBool("Andando", false);
            }
        }
    }

    private void andar()
    {
        if (!controller.Pausado)
        {
            rb.AddRelativeForce(Vector2.up * Speed);
            if (rb.linearVelocity.magnitude > maxSpeed)
            {
                rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
            }
            if (anim != null) anim.SetBool("Andando", true);
            if (!ad2.isPlaying)
            {
                ad2.Play();
            }
        }
    }

      IEnumerator ResetBoost()
      {
        yield return new WaitForSeconds(Resetboost);
        Boost = true;
      }

    IEnumerator tomardano()
    {
       yield return new WaitForSeconds(1.5f);
        podebater = true;
    }

    public void Booost()
    {
        if (Boost)
        {
            if (controller.Pausado == false)
            {
                ad.Play();
                rb.AddRelativeForce(Vector2.up * BoostForce);
                Boost = false;
                if (anim != null) anim.SetBool("Boost", true);
                StartCoroutine(ResetBoost());
            }
        }
    }
  
    public void Pausa()
    {

    }
    public void direitaup()
    {
        direitando = false;
    }
    public void direitadown()
    {
        direitando = true;
    }
    public void esqueraup()
    {
        esquerdando = false;
    }
    public void esquerdadown()
    {
        esquerdando=true;
    }

    public void parara()
    {
        if (!controller.Pausado)
        {
            ad2.Stop();
            ad.PlayOneShot(fp);
        }
    }

    public void andartrue()
    {
        andando = true;
    }
    public void andarfalse()
    {
        andando = false;
    }

    public void pausando()
    {
        pp = !pp;
    }

    public void AtivarTudo()
    {
        foreach (GameObject obj in objetosParaAtivar)
        {
            if (obj != null)
            {
                obj.SetActive(true);
            }
        }
    }

}
