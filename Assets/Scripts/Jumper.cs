using System.Collections;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    private readonly float _timeJump = 1f;
    private readonly float _heightJump = 11f;
    private readonly float _lengthJump = 22;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            StartCoroutine(JumpCoroutine(player.transform));
        }
    }

    private IEnumerator JumpCoroutine(Transform player)
    {
        float currentTime = 0;
        Vector3 target = player.position;
        target.z += _lengthJump;

        AnimationCurve roadY = AnimationCurve.EaseInOut(0, player.position.y, _timeJump, target.y);
        AnimationCurve roadZ = AnimationCurve.EaseInOut(0, player.position.z, _timeJump, target.z);

        roadY.AddKey(_timeJump / 2, _heightJump);

        //for (currentTime = 0; currentTime <= _timeJump; currentTime += Time.fixedDeltaTime)
        //{
        //    player.position = new Vector3(player.position.x,
        //        roadY.Evaluate(currentTime), roadZ.Evaluate(currentTime));
        //    yield return null;
        //}

        while (true)
        {
            currentTime += Time.fixedDeltaTime;
            player.position = new Vector3(player.position.x, roadY.Evaluate(currentTime), roadZ.Evaluate(currentTime));
            target.x = player.position.x;
            if (Vector3.Distance(player.position, target) < 0.01f)
                break;

            yield return null;
        }

        target.x = player.position.x;
        player.position = target;
        yield break;
    }
}
