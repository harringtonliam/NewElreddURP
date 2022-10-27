using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace RPG.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] float moveSpeed = 10f;
        [SerializeField] float rotationSpeed = 100f;
        [SerializeField] float zoomAmount = 1f;
        [SerializeField] float zoomSpeed = 5f;
        [SerializeField] float minFollowYOffset = 2f;
        [SerializeField] float maxFollowYOffset = 15f;
        [SerializeField] CinemachineVirtualCamera virtualCamera;


        private Vector3 targetFollowOffset;
        private CinemachineTransposer cinemachineTransposer;

        private void Start()
        {
             cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            targetFollowOffset = cinemachineTransposer.m_FollowOffset;
        }



        // Update is called once per frame
        void Update()
        {
            HandleMovement();

            HandleRotation();

            HandleZoom();

        }

        private void HandleZoom()
        {
            cinemachineTransposer = virtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            if (Input.mouseScrollDelta.y > 0)
            {
                targetFollowOffset.y -= zoomAmount;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                targetFollowOffset.y += zoomAmount;
            }
            targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, minFollowYOffset, maxFollowYOffset);
            cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, Time.deltaTime * zoomSpeed);
        }

        private void HandleRotation()
        {
            Vector3 rotationVector = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.Q))
            {
                rotationVector.y = +1f;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotationVector.y = -1f;
            }

            transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;
        }

        private void HandleMovement()
        {
            Vector3 inputMoveDirection = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDirection.z = +1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputMoveDirection.z = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputMoveDirection.x = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputMoveDirection.x = +1f;
            }

            Vector3 moveVector = transform.forward * inputMoveDirection.z + transform.right * inputMoveDirection.x;
            transform.position += moveVector * moveSpeed * Time.deltaTime;
        }
    }

}


