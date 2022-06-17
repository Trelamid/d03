using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlButton : MonoBehaviour
{
    [SerializeField] private GameObject _SpawnObj;
    private Image _SpawnSpObj;
    
    [SerializeField]private Sprite _spawnSpite1;
    // [SerializeField]private Sprite _spawnSpite2;
    // [SerializeField]private Sprite _spawnSpite3;
    
    [SerializeField]private GameObject _spawnObj1;
    // [SerializeField]private GameObject _spawnObj2;
    // [SerializeField]private GameObject _spawnObj3;

    public TextMeshProUGUI _costText;
    public TextMeshProUGUI _damageText;
    public Image _flyImage;
    public TextMeshProUGUI _aimText;
    public TextMeshProUGUI _timeText;

    public Sprite fly;
    public Sprite notFly;

    private int towerLvl = 1;
    
    private GameObject _spawnedTowerSprite;
    private static bool _spawn;
    private Camera _camera;

    private gameManager _gameManager;
    private Image _thisImage;

    public KeyCode key;
    
    //states
    private int _cost;
    
    public int delay = 0;
    private int time;

    // Start is called before the first frame update
    void Start()
    {
        var auf = _spawnObj1.GetComponent<towerScript>();
        if (auf)
            _cost = auf.energy;
        else
            _cost = 0;
        
        if(_costText)
            _costText.text = _cost.ToString();

        if(_damageText)
            _damageText.text = _spawnObj1.GetComponent<towerScript>().damage.ToString();
        if (_flyImage && fly && notFly && _spawnObj1.GetComponent<towerScript>().type == towerScript.Type.canon)
            _flyImage.sprite = notFly;
        else
        {
            if (_cost != 0)
            {
                // Debug.Log("bl");
                _flyImage.sprite = fly;
            }
        }

        if(_timeText)
            _timeText.text = _spawnObj1.GetComponent<towerScript>().fireRate.ToString();
        if(_aimText)
            _aimText.text = _spawnObj1.GetComponent<towerScript>().range.ToString();
        
        _gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        _SpawnSpObj = _SpawnObj.GetComponent<Image>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _thisImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if( Input.GetKeyDown(key))
            Click();
        
        if(_gameManager.playerEnergy >= _cost)
            _thisImage.color = Color.white;
        else
            _thisImage.color = Color.red;
        
        if (_spawnedTowerSprite)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(1);
                _spawn = false;
                Destroy(_spawnedTowerSprite);
                SpawnTower();
            }
            else
            {
                // _spawnedTowerSprite.transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
                _spawnedTowerSprite.transform.position = Input.mousePosition;
            }
        }
    }

    void SpawnTower()
    {
        // var aud = Physics2D.OverlapCircleAll(_camera.ScreenToWorldPoint(Input.mousePosition), 1f);
        var aud = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
        // foreach (var VARIABLE in aud)
        // {
        //     Debug.Log(VARIABLE);
        //     VARIABLE.GetComponent<SpriteRenderer>().color = Color.red;
        //     if(VARIABLE.gameObject.tag != "empty")
        //         return;
        // }
        // Debug.Log(aud.gameObject.tag);
        if (aud && aud.gameObject.tag == "empty")
        {
            if (towerLvl == 1)
            {
                _gameManager.playerEnergy -= _cost;
                var obj = Instantiate(_spawnObj1, aud.transform.position, Quaternion.identity);
                obj.transform.position = new Vector3(obj.transform.position.x,
                    obj.transform.position.y, 0);
                if(_cost != 0)
                    Destroy(aud);
            }
            else if (towerLvl == 2)
            {

            }
            else if (towerLvl == 3)
            {

            }
        }
    }

    public void Click()
    {
        if (_cost == 0 && Time.time > time)
        {
            time = (int)Time.time + delay;
        }
        else if(_cost == 0)
            return;
        
        if(_spawnedTowerSprite || _spawn)
            return;
        if(_gameManager.playerEnergy < _cost)
            return;
        _spawn = true;
        if (towerLvl == 1)
        {
            _SpawnSpObj.sprite = _spawnSpite1;
            _spawnedTowerSprite = Instantiate(_SpawnObj, Input.mousePosition, Quaternion.identity, transform);
        }
    }

    public void IncreaseTowerLvl()
    {
        towerLvl++;
        // if (towerLvl == 2)
        // {
        //     GetComponent<Image>().sprite = _spawnSpite2;
        //
        // }
        // else if (towerLvl == 3)
        // {
        //     GetComponent<Image>().sprite = _spawnSpite3;
        // }
    }
}
