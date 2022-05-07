using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BTLGeek.Manager
{
    public class MoveCountManager : SingletonMonoBehaviour<MoveCountManager>
    {

        /// <summary>
        /// 今のMoveCount
        /// </summary>
        public int MoveCount { get; private set; } = 0;

        // Start is called before the first frame update
        void Start()
        {
            MoveCount = 0;
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// お題が更新された時に呼ぶ
        /// </summary>
        /// <param name="num">動かせる回数</param>
        public void SetCount(int num)
        {
            MoveCount = num;
        }

        /// <summary>
        /// Foodを動かせたタイミングで呼ぶ
        /// </summary>
        public void DecrementMoveCount()
        {
            if (MoveCount <= 0) return;

            MoveCount--;
        }
    }

}