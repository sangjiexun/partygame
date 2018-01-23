/*************************************************************
   Copyright(C) 2017 by dayugame
   All rights reserved.
   
   MoveCamera.cs
   PartyRhythmGame
   
   Created by WuIslet on 2018-01-08.
   
*************************************************************/

using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace isletspace
{
    public class LookUpCamera : MoveCamera
    {
        [Tooltip("For Debug")]
        public bool isPlaying;

        #region const
        private Vector3 LookFront = new Vector3(0, 180 - 40, 0);
        private Vector3 LookRotatePoint1 = new Vector3(0, 180, 0);
        private Vector3 LookRotatePoint2 = new Vector3(0, 180 + 40, 0);
        #endregion

        public int DoCameraMove(Vector3 target)
        {
            ResetPos();

            if (target == null)
            {
                return -1;
            }

            if (isPlaying)
            {
                return -2;
            }

            isPlaying = true;

            Sequence seq = DOTween.Sequence();
            //ZoomIn
            seq.Append(transform.DOMove(target, 0.8f).SetEase(Ease.OutExpo));
            seq.Join(transform.DORotate(LookFront, 0.8f));
            //gap
            seq.AppendInterval(0.3f);
            //Rotate
            seq.Append(transform.DORotate(LookRotatePoint1, 1.4f).SetEase(Ease.Linear));
            seq.Append(transform.DORotate(LookRotatePoint2, 1.4f).SetEase(Ease.Linear));
            //gap
            seq.AppendInterval(0.3f);
            //ZoomOut
            seq.Append(transform.DOMove(StartPos, 0.8f));
            seq.Join(transform.DORotate(StartRotate, 0.8f));
            //PlayEnd
            seq.AppendCallback(() => { isPlaying = false; });

            return 0;
        }
    }
}