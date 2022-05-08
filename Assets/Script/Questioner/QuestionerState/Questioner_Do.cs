using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTLGeek.Manager;


namespace BTLGeek.State
{
	public class Questioner_Do : DesignPattern.State<Questioner>
	{
		/*---- メンバ ----*/


		/*---- メソッド ----*/
		public override void Start(Questioner owner)
		{
			// 出題
			owner.CreateQuestion( );

			// ステート切り替え
			owner.stateMachine_.ChangeState<Quesioner_Idling>( );
		}

		public override void Finish(Questioner owner)
		{
		}

		public override void Update(Questioner owner)
		{
		}

		public override void FixedUpdate(Questioner owner)
		{
		}

		public override void LateUpdate(Questioner owner)
		{
		}
	}

}
