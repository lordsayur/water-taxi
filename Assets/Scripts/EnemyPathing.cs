﻿using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField]
    private List<Transform> _waypoints;
    [SerializeField]
    private float _movementSpeed = 2f;
    [SerializeField]
    private float _rotationSpeed = 2f;
    private int _waypointIndex = 0;

    private void Start()
    {
        transform.position = _waypoints[_waypointIndex++].transform.position;
        transform.LookAt(_waypoints[_waypointIndex].transform.position);
    }

    private void Update()
    {
        MoveAlongWayPoints();
    }

    private void MoveAlongWayPoints()
    {
        if (_waypointIndex < _waypoints.Count)
        {
            var targetPosition = _waypoints[_waypointIndex].transform.position;

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, _movementSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPosition - transform.position), Time.deltaTime * _rotationSpeed);

            Debug.DrawLine(transform.position, targetPosition, Color.red);
            Debug.DrawRay(transform.position, targetPosition - transform.position, Color.green);

            if (transform.position == _waypoints[_waypointIndex].transform.position)
            {
                _waypointIndex++;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}