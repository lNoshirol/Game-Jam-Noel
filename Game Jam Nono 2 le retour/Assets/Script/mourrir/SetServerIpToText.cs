using FishNet.Example;
using FishNet.Transporting.Tugboat;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;

public class SetServerIpToText : MonoBehaviour
{
    public TextMeshProUGUI _TEXTE;

    private void Start()
    {
        CHANGELETEXT();
    }

    public void CHANGELETEXT()
    {
        _TEXTE.text = Dns.GetHostEntry(Dns.GetHostName())
        .AddressList.First(
        f => f.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
        .ToString();
    }
}
