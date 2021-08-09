using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

//EmoteKey: Happy = 0, Upset = 1, Exclaim = 2, Query = 3, Up = 4, Down = 5, Left = 6, Right = 7

public class EmoteSending : MonoBehaviour
{
    public UIBehaviours uiObject;
    public DPadPad dPad;

    PhotonView photonView;

    [Space(10)]
    [Header("EmoteKey: Happy = 0, Upset = 1, Exclaim = 2, Query = 3, Up = 4, Down = 5, Left = 6, Right = 7")]
    public List<Sprite> emoteSprites = new List<Sprite>();

    // Start is called before the first frame update
    void Start()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void ToIntelligence(int emote) //Called from diver's UI
    {
        photonView.RPC("RPC_ToIntelligence", RpcTarget.Others, emote);
    }

    public void ToDiver(int emote)
    {
        photonView.RPC("RPC_ToDiver", RpcTarget.Others, emote);
    }

    [PunRPC]
    void RPC_ToIntelligence(int emote)
    {
        if(dPad == null) { Debug.LogWarning("Unable to locate DPad. This is quite the big oof."); return; }
        dPad.DPadReceive(emote);
    }

    [PunRPC]
    void RPC_ToDiver(int emote)
    {
        if (dPad == null) { Debug.LogWarning("Unable to locate Diver UI. This is quite the big oof."); return; }
        uiObject.ReceiveFromIntelligence(emote);
    }
}
