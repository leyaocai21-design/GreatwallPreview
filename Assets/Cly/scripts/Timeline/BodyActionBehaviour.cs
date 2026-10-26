using System;
using UnityEngine;
using UnityEngine.Playables;

namespace Greatwall.VRAnimation.Timeline
{
    [Serializable]
    public sealed class BodyActionBehaviour : PlayableBehaviour
    {
        public BodyActionCommand command;
        public string statePath;
        public float stateCrossFadeSeconds = 0.2f;
        public float layerFadeSeconds = 0.25f;

        private bool triggered;

        public override void OnGraphStart(Playable playable)
        {
            triggered = false;
        }

        public override void ProcessFrame(Playable playable, FrameData info, object playerData)
        {
            if (triggered || info.effectiveWeight <= 0f) return;

            CharacterTimelineAnimator controller = playerData as CharacterTimelineAnimator;
            if (controller == null)
            {
                Debug.LogWarning("Body Action Track 没有绑定 CharacterTimelineAnimator。请把角色拖到轨道绑定框中。");
                triggered = true;
                return;
            }

            if (command == BodyActionCommand.ReleaseToBaseLayer)
                controller.ReleaseBodyAction(layerFadeSeconds);
            else
                controller.PlayBodyState(
                    statePath,
                    stateCrossFadeSeconds,
                    layerFadeSeconds,
                    Mathf.Max(0f, (float)playable.GetTime()));

<<<<<<< Updated upstream
            triggered = true;
=======
                triggered = true;
            }

            // 运行时只在片段开始时指定起播位置（包含 Clip In）。
            // 后续由 Animator 的状态速度/Speed 参数推进，不能每帧用 Timeline
            // 时间强制采样，否则会覆盖移动速度同步，并打断 CrossFade。
            // 编辑模式保留采样，用于拖动 Timeline 预览。
            if (!Application.isPlaying && referenceAnimationLength > 0f)
            {
                float normalizedTime = Mathf.Repeat(localTime, referenceAnimationLength) / referenceAnimationLength;
                controller.SampleBodyState(statePath, normalizedTime);
            }
>>>>>>> Stashed changes
        }

        public override void OnBehaviourPause(Playable playable, FrameData info)
        {
            double time = playable.GetTime();
<<<<<<< Updated upstream
            if (time <= 0d || time >= playable.GetDuration())
=======
            double duration = playable.GetDuration();

            if (time <= 0d || time >= duration)
            {
                if (controller != null && command == BodyActionCommand.PlayState)
                {
                    controller.ReleaseBodyAction(layerFadeSeconds, statePath);
                }

>>>>>>> Stashed changes
                triggered = false;
        }
    }
}
