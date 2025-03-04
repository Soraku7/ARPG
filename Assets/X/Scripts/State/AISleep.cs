using UnityEngine;

namespace X.Scripts.StateMachine.State
{
    [CreateAssetMenu(fileName = "AISleep", menuName = "StateMachine/State/AISleep")]
    public class AISleep : StateActionSO
    {
        public override void OnUpdate()
        {
            Debug.Log("AISleep");
        }
    }
}