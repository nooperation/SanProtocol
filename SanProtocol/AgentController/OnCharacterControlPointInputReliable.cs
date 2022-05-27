using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AgentController
{
    public class CharacterControlPointInputReliable : CharacterControlPointInput
    {
        public override uint MessageId => Messages.AgentController.CharacterControlPointInputReliable;
        
        public CharacterControlPointInputReliable(ulong frame, uint agentControllerId, uint controlPointsLength, byte leftIndexTrigger, byte rightIndexTrigger, byte leftGripTrigger, byte rightGripTrigger, byte leftTouches, byte rightTouches, bool indexTriggerControlsHand, bool leftHandIsHolding, bool rightHandIsHolding)
        : base(
                frame,
                agentControllerId,
                controlPointsLength,
                leftIndexTrigger,
                rightIndexTrigger,
                leftGripTrigger,
                rightGripTrigger,
                leftTouches,
                rightTouches,
                indexTriggerControlsHand,
                leftHandIsHolding,
                rightHandIsHolding
            )
        {
        }

        public CharacterControlPointInputReliable(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return $"AgentController::CharacterControlPointInputReliable:\n" +
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
