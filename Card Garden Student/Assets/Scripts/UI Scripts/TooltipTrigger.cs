using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum GameActorType
{
    Tower,
    Generator,
    Utility,
    Minion,
    Enemy
}

public class TooltipTrigger : MonoBehaviour // IPointerEnterHandler, IPointerExitHandler
{
    public GameActorType actorType;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            CheckContent();
        }
    }

    //public void OnPointerEnter(PointerEventData eventData)
    //{
    //    TooltipSystem.Show(content, header);
    //    CheckContent();
    //}

    //public void OnPointerExit(PointerEventData eventData)
    //{
    //    TooltipSystem.Hide();
    //}

    //private void OnMouseEnter()
    //{
    //    TooltipSystem.Show(content, header);
    //    CheckContent();
    //}
    //private void OnMouseExit()
    //{
    //    TooltipSystem.Hide();
    //}

    void CheckContent()
    {
        string _level;
        string _name;
        string _health;
        string _attack;
        string _attackspeed;

        switch (actorType)
        {
            case GameActorType.Tower:
                Building tower = GetComponent<Building>();
                _level = (tower.level + 1).ToString();
                _name = tower.cardData.name;
                _health = "";
                _attack = tower.cardData.attackPowers[tower.level].ToString();
                _attackspeed = tower.cardData.attackRates[tower.level].ToString();
                TooltipSystem.Show(_level, _name, _health, _attack, _attackspeed);
                break;
            case GameActorType.Generator:
                Building generator = GetComponent<Building>();
                _level = (generator.level +1).ToString();
                _name = generator.cardData.name;
                _health = "";
                _attack = "";
                _attackspeed = "";
                TooltipSystem.Show(_level, _name, _health, _attack, _attackspeed);
                break;
            case GameActorType.Utility:
                Building utility = GetComponent<Building>();
                _level = (utility.level +1).ToString();
                _name = utility.cardData.name;
                _health = "";
                _attack = "";
                _attackspeed = "";
                TooltipSystem.Show(_level, _name, _health, _attack, _attackspeed);
                break;
            case GameActorType.Minion:
                PlayerUnitAI minion = GetComponent<PlayerUnitAI>();
                _level = (minion.level +1).ToString();
                _name = minion.cardData.name;
                _health = minion.gameObject.GetComponent<Damageable>().Health.ToString();
                _attack = minion.cardData.attackPowers[minion.level].ToString();
                _attackspeed = minion.cardData.attackRates[minion.level].ToString();
                TooltipSystem.Show(_level, _name, _health, _attack, _attackspeed);
                break;
            case GameActorType.Enemy:
                EnemyUnitAI enemy = GetComponent<EnemyUnitAI>();
                _level = (enemy.startingLevel + 1).ToString();
                _name = enemy.enemy.name;
                _health = enemy.gameObject.GetComponent<Damageable>().Health.ToString();
                _attack = enemy.enemy.attackPowers[enemy.startingLevel].ToString();
                _attackspeed = enemy.enemy.attackRates[enemy.startingLevel].ToString();
                TooltipSystem.Show(_level, _name, _health, _attack, _attackspeed);
                break;
            default:
                break;
        }
    }


}
