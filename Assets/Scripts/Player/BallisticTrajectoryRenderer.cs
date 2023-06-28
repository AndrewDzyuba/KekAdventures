using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(LineRenderer))]
    public class BallisticTrajectoryRenderer : MonoBehaviour
    {
        private LineRenderer _line;
        
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private Vector3 _startVelocity;
        [SerializeField] private float _trajectoryVertDist = 0.25f;
        [SerializeField] private float _maxCurveLength = 5;
        
        [Header("Debug")]
        [SerializeField] private bool _debugAlwaysDrawTrajectory = false;
        
        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }
        
        private void Update()
        {
            SetBallisticValues(transform.position, transform.rotation.eulerAngles);
            
            if (Input.GetKey(InputSettings.FIRE) || _debugAlwaysDrawTrajectory)
            {
                //DrawTrajectory();
            }
            
            if (Input.GetKeyUp(InputSettings.FIRE) && !_debugAlwaysDrawTrajectory)
            {
                //ClearTrajectory();
            }
        }

        public void SetBallisticValues(Vector3 startPosition, Vector3 startVelocity)
        {
            _startPosition = startPosition;
            _startVelocity = startVelocity;
        }

        private void DrawTrajectory()
        {
            var curvePoints = new List<Vector3>();
            curvePoints.Add(_startPosition);
            
            var currentPosition = _startPosition;
            var currentVelocity = _startPosition;
            
            RaycastHit hit;
            Ray ray = new Ray(currentPosition, currentVelocity.normalized);
           
            while (!Physics.Raycast(ray, out hit, _trajectoryVertDist) && Vector3.Distance(_startPosition, currentPosition) < _maxCurveLength)
            {
                var trajectoryTime = _trajectoryVertDist / currentVelocity.magnitude;
                
                currentVelocity = currentVelocity + trajectoryTime * Physics.gravity;
                currentPosition = currentPosition + trajectoryTime * currentVelocity;
                curvePoints.Add(currentPosition);
                
                ray = new Ray(currentPosition, currentVelocity.normalized);
            }
            
            if (hit.transform)
            {
                curvePoints.Add(hit.point);
            }
            
            _line.positionCount = curvePoints.Count;
            _line.SetPositions(curvePoints.ToArray());
        }

        private void ClearTrajectory()
        {
            _line.positionCount = 0;
        }
    }
}
