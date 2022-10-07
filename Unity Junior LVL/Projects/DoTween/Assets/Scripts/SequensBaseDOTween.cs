using DG.Tweening;
using UnityEngine;

public class SequensBaseDOTween : MonoBehaviour
{
    private void Start()
    {
        var sequens = DOTween.Sequence();

        sequens.Append(transform.DOMoveX(20, 5));
        sequens.Insert(0,transform.DORotate(new Vector3(300,300,300), 4));

        sequens.Append(transform.DOMoveY(-10, 3));
        sequens.Insert(1, transform.DORotate(new Vector3(-300, -300, -300), 5));

        sequens.SetLoops(-1, LoopType.Yoyo);
    }
}
