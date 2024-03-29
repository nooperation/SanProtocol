﻿namespace SanProtocol.AgentController
{
    public class CharacterControlPointInput : IPacket
    {
        public virtual uint MessageId => Messages.AgentControllerMessages.CharacterControlPointInput;

        public ulong Frame { get; set; }
        public uint AgentControllerId { get; set; }
        public uint ControlPointsLength { get; set; }
        public List<AgentController.ControlPoint> ControlPoints { get; set; }
        public float LeftIndexTrigger { get; set; }
        public float RightIndexTrigger { get; set; }
        public float LeftGripTrigger { get; set; }
        public float RightGripTrigger { get; set; }
        public byte LeftTouches { get; set; }
        public byte RightTouches { get; set; }
        public bool IndexTriggerControlsHand { get; set; }
        public bool LeftHandIsHolding { get; set; }
        public bool RightHandIsHolding { get; set; }

        public CharacterControlPointInput(
            ulong frame,
            uint agentControllerId,
            List<AgentController.ControlPoint> controlPoints,
            float leftIndexTrigger,
            float rightIndexTrigger,
            float leftGripTrigger,
            float rightGripTrigger,
            byte leftTouches,
            byte rightTouches,
            bool indexTriggerControlsHand,
            bool leftHandIsHolding,
            bool rightHandIsHolding)
        {
            Frame = frame;
            AgentControllerId = agentControllerId;
            ControlPointsLength = (uint)controlPoints.Count;
            ControlPoints = controlPoints;
            LeftIndexTrigger = leftIndexTrigger;
            RightIndexTrigger = rightIndexTrigger;
            LeftGripTrigger = leftGripTrigger;
            RightGripTrigger = rightGripTrigger;
            LeftTouches = leftTouches;
            RightTouches = rightTouches;
            IndexTriggerControlsHand = indexTriggerControlsHand;
            LeftHandIsHolding = leftHandIsHolding;
            RightHandIsHolding = rightHandIsHolding;
        }

        public CharacterControlPointInput(BinaryReader br)
        {
            Frame = br.ReadUInt64();
            AgentControllerId = br.ReadUInt32();
            ControlPointsLength = br.ReadUInt32();

            ControlPoints = new List<ControlPoint>();

            var bitReader = new BitReader(br);
            for (var i = 0; i < ControlPointsLength; i++)
            {
                var newControlPoint = new AgentController.ControlPoint(bitReader);
                ControlPoints.Add(newControlPoint);
            }

            LeftIndexTrigger = bitReader.ReadFloat(8, 1.0f);
            RightIndexTrigger = bitReader.ReadFloat(8, 1.0f);
            LeftGripTrigger = bitReader.ReadFloat(8, 1.0f);
            RightGripTrigger = bitReader.ReadFloat(8, 1.0f);
            LeftTouches = (byte)bitReader.ReadUnsigned(7);
            RightTouches = (byte)bitReader.ReadUnsigned(7);
            IndexTriggerControlsHand = bitReader.ReadUnsigned(1) != 0;
            LeftHandIsHolding = bitReader.ReadUnsigned(1) != 0;
            RightHandIsHolding = bitReader.ReadUnsigned(1) != 0;
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(Frame);
                    bw.Write(AgentControllerId);
                    bw.Write(ControlPointsLength);

                    var bitWriter = new BitWriter();
                    foreach (var controlPoint in ControlPoints)
                    {
                        controlPoint.WriteBits(bitWriter);
                    }

                    bitWriter.WriteFloat(LeftIndexTrigger, 8, 1.0f);
                    bitWriter.WriteFloat(RightIndexTrigger, 8, 1.0f);
                    bitWriter.WriteFloat(LeftGripTrigger, 8, 1.0f);
                    bitWriter.WriteFloat(RightGripTrigger, 8, 1.0f);
                    bitWriter.WriteUnsigned(LeftTouches, 7);
                    bitWriter.WriteUnsigned(RightTouches, 7);
                    bitWriter.WriteUnsigned(IndexTriggerControlsHand ? 1u : 0u, 1);
                    bitWriter.WriteUnsigned(LeftHandIsHolding ? 1u : 0u, 1);
                    bitWriter.WriteUnsigned(RightHandIsHolding ? 1u : 0u, 1);

                    var bits = bitWriter.GetBytes();
                    bw.Write(bits);
                }

                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"AgentController::CharacterControlPointInput:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(AgentControllerId)} = {AgentControllerId}\n" +
                   $"  {nameof(ControlPointsLength)} = {ControlPointsLength}\n" +
                   $"  {nameof(LeftIndexTrigger)} = {LeftIndexTrigger}\n" +
                   $"  {nameof(RightIndexTrigger)} = {RightIndexTrigger}\n" +
                   $"  {nameof(LeftGripTrigger)} = {LeftGripTrigger}\n" +
                   $"  {nameof(RightGripTrigger)} = {RightGripTrigger}\n" +
                   $"  {nameof(LeftTouches)} = {LeftTouches}\n" +
                   $"  {nameof(RightTouches)} = {RightTouches}\n" +
                   $"  {nameof(IndexTriggerControlsHand)} = {IndexTriggerControlsHand}\n" +
                   $"  {nameof(LeftHandIsHolding)} = {LeftHandIsHolding}\n" +
                   $"  {nameof(RightHandIsHolding)} = {RightHandIsHolding}\n";
        }
    }

}
