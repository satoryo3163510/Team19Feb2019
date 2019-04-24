﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ChangeCamera : MonoBehaviour
{
    public GameObject mainCamera;       //三人称カメラ
    public GameObject fpsCamera;        //一人称カメラ

    private GameObject playerObject;    //回転の中心となるプレイヤー
    private float rotateSpeed = 0.5f;   //回転の速さ
    public Image sight;                 //ロックオンサイト
    public bool changeflag;
    
    // Start is called before the first frame update
    //開始時は三人称、ScopeModeはオフに
    void Start()
    {
        playerObject = GameObject.Find("PlayerSenkan");
        gameObject.GetComponent<ScopeMode>().enabled = false;
        sight.enabled = false;
        changeflag = true;
    }

    // Update is called once per frame
    void Update()
    {
        //rotateCameraの呼び出し
        RotateCamera();
        
        //右クリックが押されたらカメラのアクティブを切り替える
        if (Input.GetMouseButtonDown(1))
        {
            mainCamera.SetActive(!mainCamera.activeSelf);
            fpsCamera.SetActive(!fpsCamera.activeSelf);
        }
    }

    //一人称の時にマウス操作でカメラと同時に旋回することで
    //三人称のカメラとの違和感を軽減できる？（保留）
    private void RotateCamera()
    {
        //transform.RtateAround()をしようしてメインカメラを回転させる
        //fpsCameraがactiveのときはsightUIを表示する
        if (mainCamera.activeSelf)
        {
            //Vector3でX,Y方向の回転度合いの定義
            Vector3 angl = new Vector3(Input.GetAxis("Mouse X") * -rotateSpeed, Input.GetAxis("Mouse Y") * -rotateSpeed, 0);
            mainCamera.transform.RotateAround(playerObject.transform.position, Vector3.down, angl.x);
            mainCamera.transform.RotateAround(playerObject.transform.position, transform.right, angl.y);
            gameObject.GetComponent<ScopeMode>().enabled = false;
            sight.enabled = false;
        }
        else if (fpsCamera.activeSelf)
        {
            Vector3 angl = new Vector3(Input.GetAxis("Mouse X") * -rotateSpeed * 0.4f, Input.GetAxis("Mouse Y") * -rotateSpeed * 0.4f, 0);
            fpsCamera.transform.RotateAround(playerObject.transform.position, Vector3.down, angl.x);
            fpsCamera.transform.RotateAround(playerObject.transform.position, transform.right, angl.y);
            gameObject.GetComponent<ScopeMode>().enabled = true;
            sight.enabled = true;
        }
    }
}
