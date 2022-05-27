using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanProtocol.Render
{
    public class LightStateChanged : IPacket
    {
        public uint MessageId => Messages.Render.LightStateChanged;

        public ulong ComponentId { get; set; }
        public ulong Frame { get; set; }
        public List<float> Rgb { get; set; } = new List<float>();
        public float Range { get; set; }
        public uint ShadowPriority { get; set; }
        public float SpotSinHalfAngle { get; set; }
        public float SpotAngularFalloff { get; set; }
        public float SpotNearClip { get; set; }

        public LightStateChanged(ulong componentId, ulong frame, List<float> rgb, float range, uint shadowPriority, float spotSinHalfAngle, float spotAngularFalloff, float spotNearClip)
        {
            this.ComponentId = componentId;
            this.Frame = frame;
            this.Rgb = rgb;
            this.Range = range;
            this.ShadowPriority = shadowPriority;
            this.SpotSinHalfAngle = spotSinHalfAngle;
            this.SpotAngularFalloff = spotAngularFalloff;
            this.SpotNearClip = spotNearClip;
        }

        public LightStateChanged(BinaryReader br)
        {
            ComponentId = br.ReadUInt64();
            Frame = br.ReadUInt64();
            for (var i = 0; i < 3; ++i)
            {
                var item = br.ReadSingle();
                Rgb.Add(item);
            }
            Range = br.ReadSingle();
            ShadowPriority = br.ReadUInt32();
            SpotSinHalfAngle = br.ReadSingle();
            SpotAngularFalloff = br.ReadSingle();
            SpotNearClip = br.ReadSingle();
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(ComponentId);
                    bw.Write(Frame);
                    foreach (var item in Rgb)
                    {
                        bw.Write(item);
                    }
                    bw.Write(Range);
                    bw.Write(ShadowPriority);
                    bw.Write(SpotSinHalfAngle);
                    bw.Write(SpotAngularFalloff);
                    bw.Write(SpotNearClip);
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"Render::LightStateChanged:\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(Rgb)} = <{String.Join(',', Rgb)}>\n" +
                   $"  {nameof(Range)} = {Range}\n" +
                   $"  {nameof(ShadowPriority)} = {ShadowPriority}\n" +
                   $"  {nameof(SpotSinHalfAngle)} = {SpotSinHalfAngle}\n" +
                   $"  {nameof(SpotAngularFalloff)} = {SpotAngularFalloff}\n" +
                   $"  {nameof(SpotNearClip)} = {SpotNearClip}\n";
        }
    }
}
