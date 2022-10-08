using UnityEngine;

public class SeterZByOrderInLayer : MonoBehaviour
{
    [SerializeField] private float _orderInLayerToZPositonMultiplyer;

    [ContextMenu("Set Z by order in layer")]
    private void SetZByOrderInLayer()
    {
        foreach(SpriteRenderer spriteRenderer in FindObjectsOfType<SpriteRenderer>())
        {
            var findedTransform = spriteRenderer.gameObject.transform;

            findedTransform.position = new Vector3(findedTransform.position.x, findedTransform.position.y, spriteRenderer.sortingOrder * _orderInLayerToZPositonMultiplyer);
        }
    }
}
