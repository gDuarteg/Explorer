using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float _baseSpeed = 10.0f;
    float _gravidade = 9.8f;
    GameManager gm;

    public GameObject chest;

    CharacterController characterController;

    //Refer�ncia usada para a c�mera filha do jogador
    GameObject playerCamera;
    //Utilizada para poder travar a rota��o no angulo que quisermos.
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

        //Verificando se � preciso aplicar a gravidade
        float y = 0;
        //if ( !characterController.isGrounded ) {
        //    y = -_gravidade;
        //}

        if (Input.GetKeyDown(KeyCode.Space)) {
            //transform.position += new Vector3(0, 100, 0) * 10 * Time.deltaTime;
            //characterController.SimpleMove(new Vector3(0, 100, 0));
            //characterController.Move(new Vector3(0, 100, 0) * 1 * Time.deltaTime);
            //y = 10;

            warpPosition = new Vector3(0, 5, 0);



            //Vector3 aaa = transform.right * x + transform.up * y + transform.forward * z;

            //characterController.Move(aaa * _baseSpeed * Time.deltaTime);
        }

        //Tratando movimenta��o do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        //Tratando a rota��o da c�meras
        cameraRotation += mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

        Debug.Log(direction * _baseSpeed * Time.deltaTime);
        characterController.Move(direction * _baseSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, mouse_dX);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
    }

    void LateUpdate() {
        if (gm.currentState != GameManager.GameState.GAME) {
            return;
        }

        RaycastHit hit;
        Debug.DrawRay(transform.position, playerCamera.transform.forward* 5.0f, Color.magenta);
        if (gm.currentState == GameManager.GameState.GAME) {
            if ( Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 5.0f) ) {
                if ( hit.collider.gameObject.Equals(chest) ) {
                    gm.EndGame();
                    Reset();
                }
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