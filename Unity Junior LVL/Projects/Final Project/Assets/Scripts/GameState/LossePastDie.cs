using UnityEngine;

public class LossePastDie : LosseCondition
{
    [SerializeField] private PlayerHealth _spectableHealth;

    private void OnEnable()
    {
        _spectableHealth.OnDie += OnDie;
    }

    private void OnDisable()
    {
        _spectableHealth.OnDie -= OnDie;
    }

    private void OnDie()
    {
        Losse();
    }
}
