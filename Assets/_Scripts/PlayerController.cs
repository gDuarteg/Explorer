using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    float _baseSpeed = 10.0f;
    float _gravidade = 9.8f;
    float jumpForce = 200;
    bool doubleJump = false;
    public GameObject chest;
    
    GameManager gm;
    CharacterController characterController;
    //public Rigidbody rb;

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
        //rb = GetComponent<Rigidbody>();
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
            gm.SetSoundFx(SoundFXManager.ClipName.IDLE);

            return;
        }

        gm.remainingTime -= 1 * Time.deltaTime;
        if ( gm.remainingTime <= 0 ) {
            gm.EndGame();
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");



        //Verificando se é preciso aplicar a gravidade
        //float y = 0;
        //if (!characterController.isGrounded) {
        //    y = -_gravidade;
        //}
        
        //if(characterController.isGrounded) {
        //    y = -_gravidade;
            
        //    if (Input.GetKeyDown(KeyCode.Space)) {
        //        //transform.position += new Vector3(0, 100, 0) * 10 * Time.deltaTime;
        //        //characterController.SimpleMove(new Vector3(0, 100, 0));
        //        //characterController.Move(new Vector3(0, 100, 0) * 1 * Time.deltaTime);
        //        y = jumpForce;
        //    }
        //} else {
        //    y = -_gravidade;
        //}

        //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = Input.GetAxis("Mouse Y");

        //Tratando a rotação da câmeras
        cameraRotation += mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);
        
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            _baseSpeed = 20.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift)) {
            _baseSpeed = 10.0f;
        }

        // Sound Fx
        if ( _baseSpeed == 20.0f ) {
            gm.SetSoundFx(SoundFXManager.ClipName.RUNNING);
        } else if ( x != 0 || z != 0 ) {
            gm.SetSoundFx(SoundFXManager.ClipName.WALKING);
        } else {
            gm.SetSoundFx(SoundFXManager.ClipName.IDLE);
        }

        Vector3 moveDirection = new Vector3(x, 0, z);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= _baseSpeed;

        if (characterController.isGrounded) {
            doubleJump = false;
            if (Input.GetKeyDown(KeyCode.Space)) {
                moveDirection.y = jumpForce;
            }
        }

        if (!characterController.isGrounded) {
            moveDirection.y -= _gravidade;
        }

        characterController.Move(moveDirection * Time.deltaTime);

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