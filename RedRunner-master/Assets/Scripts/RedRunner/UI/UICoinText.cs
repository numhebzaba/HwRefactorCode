using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RedRunner.Collectables;
using System;
using DG.Tweening;

namespace RedRunner.UI
{
    public class UICoinText : UIText
    {
        [SerializeField]
        protected string m_CoinTextFormat = "x {0}";

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            GameManager.Singleton.m_Coin.AddEventAndFire(UpdateCoinsText, this);
        }

        private void UpdateCoinsText(int newCoinValue)
        {
            GetComponent<Animator>().SetTrigger("Collect");
            text = string.Format(m_CoinTextFormat, newCoinValue);
            DoFadeTween();
        }
        void DoFadeTween()
        {
            material.DOFade(80, 1);
            material.DOFade(100, 0.5f);
        }
    }
}