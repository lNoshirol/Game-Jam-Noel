using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerChangeName : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _playerNameText;
    [SerializeField] TextMeshProUGUI _inputFieldText;

    public void ChangePlayerName()
    {
        gameObject.name = _inputFieldText.text;
        _playerNameText.text = _inputFieldText.text;
    }

    public void NameConfirmed()
    {
        if (_inputFieldText.text == string.Empty)
        {
            _playerNameText.text = "Player" + Random.Range(0, 100000);
        }
    }
}