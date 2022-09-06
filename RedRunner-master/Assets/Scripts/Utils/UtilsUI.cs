using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RedRunner.UI
{
    public static class UtilsUI
    {
        public static void SetButtonAction(this Button button, Action action)
        {
            RemoveAllLis();
            AddLis();
        }
        void AddLis()
        {
            button.onClick.AddListener(() => action());
        }
        void RemoveAllLis()
        {
            button.onClick.RemoveAllListeners();
        }
    }
}