using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerUI : MonoBehaviour
{
    public TextMeshProUGUI textUp;
    public TextMeshProUGUI textDown;
    
    public GameObject upgrade;
    public GameObject downgrade;

    public GameObject thisTower;

    private gameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _gameManager = FindObjectOfType<gameManager>();
        if(upgrade)
            textUp.text = upgrade.GetComponent<towerScript>().energy.ToString();
        if(downgrade)
            textDown.text = downgrade.GetComponent<towerScript>().energy.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
        }
    }

    public void Upgrade()
    {
        if (upgrade && _gameManager.playerEnergy >= upgrade.GetComponent<towerScript>().energy)
        {
            _gameManager.playerEnergy -= upgrade.GetComponent<towerScript>().energy;
            
            Instantiate(upgrade, thisTower.transform.position, Quaternion.identity);
            Destroy(thisTower);
            Destroy(this.gameObject);
        }
    }

    public void DownGrade()
    {
        if (downgrade)
        {
            _gameManager.playerEnergy += downgrade.GetComponent<towerScript>().energy;
            
            Instantiate(downgrade, thisTower.transform.position, Quaternion.identity);
            Destroy(thisTower);
            Destroy(this.gameObject);
        }
    }
    
    public void Cancel()
    {
        Destroy(gameObject);
    }
}
