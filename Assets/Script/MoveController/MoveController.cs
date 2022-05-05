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
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (hit.transform.tag != "Frame") return;
            //if(Frameの子に)

            //取得したFrameの子であるfoodの親をマウスオブジェクトに変更しFrameのインデックスも取得する
            Debug.Log(hit.transform.name);

            _isItemTouch = true;
        }

        //if(hit.collider)
        //{
        //    //hit.transform.tag
        //
        //    //hit.transform.parent = this.transform;  //拾ったアイテムの親をマウスにする
        //
        //
        //
        //    Debug.Log(hit.transform.name);
        //
        //    _isItemTouch = true;
        //
        //}

    }

    /// <summary>
    /// ボタンを離した時に置く
    /// </summary>
    public void PutItem()
    {
        if (!_isItemTouch) return;

        Vector3 screenPos = Input.mousePosition;

        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        //ItemとFrameレイヤーだけ衝突
        int layerMask = 1 << 8;

        RaycastHit2D hit = Physics2D.Raycast((Vector2)ray.origin, (Vector2)ray.direction, layerMask);

        foreach (RaycastHit2D h in Physics2D.RaycastAll((Vector2)ray.origin, (Vector2)ray.direction))
        {
            if (h.transform.tag != "Frame") return;

            //取得したFrameの子であるfoodの親をマウスオブジェクトに変更しFrameのインデックスも取得する
            //自分の子にしてあるfoodを取得したFrameの子に変更しアイテム置き換え処理を呼ぶ
            Debug.Log(h.transform.name);
        }

        _isItemTouch = false; //拾ってるかフラグを戻す
    }
}
