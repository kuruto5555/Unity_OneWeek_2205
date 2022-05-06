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

    }

    // Update is called once per frame
    void Update()
    {
        FoodCatchUpdate();
        
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

        foreach (RaycastHit2D h in Physics2D.RaycastAll((Vector2)ray.origin, (Vector2)ray.direction))
        {
            if (h.transform.tag != "Frame") continue;

            isFrame = true;

            //元の場所に戻す場合
            if(h.transform == _frameTransform)
            {
                PuttoFood(h.transform);

            }
            //新しい場所かつ別のアイテムが置かれていた場合
            else if(h.transform.childCount == 1)
            {
                TradeChild(_frameTransform, h.transform);
            }
            //新しい場所かつアイテムが置かれていなかった場合
            else
            {
                PuttoFood(h.transform);
            }
     


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
    /// Foodを置く
    /// </summary>
    /// <param name="frameTrans"></param>
    private void PuttoFood(Transform frameTrans)
    {
        _itemTransform.parent = frameTrans;
        _itemTransform.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Foodを持っていた時の更新
    /// </summary>
    private void FoodCatchUpdate()
    {
        if (!_isItemTouch) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = -1f;
        _itemTransform.position = pos;
    }
}
