using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Herkdess.Tools.BehaviorTree
{
    public class T_EnemyAI : MonoBehaviour
    {
        [SerializeField] float startingHealth;
        [SerializeField] float lowHealthThreshold;
        [SerializeField] float healthRestorationRate;

        [SerializeField] float chasingRange;
        [SerializeField] float shootingRange;

        [SerializeField] NavMeshAgent Agent;

        [SerializeField] Transform playerTransform;
        [SerializeField] Cover[] AvaliableCovers;
        [SerializeField] LayerMask CoverMask;


        Material material;

        Transform bestCover;


        private Node TopNode;
        [SerializeField] float _currentHealth;
        public float currentHealth
        {
            get { return _currentHealth; }
            set { _currentHealth = Mathf.Clamp(value, 0, startingHealth); }
        }

        private void Start()
        {
            _currentHealth = startingHealth;
            material = GetComponent<MeshRenderer>().material;
            ConstructBehaviourTree();
        }

        private void ConstructBehaviourTree()
        {
            IsCoverAvaliableNode coverAvaliableNode = new IsCoverAvaliableNode(AvaliableCovers, playerTransform, this, CoverMask);
            GoToCoverNode goToCoverNode = new GoToCoverNode(Agent, this);
            HealthNode healthNode = new HealthNode(this, lowHealthThreshold);
            IsCoveredNode isCoveredNode = new IsCoveredNode(playerTransform, transform);
            ChaseNode chaseNode = new ChaseNode(playerTransform, Agent, this);
            RangeNode chasingInRangeNode = new RangeNode(chasingRange, playerTransform, transform);
            RangeNode shootingInRangeNode = new RangeNode(shootingRange, playerTransform, transform);
            ShootNode shootNode = new ShootNode(Agent, this);


            Sequence chaseSeq = new Sequence(new List<Node>
            {
                chasingInRangeNode, chaseNode
            });
            Sequence shootSeq = new Sequence(new List<Node>
            {
                shootingInRangeNode, shootNode
            });

            Sequence goToCoverSeq = new Sequence(new List<Node>
            {
                coverAvaliableNode, goToCoverNode
            });

            Selector findCoverSel = new Selector(new List<Node>
            {
                goToCoverNode, chaseNode
            });

            Selector tryToTakeCoverSel = new Selector(new List<Node>
            {
                isCoveredNode, findCoverSel
            });

            Sequence mainCoverSeq = new Sequence(new List<Node>
            {
                tryToTakeCoverSel, healthNode
            });

            TopNode = new Selector(new List<Node> { mainCoverSeq, shootSeq, chaseSeq });
        }

        private void Update()
        {
            TopNode.Evaluate();
            if (TopNode.nodestate == NodeState.Fail)
            {
                SetColor(Color.red);
            }
            currentHealth = Mathf.MoveTowards(currentHealth, startingHealth, healthRestorationRate * Time.deltaTime);

        }

        private void OnMouseDown()
        {
            currentHealth -= 10f;
        }

        public void SetColor(Color col)
        {
            material.color = col;
        }

        public void SetBestCover(Transform cover)
        {
            bestCover = cover;
        }

        public Transform GetBestCover()
        {
            return bestCover;
        }



    }
}