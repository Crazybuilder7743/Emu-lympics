using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Splines;

[RequireComponent(typeof(SplineContainer))]
public class Level : MonoBehaviour
{
    public SplineContainer splineContainer;
    public Spline playerRunRail => splineContainer[0];
    public void Awake()
    {
        splineContainer = GetComponent<SplineContainer>();
    }
    //TODO: need list of masks

}
