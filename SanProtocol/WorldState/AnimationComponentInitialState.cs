using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.WorldState
{
    public class AnimationComponentInitialState : IPacket
    {
        public uint MessageId => Messages.WorldStateMessages.AnimationComponentInitialState;

        public uint RelativeComponentId { get; set; }
        public List<float> Velocity { get; set; } = new List<float>();
        public byte[] BehaviorState { get; set; }

        public AnimationComponentInitialState(uint relativeComponentId, List<float> velocity, byte[] behaviorState)
        {
            this.RelativeComponentId = relativeComponentId;
            this.Velocity = velocity;
            this.BehaviorState = behaviorState;
        }

        public AnimationComponentInitialState(BinaryReader br)
        {
            RelativeComponentId = br.ReadUInt32();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Velocity.Add(item);
            }
            var behaviorStateLength = br.ReadInt32();
            BehaviorState = br.ReadBytes(behaviorStateLength);
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(RelativeComponentId);
                    foreach (var item in Velocity)
                    {
                        bw.Write(item);
                    }
                    bw.Write(BehaviorState.Length);
                    bw.Write(BehaviorState);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::AnimationComponentInitialState:\n" +
                   $"  {nameof(RelativeComponentId)} = {RelativeComponentId}\n" +
                   $"  {nameof(Velocity)} = <{String.Join(',', Velocity)}>\n" +
                   $"  {nameof(BehaviorState)} = {BehaviorState}\n";
        }
    }
}
