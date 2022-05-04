using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInput : MonoBehaviour, Unity_OneWeek_2205.IPlayerActions
{
    public static MyInput _instance { get; private set; }

    [Header("スワイプ判定になるまでの距離")]
    [SerializeField] float SwipeLen = 1.0f;



    /// <summary>
    /// マウスの現在の座標
    /// </summary>
    public Vector3 MousePos { get; private set; } = Vector3.zero;

    /// <summary>
    /// タッチされた時の開始座標
    /// </summary>
    public Vector3 StartMousePos { get; private set; } = Vector3.zero;

    /// <summary>
    /// スワイプ判定に使う長さ
    /// </summary>
    private float _swipeLen = 0.0f;


    private Unity_OneWeek_2205 _input = null;

    /// <summary>
    /// スワイプされたかどうか
    /// </summary>
    private bool _isSwipe = false;

    /// <summary>
    /// 画面に触れているかどうか
    /// </summary>
    private bool _isTouch = false;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Destroy(this);
            Debug.LogError("MyInputはすでに別オブジェクトにアタッチされていた為破棄されました。アタッチされているObjectは" + _instance.gameObject.name);
            return;
        }

        _input = new Unity_OneWeek_2205();
        _input.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        _input.Enable();
    }

    void OnDisable()
    {
        _input.Disable();

    }

        // Start is called before the first frame update
    void Start()
    {
        _isSwipe = false;
        _isTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        StateUpdate();
    }

    private void StateUpdate()
    {
        if (!_isTouch) return;

        MousePos = Input.mousePosition;

        _swipeLen = Vector3.Distance(MousePos, StartMousePos);

        if(SwipeLen < _swipeLen)
        {
            _isSwipe = true;
            Debug.Log("スワイプの判定になったで");
        }
    }

  
    public void OnTap(InputAction.CallbackContext context)
    {
        //押された瞬間
        if (context.started)
        {
            Debug.Log("押されたお");

            StartMousePos = Input.mousePosition;

            _isTouch = true;
        }

        //タップ判定の瞬間
        if (context.performed)
        {
            Debug.Log("タップの判定になったお");

        }

        //離された瞬間
        if (context.canceled)
        {
            Debug.Log("離されたお");

            _isTouch = false;
            _isSwipe = false;
        }
    }
}
