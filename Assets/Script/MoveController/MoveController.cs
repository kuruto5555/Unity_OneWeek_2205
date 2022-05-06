using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public static MoveController _instance { get; private set; }

    /// <summary>
    /// アイテムを拾えているかどうか
    /// </summary>
    private bool _isItemTouch = false;

    /// <summary>
    /// 最初Foodを掴んだ時のFrame
    /// </summary>
    private Transform _frameTransform = null;

    /// <summary>
    /// 掴んでいるFoodのTransform
    /// </summary>
    private Transform _itemTransform = null;

    /// <summary>
    /// マウスの現在の座標
    /// </summary>
    private Vector3 _mousePos = Vector3.zero;


    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Destroy(this);
            Debug.LogError("MoveContollerはすでに別オブジェクトにアタッチされていた為破棄されました。アタッチされているObjectは" + _instance.gameObject.name);
            return;
        }
    }

        // Start is called before the first frame update
    void Start()
    {
        _isItemTouch = false;

        _frameTransform = null;
        _itemTransform = null;

        _mousePos = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //アイテムもってた場合処理（仮）
        if(_isItemTouch)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = -1f;
            _itemTransform.position = pos;

        }
    }

    /// <summary>
    /// ボタン押した瞬間マウス位置にアイテムがあればつかむ
    /// </summary>
    public void CatchItem()
    {
        Vector3 screenPos = Input.mousePosition;
        
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
     
        foreach(RaycastHit2D hit in Physics2D.RaycastAll((Vector2)ray.origin, (Vector2)ray.direction))
        {
            Transform t = hit.transform;
            if (t.tag != "Frame") return;
            if(t.childCount == 0) return;

            _frameTransform = t;
            _itemTransform = t.GetChild(0);

            _itemTransform.parent = null; //アイテムの親を無くす

            //取得したFrameの子であるfoodの親をマウスオブジェクトに変更しFrameのインデックスも取得する
            Debug.Log(hit.transform.name);

            _isItemTouch = true;
        }

    }

    /// <summary>
    /// ボタンを離した時に置く
    /// </summary>
    public void PutItem()
    {
        if (!_isItemTouch) return;

        bool isFrame = false; //Frameがあったかどうか

        Vector3 screenPos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        //ItemとFrameレイヤーだけ衝突
        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, layerMask);

        foreach (RaycastHit2D h in Physics2D.RaycastAll((Vector2)ray.origin, (Vector2)ray.direction))
        {
            if (h.transform.tag != "Frame") continue;

            isFrame = true;

            //元の場所に戻す場合
            if(h.transform == _frameTransform)
            {
                PuttoDefault(h.transform);

            }
            //新しい場所かつ別のアイテムが置かれていた場合
            else if(h.transform.childCount == 1)
            {
                TradeChild(_frameTransform, h.transform);
            }
            //新しい場所かつアイテムが置かれていなかった場合
            else
            {
                PuttoNewPos(h.transform);
            }
      
            //自分の子にしてあるfoodを取得したFrameの子に変更しアイテム置き換え処理を呼ぶ



            Debug.Log(h.transform.name);
        }

        if (!isFrame)
        {
            _itemTransform.parent = _frameTransform; //元のFrameにアイテムを戻す
            _itemTransform.localPosition = Vector3.zero;
        }

        _frameTransform = null;
        _itemTransform = null;

        _isItemTouch = false; //拾ってるかフラグを戻す
    }

    /// <summary>
    /// 子供入れ替え用
    /// </summary>
    /// <param name="FirstParent">最初に選択した親</param>
    /// <param name="NextParent">置き換えたい親</param>
    private void TradeChild(Transform FirstParent, Transform NextParent)
    {
        Transform item = NextParent.GetChild(0);

        _itemTransform.parent = NextParent;
        _itemTransform.localPosition = Vector3.zero;

        item.parent = FirstParent;
        item.localPosition = Vector3.zero;
    }

    /// <summary>
    /// 持っているFoodを元の場所に戻す
    /// </summary>
    /// <param name="frameTrans"></param>
    private void PuttoDefault(Transform frameTrans)
    {
        _itemTransform.parent = frameTrans; //自分が保持しているアイテムの親を移動先Frameに変更
        _itemTransform.localPosition = Vector3.zero;
    }

    private void PuttoNewPos(Transform frameTrans)
    {
        _itemTransform.parent = frameTrans;
        _itemTransform.localPosition = Vector3.zero;
    }
}
