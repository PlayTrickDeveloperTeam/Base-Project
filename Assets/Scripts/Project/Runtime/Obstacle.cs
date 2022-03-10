using UnityEngine;

public abstract class Obstacle : Contactable
{
    public override void WhenContact(GameObject _obj)
    {
        _obj.GetComponent<Player>().ChangeGold(-value);
    }

    public override void Effect(GameObject _obj)
    {
        //Null
    }

    public override void WhenExit(GameObject _obj)
    {
        //Null
    }
}
