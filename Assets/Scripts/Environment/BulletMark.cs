using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BulletMark : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(5f);
        GetComponent<Renderer>().material.DOColor(Color.clear, 1f).OnComplete(() => Destroy(gameObject));
    }
}
