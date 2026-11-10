using UnityEngine;

[CreateAssetMenu()]
public class LeverSwitchSO : ScriptableObject
{
    [SerializeField] private DoorTargetSO doorTarget;
    public DoorTargetSO DoorTarget=> doorTarget;
}
