using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float _baseSpeed = 10.0f;
    float _gravidade = 9.8f;
    GameManager gm;

    public GameObject chest;

    CharacterController characterController;

    //Referência usada para a câmera filha do jogador
    GameObject playerCamera;
    //Utilizada para poder travar a rotação no angulo que quisermos.
    float cameraRotation;

    Vector3 warpPosition = Vector3.zero;
    public void WarpToPosition(Vector3 newPosition) {
        warpPosition = newPosition;
    }

    public void Reset() {
        warpPosition = new Vector3(0f, 0.1f, 0f);
    }

    void Start() {
        gm = GameManager.GetInstance();
        gm.player = this;

        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;

    }

    void Update() {
        if ( gm.currentState != GameManager.GameState.GAME ) {
            Debug.Log(gm.currentState);
            return;
        }

        if ( Input.GetKeyDown(KeyCode.Escape) && gm.currentState == GameManager.GameState.GAME ) {
            gm.changeState(GameManager.GameState.PAUSE);
            return;
        }

        gm.remainingTime -= 1 * Time.deltaTime;
        if ( gm.remainingTime <= 0 ) {
            gm.EndGame();
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        //Verificando se é preciso aplicar a gravidade
        float y = 0;
        if ( !characterController.isGrounded ) {
            y = -_gravidade;
        }

        //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        //Tratando a rotação da câmera
        cameraRotation += mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

        characterController.Move(direction * _baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
    }

    void LateUpdate() {
        RaycastHit hit;

        Debug.DrawRay(transform.position, playerCamera.transform.forward* 10.0f, Color.magenta);
        if (gm.currentState == GameManager.GameState.GAME) {
            if ( Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 5.0f) ) {
                if ( hit.collider.gameObject.Equals(chest) ) {
                    gm.EndGame();
                }
                //gm.EndGame();
            }
        }
        //if ( Physics.Raycast(playerCamera.transform.position, transform.forward, out hit, 100.0f) ) {
        //    Debug.Log(hit.collider.name);
        //}
        if ( warpPosition != Vector3.zero ) {
            transform.position = warpPosition;
            warpPosition = Vector3.zero;
        }
    }
}