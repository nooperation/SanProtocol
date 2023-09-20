using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AgentController
{
    public class RequestAgentPlayAnimation : AgentPlayAnimation
    {
        public override uint MessageId => Messages.AgentControllerMessages.RequestAgentPlayAnimation;

        public RequestAgentPlayAnimation(uint agentControllerId, ulong frame, ulong componentId, SanUUID resourceId, float playbackSpeed, byte skeletonType, byte animationType, byte playbackMode)
            : base(
                agentControllerId,
                frame,
                componentId,
                resourceId,
                playbackSpeed,
                skeletonType,
                animationType,
                playbackMode
              )
        {
           
        }

        public RequestAgentPlayAnimation(BinaryReader br)
            : base(br)
        {
          
        }

        public override string ToString()
        {
            return $"AgentController::RequestAgentPlayAnimation:\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(ResourceId)} = {ResourceId}\n" +
                   $"  {nameof(PlaybackSpeed)} = {PlaybackSpeed}\n" +
                   $"  {nameof(SkeletonType)} = {SkeletonType}\n" +
                   $"  {nameof(AnimationType)} = {AnimationType}\n" +
                   $"  {nameof(PlaybackMode)} = {PlaybackMode}\n";
        }
    }
}
