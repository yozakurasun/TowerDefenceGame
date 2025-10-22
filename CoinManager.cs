using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinManager : SingletonMonoBehaviour<CoinManager>
{
    [SerializeField] private TextMeshProUGUI _coinText;

    private int _maxCoin = 1000;
    private float _nowCoin = 0;

    [SerializeField] private int _coinSpeed;

    // Start is called before the first frame update
    void Start()
    {
        _coinText.text = _nowCoin.ToString() + "/" + _maxCoin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(_nowCoin <= _maxCoin)
        {
            _nowCoin += Time.deltaTime * _coinSpeed;
            _coinText.text = _nowCoin.ToString("F0") + "/" + _maxCoin.ToString();
        }
    }

    public void SubCoin(int price)
    {
        _nowCoin -= price;
    }

    public bool CheckPay(int price)
    {
        return _nowCoin >= price;
    }
}
