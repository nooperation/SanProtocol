using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SanProtocol.WorldState
{
    public class InitiateCluster : IPacket
    {
        public uint MessageId => Messages.WorldState.InitiateCluster;

        public uint ClusterId { get; set; }
        public ulong Frame { get; set; }
        public List<RigidBodyComponentInitialState> RigidBodyInitialStates { get; set; } = new List<RigidBodyComponentInitialState>();
        public List<AnimationComponentInitialState> AnimationInitialStates { get; set; } = new List<AnimationComponentInitialState>();

        public InitiateCluster(uint clusterId, ulong frame, List<RigidBodyComponentInitialState> rigidBodyInitialStates, List<AnimationComponentInitialState> animationInitialStates)
        {
            this.ClusterId = clusterId;
            this.Frame = frame;
            this.RigidBodyInitialStates = rigidBodyInitialStates;
            this.AnimationInitialStates = animationInitialStates;
        }

        public InitiateCluster(BinaryReader br)
        {
            ClusterId = br.ReadUInt32();
            Frame = br.ReadUInt64();

            var rigidBodyInitialStatesLength = br.ReadUInt32();
            RigidBodyInitialStates = new List<RigidBodyComponentInitialState>();
            for (int i = 0; i < rigidBodyInitialStatesLength; i++)
            {
                RigidBodyInitialStates.Add(new RigidBodyComponentInitialState(br));
            }

            var animationInitialStatesLength = br.ReadUInt32();
            AnimationInitialStates = new List<AnimationComponentInitialState>();
            for (int i = 0; i < animationInitialStatesLength; i++)
            {
                AnimationInitialStates.Add(new AnimationComponentInitialState(br));
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ClusterId);
                    bw.Write(Frame);
                    bw.Write(RigidBodyInitialStates.Count);
                    foreach (var item in RigidBodyInitialStates)
                    {
                        // Skip the messageId (the first 4 bytes) of GetBytes()...
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(AnimationInitialStates.Count);
                    foreach (var item in AnimationInitialStates)
                    {
                        // Skip the messageId (the first 4 bytes) of GetBytes()...
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"WorldState::InitiateCluster:\n" +
                   $"  {nameof(ClusterId)} = {ClusterId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(RigidBodyInitialStates)} = {string.Join(",", RigidBodyInitialStates)}\n" +
                   $"  {nameof(AnimationInitialStates)} = {string.Join(",", AnimationInitialStates)}\n";
        }
    }

}
