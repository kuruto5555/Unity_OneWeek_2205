using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BTLGeek.DesignPattern;
using BTLGeek.Manager;
using BTLGeek.State;


namespace BTLGeek {
    public class Questioner : MonoBehaviour
    {
        /*---- メンバ ----*/
        /// <summary>
        /// テーブルの最大数
        /// </summary>
        public readonly int TABLE_NUM_MAX = 3;

        /// <summary>
        /// テーブルの最小数
        /// </summary>
        public readonly int TABLE_NUM_MIN = 2;

        /// <summary>
        /// テーブルの幅の半分
        /// </summary>
        private const float TABLE_HALF_WIDTH = 3;


        [field: Header("プレハブ")]
        /// <summary>
        /// テーブルのプレハブ
        /// </summary>
        [field: Tooltip("テーブルのプレハブをアタッチしてください")]
        [field: SerializeField]
        GameObject tablePrefub_ = null;

        [field: Header("デバッグ用")]
        /// <summary>
        /// テーブルの数
        /// </summary>
        [field: Tooltip("テーブルの数")]
        [field: SerializeField, Range(2, 3)]
        public int TabelNum { get; private set; }

        /// <summary>
        /// ステートマシーン
        /// </summary>
        public StateMachine<Questioner> stateMachine_ = null;

        /// <summary>
        /// テーブルのリスト
        /// </summary>
        private List<Table> tableList_ = null;

        /// <summary>
        /// 問題の難易度設定構造体
        /// </summary>
        [type: System.Serializable]
        public struct DifficultySetting
		{
            [field: Tooltip("このスコア以上の時の、問題難易度")]
            public int score;
            [field: Tooltip("食べ物の数")]
            public int foodNum;
            [field: Tooltip("最短手数の数(ランダムで、この値より±1されます。)")]
            public int efforts;
		}

        /// <summary>
        /// 難易度セッティング用のリスト
        /// </summary>
        [field: Header("難易度セッティング")]
        [field: Tooltip("スコアに応じて、難易度を設定してください。")]
        [field: SerializeField]
        private List<DifficultySetting> difficultySettingList = null;

        /// <summary>
        /// 難易度のインデックス
        /// </summary>
        private int difficultyIndex = 0;

        /// <summary>
        /// 移動回数管理クラス
        /// </summary>
        MoveCountManager moveCountManager_ = null;



        /*---- メソッド ----*/
        /// <summary>
        /// インスタンス生成時に呼ばれる
        /// </summary>
		private void Awake()
		{
            // エラー確認
            if(TabelNum < TABLE_NUM_MIN) {
                Debug.Log("テーブルの数が最小値(" + TABLE_NUM_MIN + ")より小さいです。\n最小値(" + TABLE_NUM_MIN + ")に補正します。\n");
                TabelNum = TABLE_NUM_MIN;
			}
            else if (TabelNum > TABLE_NUM_MAX) {
                Debug.Log("テーブルの数が最大値(" + TABLE_NUM_MAX + ")より大きいです。\n最大値(" + TABLE_NUM_MAX + ")に補正します。\n");
                TabelNum = TABLE_NUM_MAX;
            }
            else {
                // 正常な値
			}
            if (null == tablePrefub_) {
                Debug.LogError("テーブルのプレハブがアタッチされていません。\nインスペクター上からアタッチして下さい。");
            }
        }

		// Start is called before the first frame update
		void Start()
        {
            // ムーブコントローラーマネージャー取得
            moveCountManager_ = MoveCountManager.Instance;
            // ステートマシン生成
            stateMachine_ = new StateMachine<Questioner>(this);
            stateMachine_.ChangeState<Questioner_Start>( );
        }

        // Update is called once per frame
        void Update()
        {
            stateMachine_.Update( );
        }

        /// <summary>
        /// 出題する関数
        /// </summary>
        public void CreateQuestion()
		{
            // テーブル作成
            CreateTable( );

            // スコアによる難易度変更チェック
            // スコア取得
            int sucore = 1000;
            // 既に最高難易度の場合スキップ
            if(difficultyIndex < (difficultySettingList.Count-1)) {
                // 次の難易度のスコア以上になっていたら、インデックスを加算して次の難易度へ
                while (sucore >= difficultySettingList[difficultyIndex+1].score) {
                    difficultyIndex++;
                }
            }

            //フレームの数を計算(アイテム数+アイテム数を2で割ったあまり(奇数だった場合偶数にする))
            int frameNum = difficultySettingList[difficultyIndex].foodNum + (difficultySettingList[difficultyIndex].foodNum%2);

            // 移動回数設定
            moveCountManager_.SetCount(difficultySettingList[difficultyIndex].efforts);

            // 料理作成
            BurgerManager.Instance.SetBurger(
                difficultySettingList[difficultyIndex].foodNum,
                frameNum,
                TabelNum,
                difficultySettingList[difficultyIndex].efforts
            );

            // 各テーブルにフレーム作成
            for (int i = 0; i<tableList_.Count; i++) {
                tableList_[i].CreateFrame(frameNum);
			}
		}

        /// <summary>
        /// テーブルを作成する関数
        /// </summary>
        void CreateTable()
		{
            // テーブルリストにデータがあればクリア
            if (null != tableList_) {
                foreach (var tabel in tableList_) {
                    Destroy(tabel.gameObject);
                }
                tableList_.Clear( );
            }

            // テーブルリストを作成
            tableList_ = new List<Table>( );
            for (int i = 0; i<TabelNum; i++) {
                // テーブル生成
                Table table = Instantiate(tablePrefub_, transform).GetComponent<Table>();
                // テーブルインデックス割り当て
                table.TableIndex = i;
                // リストに保存
                tableList_.Add(table);
            }

            // テーブルを並べる初期位置を作成
            float tableX = 0f;
            for (int i = 0; i < (tableList_.Count-1); i++) {
                tableX -= TABLE_HALF_WIDTH;
            }
            // テーブルの整列
            for (int i = 0; i<tableList_.Count; i++) {
                // X座標更新
                Vector3 pos = tableList_[i].transform.localPosition;
                pos.x = tableX;
                tableList_[i].transform.localPosition = pos;
                // 次のX座標を幅の長さ(半分×2)分ずらす
                tableX += (TABLE_HALF_WIDTH * 2);
            }
        }
    }
}
