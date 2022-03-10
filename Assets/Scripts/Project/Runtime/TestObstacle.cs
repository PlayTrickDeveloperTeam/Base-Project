using DG.Tweening;
using UnityEngine;

public class TestObstacle : Obstacle
{
    public override void Effect(GameObject _obj)
    {
        base.Effect(_obj);
        _obj.transform.DOShakePosition(0.5f, 0.5f, 10, 90, false, true);
    }
}
