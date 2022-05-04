using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour
{
    public static MoveController _instance { get; private set; }

    private bool _isFrameTouch = false;



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
        _isFrameTouch = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PutItem()
    {
        if (!_isFrameTouch) return;
    }
}
