using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class BehaviorInitializationData : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.BehaviorInitializationData;

        public BehaviorInternalState BehaviorInternalState { get; set; }
        public List<BehaviorStateUpdate> BehaviorStateUpdates { get; set; }
        public List<PlayAnimation> AnimationUpdates { get; set; }

        public BehaviorInitializationData(List<BehaviorStateUpdate> behaviorStateUpdates, List<PlayAnimation> animationUpdates, BehaviorInternalState internalState)
        {
            this.BehaviorInternalState = internalState;
            this.BehaviorStateUpdates = behaviorStateUpdates;
            this.AnimationUpdates = animationUpdates;
        }

        public BehaviorInitializationData(BinaryReader br)
        {
            BehaviorInternalState = new BehaviorInternalState(br);


            var behaviorStateUpdatesLength = br.ReadInt32();
            BehaviorStateUpdates = new List<BehaviorStateUpdate>(behaviorStateUpdatesLength);
            for (int i = 0; i < behaviorStateUpdatesLength; i++)
            {
                var update = new BehaviorStateUpdate(br);
                BehaviorStateUpdates.Add(update);
            }

            var animationUpdatesLength = br.ReadInt32();
            AnimationUpdates = new List<PlayAnimation>(animationUpdatesLength);
            for (int i = 0; i < animationUpdatesLength; i++)
            {
                var update = new PlayAnimation(br);
                AnimationUpdates.Add(update);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(BehaviorInternalState.GetBytes().Skip(4).ToArray());
                    bw.Write(BehaviorStateUpdates.Count);
                    foreach (var item in BehaviorStateUpdates)
                    {
                        bw.Write(item.GetBytes().Skip(4).ToArray());
                    }

                    bw.Write(AnimationUpdates.Count);
                    foreach (var item in AnimationUpdates)
                    {
                        bw.Write(item.GetBytes().Skip(4).ToArray());
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::BehaviorInitializationData:\n" +
                   $"  {nameof(BehaviorInternalState)} = {BehaviorInternalState.ToString()}\n" +
                   $"  {nameof(BehaviorStateUpdates)} = [{BehaviorStateUpdates.Count}]\n" +
                   $"  {nameof(AnimationUpdates)} = [{AnimationUpdates.Count}]\n";
        }
    }
}
