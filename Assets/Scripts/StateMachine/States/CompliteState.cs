using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace ModularBridgeSystem
{
    public class CompliteState : StateBridgeSystem
    {
        public override void Enter()
        {
            Bridge bridge = stateMachine.Controller.Bridge;

            foreach (var element in bridge.GetAllBlocks())
            {
                element.Complite();
                element.SetLayer(config.LayerBlockCompliteState);
            }

            CompliteAnimation();
        }


        public virtual void CompliteAnimation()
        {
            Vector3 startScale = bridge.Root.localScale;
            Sequence sequence = DOTween.Sequence();

            sequence
                .AppendCallback(() => bridge.Root.localScale = new Vector3(startScale.x, 0, startScale.z))
                .Append(bridge.Root.DOScale(startScale, config.CompliteAnimationDuration).SetEase(config.CompliteAnimationEase))
                .OnComplete(OnCompliteAnimation);
        }

        public virtual void OnCompliteAnimation()
        {

            stateMachine.Controller.SetBridge(null);
            stateMachine.ChangeState(typeof(ReadyState));
        }







    }
}
