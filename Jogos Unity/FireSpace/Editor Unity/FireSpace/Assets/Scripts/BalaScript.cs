using System.Collections;
using UnityEngine;

public class BalaScript : MonoBehaviour
{
    public float velocidade = 5f;
    void OnEnable()
    {
        StartCoroutine(destruir());
    }

    IEnumerator destruir()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Vector3.up * velocidade * Time.deltaTime);

    }
}
