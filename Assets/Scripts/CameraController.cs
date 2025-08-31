using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float moveSpeed; 
        [SerializeField] private float zoomSpeed; 
        [SerializeField] private float minOrthoSize; 
        [SerializeField] private float maxOrthoSize;
        [SerializeField] private Camera cam;



        void Update()
        {

            float moveX = Input.GetAxisRaw("Horizontal"); 
            float moveY = Input.GetAxisRaw("Vertical");

            Move(moveX, moveY);


            if (Input.GetKey(KeyCode.Equals) || Input.GetKey(KeyCode.KeypadPlus))
            {
                CameraZoom(1);
            }
            else if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.KeypadMinus)) 
            {
                CameraZoom(-1);
            }
        }

        private void CameraZoom(int directionZoom )
        {
            float size = cam.orthographicSize + zoomSpeed * directionZoom * Time.deltaTime;
            size = Mathf.Clamp(size, minOrthoSize, maxOrthoSize);
            cam.orthographicSize = size;
        }

        private void Move(float x,float y)
        {
            Vector3 moveDirection = new Vector3(x, y, 0);
            moveDirection = moveDirection.normalized;
            cam.transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }


    }
}
