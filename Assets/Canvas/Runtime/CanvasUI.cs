using System;
using UnityEngine;

namespace NoodleEater.Canvas
{
    public class CanvasUI
    {
        [SerializeField] protected GameObject root;
        
        public Action OnInitialized;
        public Action OnShown;
        public Action OnClosed;

        public virtual void Initialize()
        {
            if(!root) return;
            root.SetActive(false);
        }

        public virtual void Show()
        {
            if(!root) return;
            root.SetActive(true);
            OnShown?.Invoke();
        }

        public virtual void Close()
        {
            if(!root) return;
            root.SetActive(false);
            OnClosed?.Invoke();
        }
    }
}