using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Erinn
{
    public sealed class Tractor : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private PointGenerator _pointGenerator;
        private Animator _anim;
        private bool _isWalking;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _pointGenerator = GetComponent<PointGenerator>();
            _anim = transform.Find("Model").GetComponent<Animator>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                BeginWalking();
            }
        }

        private void BeginWalking()
        {
            if (_isWalking)
                return;
            _isWalking = true;
            _anim.Play(Animator.StringToHash("Walk"), 0, 0f);
            StartCoroutine(Walking());
        }

        private IEnumerator Walking()
        {
            var points = _pointGenerator.Positions;
            for (var i = 0; i < points.Length; ++i)
            {
                var point = points[i];
                point.y = transform.position.y;
                _agent.SetDestination(point);
                while (Vector3.Distance(transform.position, point) > 0.1f)
                    yield return null;
            }

            _isWalking = false;
            _anim.Play(Animator.StringToHash("Idle"), 0, 0f);
        }
    }
}