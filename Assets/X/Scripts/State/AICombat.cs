using UnityEngine;

namespace X.Scripts.StateMachine.State
{
    [CreateAssetMenu(fileName = "AICombat", menuName = "StateMachine/State/AICombat")]
    public class AICombat : StateActionSO
    {
        
        public override void OnUpdate()
        {
            Debug.Log("AICombat");
            NoCombat();
        }
        
        
        public void NoCombat()
        {
            if (_animator.CheckAnimationTag("Motion"))
            {
                if (_combatSystem.GetCurrentTargetDistance() < 4.1 + 0.1f)
                {
                    _movement.CharacterMoveInterface(-_movement.transform.forward, 1.5f , true);
                    _animator.SetFloat(verticalID , -1f , 0.23f , Time.deltaTime);
                    _animator.SetFloat(horizontalID , 0f , 0.1f , Time.deltaTime);

                    if (_combatSystem.GetCurrentTargetDistance() < 1.5f)
                    {
                        _animator.Play("Roll_B" , 0 , 0f);
                    }
                }

                if (_combatSystem.GetCurrentTargetDistance() > 4.1 + 0.1f)
                {
                    _movement.CharacterMoveInterface(_movement.transform.forward, 1.5f , true);
                    _animator.SetFloat(verticalID , 1f , 0.23f , Time.deltaTime);
                    _animator.SetFloat(horizontalID , 0f , 0.1f , Time.deltaTime);

                }
            }
        }
    }
}