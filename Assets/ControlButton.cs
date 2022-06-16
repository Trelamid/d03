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

    private int towerLvl = 1;
    
    private GameObject _spawnedTowerSprite;
    private Camera _camera;

    private gameManager _gameManager;
    private Image _thisImage;
    
    //states
    private int _cost;

    // Start is called before the first frame update
    void Start()
    {
        _cost = _spawnObj1.GetComponent<towerScript>().energy;
        _gameManager = GameObject.Find("GameManager").GetComponent<gameManager>();
        _SpawnSpObj = _SpawnObj.GetComponent<Image>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _thisImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        _costText.text = _cost.ToString();
        
        if(_gameManager.playerEnergy >= _cost)
            _thisImage.color = Color.white;
        else
            _thisImage.color = Color.red;
        
        if (_spawnedTowerSprite)
        {
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log(1);
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
        var aud = Physics2D.OverlapPoint(_camera.ScreenToWorldPoint(Input.mousePosition));
        // Debug.Log(aud.gameObject.tag);
        if (aud && aud.gameObject.tag == "empty")
        {
            if (towerLvl == 1)
            {
                _gameManager.playerEnergy -= _cost;
                var obj = Instantiate(_spawnObj1, _camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
                obj.transform.position = new Vector3(obj.transform.position.x,
                    obj.transform.position.y, 0);
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
        if(_gameManager.playerEnergy < _cost)
            return;
        Debug.Log(gameObject.name);
        // Instantiate(_SpawnObj, _camera.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity, transform);
        // return;
        if (towerLvl == 1)
        {
            _SpawnSpObj.sprite = _spawnSpite1;
            _spawnedTowerSprite = Instantiate(_SpawnObj, Input.mousePosition, Quaternion.identity, transform);
        }
        // else if(towerLvl == 2)
        // {
        //     _SpawnSpObj.sprite = _spawnSpite2;
        //     _spawnedTowerSprite = Instantiate(_SpawnObj, Input.mousePosition, Quaternion.identity, transform);
        // }
        // else if(towerLvl == 3)
        // {
        //     _SpawnSpObj.sprite = _spawnSpite3;
        //     _spawnedTowerSprite = Instantiate(_SpawnObj, Input.mousePosition, Quaternion.identity, transform);
        // }
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
