using UnityEngine;

namespace BTLGeek.DesignPattern
{
	/// <summary>
	/// <para>
	///	クラス名：ステートクラス
	///	</para>
	///	<para>
	/// -- 概要 --<br/>
	/// ステート作成時に継承するクラス。<br/>
	/// </para>
	/// <para>
	/// -- 詳細 --<br/>
	/// 継承先に、引数付きコンストラクタを作成するのはNG!!<br/>
	/// </para>
	/// </summary>
	/// <typeparam name="T">ステートマシンを所持している、コンポーネントを継承したクラス</typeparam>
	public abstract class State<T> where T : MonoBehaviour
	{
		/// <summary>
		/// State生成時に呼ばれる
		/// </summary>
		/// <param name="owner">ステートマシンを所持しているコンポーネント</param>
		abstract public void Start(T owner);
		/// <summary>
		/// ステート切り替え時に呼ばれる
		/// </summary>
		/// <param name="owner">ステートマシンを所持しているコンポーネント</param>
		abstract public void Finish(T owner);
		/// <summary>
		/// 毎フレーム呼ばれる
		/// </summary>
		/// <param name="owner">ステートマシンを所持しているコンポーネント</param>
		abstract public void Update(T owner);
		/// <summary>
		/// 設定している間隔で、Update前に呼ばれる
		/// </summary>
		/// <param name="owner">ステートマシンを所持しているコンポーネント</param>
		abstract public void FixedUpdate(T owner);
		/// <summary>
		/// Updateの後に呼ばれる
		/// </summary>
		/// <param name="owner">ステートマシンを所持しているコンポーネント</param>
		abstract public void LateUpdate(T owner);

	}
}