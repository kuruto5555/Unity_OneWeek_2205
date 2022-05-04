using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MyInput : MonoBehaviour, Unity_OneWeek_2205.IPlayerActions
{
    public static MyInput _instance { get; private set; }

    public Vector3 MousePos { get; private set; } = Vector3.zero;




    private Unity_OneWeek_2205 _input = null;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

  
    public void OnTap(InputAction.CallbackContext context)
    {
        //押された瞬間
        if (context.started)
        {
            Debug.Log("押されたお");

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
        }
    }
}
