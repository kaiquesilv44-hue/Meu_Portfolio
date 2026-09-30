using System.Collections.Generic;
using UnityEngine;

public class BalaPoolScript : MonoBehaviour
{
    int Poolmax = 20;
    List<GameObject> BalaPool;
    int Ultimabala = 0;
    public GameObject Bala;

    void Awake()
    {
        BalaPool = new List<GameObject>();
        for (int i = 0; i < Poolmax; i++)
        {
            var bala = Instantiate(Bala);
            bala.SetActive(false);
            BalaPool.Add(bala);
        }
    }

   public GameObject Getbala()
    {
        GameObject bala = BalaPool[Ultimabala++];
        if (Ultimabala >= BalaPool.Count)
        
            Ultimabala = 0;
            return bala;
        

    }
}
