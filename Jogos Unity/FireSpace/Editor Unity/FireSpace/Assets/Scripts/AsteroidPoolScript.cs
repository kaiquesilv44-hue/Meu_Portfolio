using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidPoolScript : MonoBehaviour
{
    int Poolmax = 60;
    List<GameObject> AsteroidPool;
    int Ultimoasteroid = 0;
    public GameObject Asteroid;

    void Awake()
    {
        comeco();
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
      comeco();
    }

    public GameObject Getasteroid()
    {
        GameObject asteroid = AsteroidPool[Ultimoasteroid++];
        if (Ultimoasteroid >= AsteroidPool.Count)
            Ultimoasteroid = 0;
        return asteroid;
    }

    public void comeco()
    {
        AsteroidPool = new List<GameObject>();
        for (int i = 0; i < Poolmax; i++)
        {
            var asteroid = Instantiate(Asteroid);
            asteroid.SetActive(false);
            AsteroidPool.Add(asteroid);
            DontDestroyOnLoad(this.gameObject);
        }
    }


}
