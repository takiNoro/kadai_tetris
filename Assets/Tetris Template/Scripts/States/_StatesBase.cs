using UnityEngine;
using System.Collections;
using System;
using UniRx;

public abstract class _StatesBase
{
		public Action ActiveAction { get; set; }
        public Action DeactivateAction { get; set; }

        //実行処理
        public virtual void ExecuteActive()
        {
            if (ActiveAction != null) ActiveAction();
        }

		public virtual void ExecuteDeactivate()
        {
            if (DeactivateAction != null) DeactivateAction();
        }


        //ステート名を取得するメソッド
        public abstract string GetStateName();
}
