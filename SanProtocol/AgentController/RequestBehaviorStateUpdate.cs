using SanProtocol.AnimationComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanProtocol.AgentController
{
    public class RequestBehaviorStateUpdate : BehaviorStateUpdate
    {
        public RequestBehaviorStateUpdate(
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
            : base(
                  frame,
                  componentId,
                  exceptAgentControllerId,
                  floats,
                  vectors,
                  quaternions,
                  int8s,
                  bools,
                  internalEventIds,
                  animationAction,
                  nodeLocalTimes,
                  nodeCropValues
            )
        {
        }

        public RequestBehaviorStateUpdate(BinaryReader br)
            : base(br)
        {
        }

        public override string ToString()
        {
            return $"AgentController::RequestBehaviorStateUpdate:\n" +
                   $"  {nameof(Frame)} = {Frame}\n" +
                   $"  {nameof(ComponentId)} = {ComponentId}\n" +
                   $"  {nameof(ExceptAgentControllerId)} = {ExceptAgentControllerId}\n" +
                   $"  {nameof(Floats)} = {Floats}\n" +
                   $"  {nameof(Vectors)} = {Vectors}\n" +
                   $"  {nameof(Quaternions)} = {Quaternions}\n" +
                   $"  {nameof(Int8s)} = {Int8s}\n" +
                   $"  {nameof(Bools)} = {Bools}\n" +
                   $"  {nameof(InternalEventIds)} = {String.Join(',', InternalEventIds)}\n" +
                   $"  {nameof(AnimationAction)} = {AnimationAction}\n" +
                   $"  {nameof(NodeLocalTimes)} = {String.Join(',', NodeLocalTimes)}\n" +
                   $"  {nameof(NodeCropValues)} = {String.Join(',', NodeCropValues)}\n";
        }
    }
}
