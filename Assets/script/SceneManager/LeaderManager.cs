/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   LeaderManager.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-16.
   
*************************************************************/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace isletspace
{
    /// <summary>
    /// 
    /// </summary>
    public class LeaderManager : MonoBehaviour
    {
        public TweenLight spotLight;
        public TweenLight fireLight;
        public GameObject torch;
        public DancerAni dancer;
        public Transform cameraLookPoint;
        
        [Tooltip("For Debug")]
        public bool isCameraFollow = false;

        private Transform mainCamera;
        private Vector3 oldCamera;

        public void PlayOP()
        {
            StartCoroutine(OPRoute());
        }

        IEnumerator OPRoute()
        {
            //��ʼ������
            StartCameraFollow();
            yield return new WaitForSeconds(1f);
            dancer.PlayOP();
            yield return new WaitForSeconds(0.3f);
            //�볡����
            spotLight.Begin();
            SoundManager.Instance.PlayLaZha();
            yield return new WaitForSeconds(2.8f);
            SoundManager.Instance.PlayFoot();
            yield return new WaitForSeconds(5.5f);
            //���������
            SoundManager.Instance.PlayBackground(3);
            EndCameraFollow();
            spotLight.End();
            StartCoroutine(dancer.DrumConstant(1, 5, 0.5f));
            yield return new WaitForSeconds(5);
            //��������
            dancer.PlayOPPose();
            torch.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            fireLight.Begin();
        }

        private void StartCameraFollow()
        {
            enabled = true;
            mainCamera = Director.Instance.currentCamera.transform;
            oldCamera = mainCamera.position;
            isCameraFollow = true;
        }

        private void EndCameraFollow()
        {
            if (mainCamera == null)
            {
                return;
            }

            mainCamera.position = oldCamera;
            mainCamera = null;
            isCameraFollow = false;
            enabled = false;
        }

        private void Update()
        {
            if(isCameraFollow && mainCamera != null)
            {
                Vector3 pos = cameraLookPoint.position;
                pos.y = 0;
                mainCamera.position = pos;
                spotLight.transform.position = pos;
            }
        }

        public void PlayJoin()
        {
            dancer.PlayJoin();
        }

        public void PlayEndJoin()
        {
            dancer.PlayEndJoin();
        }
    }
}
