using System;
using UnityEngine;
using UGG.Combat;
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

        private void Update()
        {
            AIView();
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
        
    }
}