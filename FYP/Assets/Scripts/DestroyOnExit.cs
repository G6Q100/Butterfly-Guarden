using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnExit : MonoBehaviour
{
    public int destoryTime;
    void Update()
    {
        StartCoroutine(DestroyAfterAnimation());
    }

    private IEnumerator DestroyAfterAnimation()
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gameObject);
    }
}
