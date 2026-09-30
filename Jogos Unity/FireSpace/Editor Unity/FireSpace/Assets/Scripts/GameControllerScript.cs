using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameControllerScript : MonoBehaviour
{
    public float tempoSpawn = 0.3f;
    public int MaiorScore;
    AsteroidPoolScript ap;
    NaveScript Player;
    public Text ScoreText;
    private static GameControllerScript Instance;
    public bool Pausado = false;


    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        Player = FindFirstObjectByType<NaveScript>();
        ap = FindFirstObjectByType<AsteroidPoolScript>();
        StartCoroutine(Spawnar());
        carregar();
        ScoreText.text = "MELHOR \r\nSCORE:" + MaiorScore;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        if (scene.name == "Menu")
        {
            transform.position = new Vector3(0, 0, 0);
            Time.timeScale = 1f;
            carregar();
            if (ScoreText != null)
            {
                ScoreText.text = "MELHOR \r\nSCORE:" + MaiorScore;
            }
        }
        ap = FindFirstObjectByType<AsteroidPoolScript>();
        Player = FindFirstObjectByType<NaveScript>();
        StartCoroutine(Spawnar());
    }

    public void Update()
    {
        if (Player != null)
        {
            if (Player.pp == true)
            {
                Pausado = true;
            }
            else
            {
                Pausado = false;
            }
        }


        Scene cena = SceneManager.GetActiveScene();
        if (Player != null)
        {
            transform.position = Player.transform.position;
        }

        if (cena.name == "MainScene")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Pausado = !Pausado;
            }
        }


        if (cena.name == "MainScene")
        {
            if (Pausado == true)
            {
                salvar();
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }
    public void SpawnAsteroides()
    {
        var posx = (Random.value > 0.5f) ? Random.Range(11, 14) : Random.Range(-11, -14);
        var posy = (Random.value > 0.5f) ? Random.Range(7, 10) : Random.Range(-7, -10);
        Vector3 Local = new Vector3(posx, posy, 0);

        var Asteroide = ap.Getasteroid();
        if (Asteroide == null) return;
        Asteroide.transform.position = transform.position + Local;
        Asteroide.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0f, 360f));
        Asteroide.SetActive(true);
    }

    public void salvar()
    {
        if (Player.Score > MaiorScore)
        {
            PlayerPrefs.SetInt("Score", Player.Score);
        }
    }
    
    public void carregar()
    {
       MaiorScore = PlayerPrefs.GetInt("Score", 0);
    }

    IEnumerator Spawnar()
    {
        SpawnAsteroides();
        yield return new WaitForSeconds(tempoSpawn);
        StartCoroutine(Spawnar());
    }

}
