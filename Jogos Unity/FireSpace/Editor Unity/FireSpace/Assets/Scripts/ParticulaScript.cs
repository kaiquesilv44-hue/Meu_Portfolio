using System.Collections;
using UnityEngine;

public class ParticulaScript : MonoBehaviour
{
   
    void Start()
    {
        StartCoroutine(DestruirParticula());
    }

    IEnumerator DestruirParticula()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }


}
