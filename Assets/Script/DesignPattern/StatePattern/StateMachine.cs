using UnityEngine;

namespace BTLGeek.DesignPattern
{
    /// <summary>
    /// <para>
    /// クラス名：ステートマシン<br/>
    /// </para>
    /// <para>
    /// -- 概要 --<br/>
    /// ステートを管理するクラス<br/>
    /// </para>
    /// <para>
    /// -- 詳細 --<br/>
    /// ステートパターンを使いたい、コンポーネントクラスに持たせる。
    /// Stateを継承したクラスを用意して、ChangeStateメソッドでステートを設定する。
    /// </para>
    /// </summary>
    /// <typeparam name="T">ステートマシンを所持している、コンポーネントを継承したクラス</typeparam>
    public class StateMachine<T> where T : MonoBehaviour
    {
        /// <summary>
        /// ステートマシーンを所持しているコンポーネント
        /// </summary>
        private T owner_ = null;

        /// <summary>
        /// 現在のステート
        /// </summary>
        private State<T> state_ = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="owner">ステートマシーンを所持しているコンポーネント</param>
        public StateMachine(T owner)
		{
            owner_ = owner;
		}

        /// <summary>
        /// 毎フレーム呼ばれる
        /// </summary>
        public void Update()
        {
            state_?.Update(owner_);
        }

        /// <summary>
		/// 設定している間隔で、Update前に呼ばれる
		/// </summary>
		public void FixedUpdate()
		{
            state_?.FixedUpdate(owner_);
		}

        /// <summary>
        /// 毎フレーム、全てのUpdateが呼ばれた後に呼ばれる
        /// </summary>
		public void LateUpdate()
		{
            state_?.LateUpdate(owner_);
		}


        public void ChangeState<S>() where S :State<T>, new()
		{
            state_?.Finish(owner_);
            state_ = new S( );
            state_.Start(owner_);
        }
	}
}