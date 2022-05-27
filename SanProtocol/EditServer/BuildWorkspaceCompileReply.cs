﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace SanBot.Packets.EditServer
{
    public class BuildWorkspaceCompileReply : IPacket
    {
        public uint MessageId => Messages.EditServer.BuildWorkspaceCompileReply;

        public byte CompileStatus { get; set; }
        public byte IsCanceled { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> NonErrorMessage { get; set; } = new List<string>();

        public BuildWorkspaceCompileReply(byte compileStatus, byte isCanceled, List<string> errors, List<string> nonErrorMessage)
        {
            this.CompileStatus = compileStatus;
            this.IsCanceled = isCanceled;
            this.Errors = errors;
            this.NonErrorMessage = nonErrorMessage;
        }

        public BuildWorkspaceCompileReply(BinaryReader br)
        {
            CompileStatus = br.ReadByte();
            IsCanceled = br.ReadByte();
            var numErrors = br.ReadUInt32();
            for (var i = 0; i < numErrors; ++i)
            {
                var str = br.ReadSanString();
                Errors.Add(str);
            }
            var numNonErrorMessage = br.ReadUInt32();
            for (var i = 0; i < numNonErrorMessage; ++i)
            {
                var str = br.ReadSanString();
                NonErrorMessage.Add(str);
            }
        }

        public byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                using (var bw = new BinaryWriter(ms))
                {
                    bw.Write(MessageId);
                    bw.Write(CompileStatus);
                    bw.Write(IsCanceled);
                    bw.Write(Errors.Count);
                    foreach (var item in Errors)
                    {
                        bw.WriteSanString(item);
                    }
                    bw.Write(NonErrorMessage.Count);
                    foreach (var item in NonErrorMessage)
                    {
                        bw.WriteSanString(item);
                    }
                }
                return ms.ToArray();
            }
        }

        public override string ToString()
        {
            return $"EditServer::BuildWorkspaceCompileReply:\n" +
                   $"  {nameof(CompileStatus)} = {CompileStatus}\n" +
                   $"  {nameof(IsCanceled)} = {IsCanceled}\n" +
                   $"  {nameof(Errors)} = {String.Join(',', Errors)}\n" +
                   $"  {nameof(NonErrorMessage)} = {String.Join(',', NonErrorMessage)}\n";
        }
    }
}