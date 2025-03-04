using System;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UGG.Combat;
using Unity.VisualScripting;

namespace Enemy.Combat
{
    public class AICombatSystem : CharacterCombatSystemBase
    {
        [SerializeField , Header("范围检测")] private Transform detectionCenter;
        [SerializeField] private float detectionRang;
        [SerializeField]private LayerMask _whatIsEnemy;
        [SerializeField]private LayerMask _whatIsBos;

        private Collider[] _colliderTarget = new Collider[1];
        [SerializeField , Header("目标")]
        private Transform currentTarget;

        //动画
        private int lockOnID = Animator.StringToHash("LockOn");
        
        private void Update()
        {
            AIView();
            LockOnTarget();
            UpdateAnimation();
        }


        private void AIView()
        {
            int targteCounts = Physics.OverlapSphereNonAlloc(detectionCenter.position,
                detectionRang,
                _colliderTarget,
                _whatIsEnemy);
            
            if (targteCounts > 0)
            {
                if (!Physics.Raycast((transform.root.position + transform.root.up * 0.5f),
                        (_colliderTarget[0].transform.position - transform.root.position).normalized, out var hit,
                        detectionRang, _whatIsBos))
                {
                    if (Vector3.Dot((_colliderTarget[0].transform.position - transform.root.position).normalized,
                            transform.root.forward) > 0.45f)
                    {
                        Debug.Log("检测到玩家");
                        currentTarget = _colliderTarget[0].transform;
                    }
                }
            }
            
        }

        private void LockOnTarget()
        {
            if (_animator.CheckAnimationTag("Motion") && currentTarget != null)
            {
                _animator.SetFloat(lockOnID , 1f);
                transform.root.rotation = transform.LockOnTarget(currentTarget, transform.root.transform, 50f);
            }

            else
            {
                _animator.SetFloat(lockOnID, 0f);
            }
        }

        public Transform GetCurrentTarget()
        {
            if (currentTarget == null) return null;
            
            return currentTarget;
        }

        private void UpdateAnimation()
        {
            if (_animator.CheckAnimationTag("Roll"))
            {
                _characterMovementBase.CharacterMoveInterface(transform.root.forward,
                    _animator.GetFloat(animationMoveID), true);
            }
        }
        
        public float GetCurrentTargetDistance() => Vector3.Distance(currentTarget.position, transform.root.position);
    }
}