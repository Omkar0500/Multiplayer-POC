using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player_Movement : MonoBehaviour
{
    [SerializeField] float speed;

    [SerializeField] float xDirection;
    [SerializeField] float zDirection;

    [SerializeField] Vector3 moveDirection;

    [SerializeField] GameObject cameraObject;

    private PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        cameraObject = gameObject.transform.GetChild(0).gameObject;

        if (view.IsMine)
        {
            cameraObject.SetActive(true);
        }
    }
    void Update()
    {
        if (view.IsMine)
        {
            xDirection = Input.GetAxis("Horizontal");
            zDirection = Input.GetAxis("Vertical");

            moveDirection = new Vector3(xDirection, 0.0f, zDirection);

            transform.position += moveDirection * speed * Time.deltaTime;
        }
    }
}
