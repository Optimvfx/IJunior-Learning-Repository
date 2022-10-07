using DG.Tweening;
using System.Linq;
using UnityEngine;

public class PathBaseDOTween : MonoBehaviour
{
    [SerializeField] private Transform[] _pathWaypoints;
    [SerializeField] private float _pathDuration;
    [Range(0, 1)]
    [SerializeField] private float _lookAtSpeed;

    private void OnValidate()
    {
        _pathDuration = Mathf.Max(_pathDuration, 0);
    }

    private void Start()
    {
         var pathWaypointsPositions = (_pathWaypoints.Select(wayPoint => wayPoint.position)).ToArray();

        Tween pathMoveing = transform.DOPath(pathWaypointsPositions, _pathDuration, PathType.CatmullRom).SetOptions(true).SetLookAt(_lookAtSpeed);

        pathMoveing.SetEase(Ease.Linear);
        pathMoveing.SetLoops(-1);
    }
}
