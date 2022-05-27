using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.AnimationComponent
{
    public class CharacterBehaviorInternalState : BehaviorInternalState
    {
        public override uint MessageId => Messages.AnimationComponent.CharacterBehaviorInternalState;

        public CharacterBehaviorInternalState(ulong componentId, ulong frame, List<AnimationOverride> overrides, byte[] slotStates, byte[] stateData, byte isPlaying)
            : base(componentId, frame, overrides, slotStates, stateData, isPlaying)
        {
        }

        public CharacterBehaviorInternalState(BinaryReader br)
            :base(br)
        {
        }

        public override string ToString()
        {
            return $"AnimationComponent::CharacterBehaviorInternalState:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(SlotStates)} = {SlotStates}\n" +
                   $"  {nameof(StateData)} = {StateData}\n" +
                   $"  {nameof(IsPlaying)} = {IsPlaying}\n";
        }
    }
}
