using UnityEngine;

public class LossePastDie : LosseCondition
{
    [SerializeField] private PlayerHealth _spectableHealth;

    private void OnEnable()
    {
        _spectableHealth.OnDie += Losse;
    }

    private void OnDisable()
    {
        _spectableHealth.OnDie -= Losse;
    }
}
