using Enemy.Combat;
using UnityEngine;

namespace X.Scripts.Condition
{
    [CreateAssetMenu(fileName = "ConditionSO", menuName = "StateMachine/Condition/ConditionSO")]
    public class ToCombatCondition : ConditionSO
    {
        private AICombatSystem _combatSystem;
        
        public override bool ConditionSetUp()
        {
            return (_combatSystem.GetCurrentTarget() != null) ? true : false;
        }

        public override void Init(StateMachineSystem stateMachineSystem)
        {
            _combatSystem = stateMachineSystem.transform.root.GetComponentInChildren<AICombatSystem>();
        }
    }
}