using UnityEngine;
using UnityEngine.UI;   //引用 介面 API
using System.Collections;

public class npc : MonoBehaviour
{
    #region 欄位
    // 定義列舉
    //修飾詞 列舉 列舉名稱 { 列舉內容, .... }
    public enum state
    {
        //一般、尚未完成、完成
        normal, notcomplete, complete
    }
    //使用列舉
    //修飾詞 類型 名稱
    public state _state;

    [Header("對話")]
    public string sayStart = "你必須拿到寶箱給我";
    public string sayNotComplete = "還沒拿到寶箱嗎?";
    public string sayComplete = "做得好!";
    [Header("對話速度")]
    public float speed = 1.5f;
    [Header("任務相關")]
    public bool complete;
    public int countPlayer;
    public int countFinish = 1;
    [Header("介面")]
    public GameObject objCanvas;
    public Text textSay;
    #endregion

    public AudioClip soundSay;

    private AudioSource aud;

    public static npc score;

    private void Start()
    {
        aud = GetComponent<AudioSource>();

        score = this;
    }
    //2D 觸發事件
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //如果碰到物件為"player"
        if (collision.tag == "player")
        {
            Say();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "player")
        {
            SayClose();
        }
    }

    /// <summary>
    /// 對話:打字效果
    /// </summary>
    private void Say()
    {
        //畫布.顯示
        objCanvas.SetActive(true);
        StopAllCoroutines();

        if (countPlayer >= countFinish)
        {
            _state = state.complete;
        }

        //文字介面.文字 = 對話1
        switch (_state)
        {
            case state.normal:
                StartCoroutine(ShowDialog(sayStart));               //開始對話
                _state = state.notcomplete;
                break;
            case state.notcomplete:
                StartCoroutine(ShowDialog(sayNotComplete));         //未完成對話
                break;
            case state.complete:
                StartCoroutine(ShowDialog(sayComplete));            //完成對話
                break;
        }
    }

    private IEnumerator ShowDialog(string say)
    {
        textSay.text = "";                               //清空文字

        for (int i = 0; i < say.Length; i++)             //迴圈跑對話.長度
        {
            textSay.text += say[i].ToString();           //累加每個文字
            aud.PlayOneShot(soundSay, 1.5f);
            yield return new WaitForSeconds(speed);      //等待
        }
    }

    /// <summary>
    /// 關閉對話
    /// </summary>
    private void SayClose()
    {
        objCanvas.SetActive(false);
        StopAllCoroutines();
    }

    /// <summary>
    /// 玩家取得道具
    /// </summary>
    public void PlayerGet()
    {
        countPlayer++;
    }
}
