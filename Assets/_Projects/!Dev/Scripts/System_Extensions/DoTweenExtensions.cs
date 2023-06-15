using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using UnityEngine;

namespace nyy.FG_Case.Extensions
{
    public static class DoTweenExtensions 
    {
        public static TweenerCore<Vector3, Vector3, VectorOptions> DOFollow(this Transform transform, Transform target, Vector3 endPosition, float duration)
        {
            var t = DOTween.To(
                () => transform.position - target.transform.position,x => transform.position = x + target.transform.position, endPosition, 
                duration);
            
            t.SetTarget(transform);
            
            return t;
        }
    }
}