using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class BehaviorStateUpdate : IPacket
    {
        public uint MessageId => Messages.AnimationComponentMessages.BehaviorStateUpdate;

        public ulong Frame { get; set; }
        public ulong ComponentId { get; set; }
        public uint ExceptAgentControllerId { get; set; }
        public List<FloatVariable> Floats { get; set; } = new List<FloatVariable>();
        public List<VectorVariable> Vectors { get; set; } = new List<VectorVariable>();
        public List<QuaternionVariable> Quaternions { get; set; } = new List<QuaternionVariable>();
        public List<Int8Variable> Int8s { get; set; } = new List<Int8Variable>();
        public List<BoolVariable> Bools { get; set; } = new List<BoolVariable>();
        public List<ushort> InternalEventIds { get; set; } = new List<ushort>();
        public byte AnimationAction { get; set; }
        public List<FloatNodeVariable> NodeLocalTimes { get; set; } = new List<FloatNodeVariable>();
        public List<FloatRangeNodeVariable> NodeCropValues { get; set; } = new List<FloatRangeNodeVariable>();

        public BehaviorStateUpdate(
            ulong frame,
            ulong componentId,
            uint exceptAgentControllerId,
            List<FloatVariable> floats,
            List<VectorVariable> vectors,
            List<QuaternionVariable> quaternions,
            List<Int8Variable> int8s,
            List<BoolVariable> bools,
            List<ushort> internalEventIds,
            byte animationAction,
            List<FloatNodeVariable> nodeLocalTimes,
            List<FloatRangeNodeVariable> nodeCropValues
            )
        {
            this.Frame = frame;
            this.ComponentId = componentId;
            this.ExceptAgentControllerId = exceptAgentControllerId;
            this.Floats = floats;
            this.Vectors = vectors;
            this.Quaternions = quaternions;
            this.Int8s = int8s;
            this.Bools = bools;
            this.InternalEventIds = internalEventIds;
            this.AnimationAction = animationAction;
            this.NodeLocalTimes = nodeLocalTimes;
            this.NodeCropValues = nodeCropValues;
        }

        public BehaviorStateUpdate(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            ComponentId = br.ReadUInt64();
            ExceptAgentControllerId = br.ReadUInt32();

            var floatsLength = br.ReadInt32();
            for (int i = 0; i < floatsLength; i++)
            {
                Floats.Add(new FloatVariable(br));
            }

            var vectorsLength = br.ReadInt32();
            for (int i = 0; i < vectorsLength; i++)
            {
                Vectors.Add(new VectorVariable(br));
            }

            var quaternionsLength = br.ReadInt32();
            for (int i = 0; i < quaternionsLength; i++)
            {
                Quaternions.Add(new QuaternionVariable(br));
            }

            var int8sLength = br.ReadInt32();
            for (int i = 0; i < int8sLength; i++)
            {
                Int8s.Add(new Int8Variable(br));
            }

            var boolsLength = br.ReadInt32();
            for (int i = 0; i < boolsLength; i++)
            {
                Bools.Add(new BoolVariable(br));
            }

            var internalEventIdsLength = br.ReadInt32();
            for (int i = 0; i < internalEventIdsLength; i++)
            {
                InternalEventIds.Add(br.ReadUInt16());
            }

            AnimationAction = br.ReadByte();

            var nodeLocalTimesLength = br.ReadInt32();
            for (int i = 0; i < nodeLocalTimesLength; i++)
            {
                NodeLocalTimes.Add(new FloatNodeVariable(br));
            }

            var nodeCropValuesLength = br.ReadInt32();
            for (int i = 0; i < nodeCropValuesLength; i++)
            {
                NodeCropValues.Add(new FloatRangeNodeVariable(br));
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(ComponentId);
                    bw.Write(ExceptAgentControllerId);

                    bw.Write(Floats.Count);
                    foreach (var item in Floats)
                    {
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(Vectors.Count);
                    foreach (var item in Vectors)
                    {
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(Quaternions.Count);
                    foreach (var item in Quaternions)
                    {
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(Int8s.Count);
                    foreach (var item in Int8s)
                    {
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(Bools.Count);
                    foreach (var item in Bools)
                    {
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(InternalEventIds.Count);
                    foreach (var item in InternalEventIds)
                    {
                        bw.Write(item);
                    }

                    bw.Write(AnimationAction);

                    bw.Write(NodeLocalTimes.Count);
                    foreach (var item in NodeLocalTimes)
                    {
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }

                    bw.Write(NodeCropValues.Count);
                    foreach (var item in NodeCropValues)
                    {
                        var bytes = item.GetBytes().Skip(4).ToArray();
                        bw.Write(bytes);
                    }
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AnimationComponent::BehaviorStateUpdate:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(ExceptAgentControllerId)} = {ExceptAgentControllerId}\n" +
                   $"  {nameof(Floats)} = {string.Join(", ", Floats)}\n" +
                   $"  {nameof(Vectors)} = {string.Join(", ", Vectors)}\n" +
                   $"  {nameof(Quaternions)} = {string.Join(", ", Quaternions)}\n" +
                   $"  {nameof(Int8s)} = {string.Join(", ", Int8s)}\n" +
                   $"  {nameof(Bools)} = {string.Join(", ", Bools)}\n" +
                   $"  {nameof(InternalEventIds)} = {String.Join(',', InternalEventIds)}\n" +
                   $"  {nameof(AnimationAction)} = {AnimationAction}\n" +
                   $"  {nameof(NodeLocalTimes)} = {String.Join(',', NodeLocalTimes)}\n" +
                   $"  {nameof(NodeCropValues)} = {String.Join(',', NodeCropValues)}\n";
        }
    }

}
