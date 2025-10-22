using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _playerTeam;
    [SerializeField] private int _price;
    [SerializeField] private Slider _slider;
    private bool _isClicked = false;
    public bool _isPay = false;
    private Button _button;

    private void Start()
    {
        _button = GetComponent<Button>();
    }

    void Update()
    {
        if(_isPay == true && _isClicked == false)
        {
            _button.enabled = true;
        }
        else
        {
            _button.enabled = false;
        }
    }

    public void OnClick()
    {
        _isPay = CoinManager.Instance.CheckPay(_price);
        if (_isPay == true && _isClicked == false)
        {
            PlayerSpawn();

            _isClicked = true;
        
            StartCoroutine(SliderUpDate());  
        }
    
    }

    private void PlayerSpawn()
    {
            float y = Random.Range(-2.9f, -3.1f);
            Instantiate( _player, new Vector3(-6, y, 0), Quaternion.identity, _playerTeam);
            CoinManager.Instance.SubCoin(_price);
    }

    IEnumerator SliderUpDate()
    {
        _slider.value = 0;
        _slider.gameObject.SetActive(true);

        while (_slider.value < _slider.maxValue) 
        {
            _slider.value++;
            yield return new WaitForSeconds(0.1f);
        }

        _slider.gameObject.SetActive(false);

        _isClicked=false;
    }
}
