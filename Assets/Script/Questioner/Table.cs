using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BTLGeek
{
    public class Table : MonoBehaviour
    {
		/*---- メンバ ----*/
		[field: Header("プレハブ")]
		/// <summary>
		/// 食品を入れるフレームのプレハブ
		/// </summary>
		[field: Tooltip("フレームのプレハブをアタッチしてください")]
		[field: SerializeField]
		private GameObject framePrefab_ = null;


		[field: Header("デバック時に見るよう")]
		/// <summary>
		/// テーブルのインデックス
		/// </summary>
		public int TableIndex = 0;

		/// <summary>
		/// フレームの幅の半分の長さ
		/// </summary>
		private const int FRAME_HALF_WIDTH = 1;

		/// <summary>
		/// フレームの高さの半分の長さ
		/// </summary>
		private const int FRAME_HALF_HEIGHT = 1;

		/// <summary>
		/// フレームの横の最大数
		/// </summary>
		private const int FRAME_SIDES_MAX = 2;

		/// <summary>
		/// フレームのリスト
		/// </summary>
		private List<Frame> frameList_ = null;



		/*---- メソッド ----*/
		/// <summary>
		/// インスタンス生成時に呼ばれる
		/// </summary>
		private void Awake()
		{
			if (framePrefab_ == null) {
				Debug.LogError("フレームのプレハブがアタッチされていません。\nインスペクター上からアタッチして下さい。");
			}
		}

		/// <summary>
		/// フレームの作成
		/// </summary>
		/// <param name="frameNum"></param>
		public void CreateFrame(int frameNum)
	    {
			//フレームを数分生成
			frameList_ = new List<Frame>( );
			for(int i = 0; i<frameNum; i++) {
				Frame frame = Instantiate(framePrefab_, transform).GetComponent<Frame>();
				frame.TableIndex = TableIndex;
				frame.FrameIndex = i;
				frame.SetFood( );
				frameList_.Add(frame);
			}

			//フレームを並べる初期位置を生成
			int frameY = 0;
			for(int i = 0; i < ((frameNum/FRAME_SIDES_MAX)-1); i++) {
				frameY += FRAME_HALF_HEIGHT;
			}

			//フレームを並び変え
			for(int i = 0, j = 0; i < (frameNum/FRAME_SIDES_MAX); i++) {
				// 左側
				Vector3 pos = frameList_[j].transform.localPosition;
				pos.y = frameY;
				pos.x = FRAME_HALF_WIDTH;
				frameList_[j].transform.localPosition = pos;
				j++;
				// 右側
				pos = frameList_[j].transform.localPosition;
				pos.y = frameY;
				pos.x = -FRAME_HALF_WIDTH;
				frameList_[j].transform.localPosition = pos;
				j++;

				// 次の高さを更新
				frameY -= FRAME_HALF_HEIGHT*2; 
			}
	    }
    }


}