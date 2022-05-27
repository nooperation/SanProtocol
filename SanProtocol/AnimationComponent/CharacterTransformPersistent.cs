﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.AnimationComponent
{
    public class CharacterTransformPersistent : CharacterTransform
    {
        public override uint MessageId => Messages.AnimationComponent.CharacterTransformPersistent;

        public CharacterTransformPersistent(ulong componentId, ulong serverFrame, ulong groundComponentId, List<float> position, Quaternion orientationQuat)
            : base(componentId, serverFrame, groundComponentId, position, orientationQuat)
        {
        }

        public CharacterTransformPersistent(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return $"AnimationComponent::CharacterTransformPersistent:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(ServerFrame)} = {ServerFrame}\n" +
                   $"  {nameof(GroundComponentId)} = {GroundComponentId}\n" +
                   $"  {nameof(Position)} = <{String.Join(',', Position)}>\n" +
                   $"  {nameof(OrientationQuat)} = <{String.Join(',', OrientationQuat)}>\n";
        }
    }

}
