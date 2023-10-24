using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadSystem : MonoBehaviour
{
    [SerializeField] private GameObject _playerGameObject;
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SkillDataSaveLoadSystem.SaveSkillData();
            RuntimePlayerData.PlayerData.Position = _playerGameObject.transform.position;
            PlayerDataSaveLoadSystem.SavePlayerData();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SkillDataSaveLoadSystem.LoadSkillData();
            PlayerDataSaveLoadSystem.LoadPlayerData();
            CharacterController characterController = _playerGameObject.GetComponent<CharacterController>();
            characterController.enabled = false;
            _playerGameObject.transform.position = RuntimePlayerData.PlayerData.Position;
            characterController.enabled = true;
        }
    }
}
