using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SanBot.Packets
{
    public class BitWriter
    {
        public List<byte> Buffer { get; }
        public long BitOffset { get; private set; }

        public BitWriter()
        {
            this.Buffer = new List<byte>();
            this.BitOffset = 0;
        }

        public byte[] GetBytes()
        {
            return Buffer.ToArray();
        }

        public void WriteFloats(List<float> floats, int bitsPerFloat, float modifier)
        {
            var numBytes = 1 + (floats.Count * bitsPerFloat) / 8;

            var tempBuffer = new List<byte>(numBytes);
            var tempBufferBitOffset = 0L;

            foreach (var value in floats)
            {
                var mask = (ulong)Math.Pow(2, bitsPerFloat - 1) - 1;
                var result = (ulong)Math.Round(value * (mask / modifier) + mask);

                tempBufferBitOffset = WriteBits(tempBuffer, tempBufferBitOffset, result, bitsPerFloat);
            }

            this.BitOffset = WriteManyBits(this.Buffer, this.BitOffset, tempBuffer.ToArray(), floats.Count * bitsPerFloat);
        }

        public void WriteQuaternion(Quaternion quat, int bitsPerFloat)
        {
            if(quat.Values.Count != 3)
            {
                throw new ArgumentException($"{nameof(quat.Values)} must contain exactly 3 floats");
            }

            BitOffset = WriteBits(Buffer, BitOffset, quat.UnknownA, 2);
            BitOffset = WriteBits(Buffer, BitOffset, quat.UnknownB ? 1u : 0u, 1);
            BitOffset = WriteBits(Buffer, BitOffset, quat.ModifierFlag ? 1u : 0u, 1);

            float modifier;
            if (quat.ModifierFlag)
            {
                modifier = 0.1f;
            }
            else
            {
                modifier = 0.70710802f;
            }

            foreach (var value in quat.Values)
            {
                var mask = (ulong)Math.Pow(2, bitsPerFloat - 1) - 1;
                var result = (ulong)Math.Round(value * (mask / modifier) + mask);

                BitOffset = WriteBits(Buffer, BitOffset, result, 13);
            }
        }

        public void WriteFloat(float value, int numBits, float modifier = 1.0f)
        {
            var mask = (ulong)Math.Pow(2, numBits - 1) - 1;
            var result = (ulong)Math.Round(value * (mask / modifier) + mask);

            BitOffset = WriteBits(Buffer, BitOffset, result, numBits);
        }

        public void WriteUnsigned(ulong value, int numBits)
        {
            BitOffset = WriteBits(Buffer, BitOffset, value, numBits);
        }


        public static long WriteBits(List<byte> buffer, long bitOffset, ulong value, int numBits)
        {
            if (numBits > 64)
            {
                throw new Exception($"{nameof(WriteBits)} can only write up to 64 bits");
            }
            if(numBits == 0)
            {
                return bitOffset;
            }

            var numExistingBits = (int)(bitOffset % 8);
            var index = (int)(bitOffset / 8);

            var numBytesToWrite = (numExistingBits + numBits + 7) / 8;
            if((index + numBytesToWrite) >= buffer.Count)
            {
                // Resize the buffer if we need more data. Buffer should really be sized correctly before calling to avoid this
                buffer.AddRange(new byte[(index + numBytesToWrite) - buffer.Count]);
            }

            //      numBits = 6
            //        Value = 101010101011
            //  maskedValue =       101011
            var maskedValue = value & (0xFFFFFFFFFFFFFFFFul >> (64 - numBits));

            // bitsToPreserve = 3
            //  buffer[index] = 11111101
            //  existingValue =      101
            var existingValue = buffer[index] & (0xFFul >> (8 - numExistingBits));

            //  val = (101011 << 3) | (101)
            //      = 00000001_01011101
            var val = (maskedValue << numExistingBits) | existingValue;

            switch (numBytesToWrite)
            {
                case 1:
                    {
                        buffer[index + 0] = (byte)((val & 0b11111111) >> 0);
                        break;
                    }
                case 2:
                    {
                        buffer[index + 1] = (byte)((val & 0b11111111_00000000) >> 8);
                        buffer[index + 0] = (byte)((val & 0b00000000_11111111) >> 0);
                        break;
                    }
                case 3:
                    {
                        buffer[index + 2] = (byte)((val & 0b11111111_00000000_00000000) >> 16);
                        buffer[index + 1] = (byte)((val & 0b00000000_11111111_00000000) >> 8);
                        buffer[index + 0] = (byte)((val & 0b00000000_00000000_11111111) >> 0);
                        break;
                    }
                case 4:
                    {
                        buffer[index + 3] = (byte)((val & 0b11111111_00000000_00000000_00000000) >> 24);
                        buffer[index + 2] = (byte)((val & 0b00000000_11111111_00000000_00000000) >> 16);
                        buffer[index + 1] = (byte)((val & 0b00000000_00000000_11111111_00000000) >> 8);
                        buffer[index + 0] = (byte)((val & 0b00000000_00000000_00000000_11111111) >> 0);
                        break;
                    }
                case 5:
                    {
                        buffer[index + 4] = (byte)((val & 0b11111111_00000000_00000000_00000000_00000000) >> 32);
                        buffer[index + 3] = (byte)((val & 0b00000000_11111111_00000000_00000000_00000000) >> 24);
                        buffer[index + 2] = (byte)((val & 0b00000000_00000000_11111111_00000000_00000000) >> 16);
                        buffer[index + 1] = (byte)((val & 0b00000000_00000000_00000000_11111111_00000000) >> 8);
                        buffer[index + 0] = (byte)((val & 0b00000000_00000000_00000000_00000000_11111111) >> 0);
                        break;
                    }
                case 6:
                    {
                        buffer[index + 5] = (byte)((val & 0b11111111_00000000_00000000_00000000_00000000_00000000) >> 40);
                        buffer[index + 4] = (byte)((val & 0b00000000_11111111_00000000_00000000_00000000_00000000) >> 32);
                        buffer[index + 3] = (byte)((val & 0b00000000_00000000_11111111_00000000_00000000_00000000) >> 24);
                        buffer[index + 2] = (byte)((val & 0b00000000_00000000_00000000_11111111_00000000_00000000) >> 16);
                        buffer[index + 1] = (byte)((val & 0b00000000_00000000_00000000_00000000_11111111_00000000) >> 8);
                        buffer[index + 0] = (byte)((val & 0b00000000_00000000_00000000_00000000_00000000_11111111) >> 0);
                        break;
                    }
                case 7:
                    {
                        buffer[index + 6] = (byte)((val & 0b11111111_00000000_00000000_00000000_00000000_00000000_00000000) >> 48);
                        buffer[index + 5] = (byte)((val & 0b00000000_11111111_00000000_00000000_00000000_00000000_00000000) >> 40);
                        buffer[index + 4] = (byte)((val & 0b00000000_00000000_11111111_00000000_00000000_00000000_00000000) >> 32);
                        buffer[index + 3] = (byte)((val & 0b00000000_00000000_00000000_11111111_00000000_00000000_00000000) >> 24);
                        buffer[index + 2] = (byte)((val & 0b00000000_00000000_00000000_00000000_11111111_00000000_00000000) >> 16);
                        buffer[index + 1] = (byte)((val & 0b00000000_00000000_00000000_00000000_00000000_11111111_00000000) >> 8);
                        buffer[index + 0] = (byte)((val & 0b00000000_00000000_00000000_00000000_00000000_00000000_11111111) >> 0);
                        break;
                    }
                case 8:
                    {
                        buffer[index + 7] = (byte)((val & 0b11111111_00000000_00000000_00000000_00000000_00000000_00000000_00000000) >> 56);
                        buffer[index + 6] = (byte)((val & 0b00000000_11111111_00000000_00000000_00000000_00000000_00000000_00000000) >> 48);
                        buffer[index + 5] = (byte)((val & 0b00000000_00000000_11111111_00000000_00000000_00000000_00000000_00000000) >> 40);
                        buffer[index + 4] = (byte)((val & 0b00000000_00000000_00000000_11111111_00000000_00000000_00000000_00000000) >> 32);
                        buffer[index + 3] = (byte)((val & 0b00000000_00000000_00000000_00000000_11111111_00000000_00000000_00000000) >> 24);
                        buffer[index + 2] = (byte)((val & 0b00000000_00000000_00000000_00000000_00000000_11111111_00000000_00000000) >> 16);
                        buffer[index + 1] = (byte)((val & 0b00000000_00000000_00000000_00000000_00000000_00000000_11111111_00000000) >> 8);
                        buffer[index + 0] = (byte)((val & 0b00000000_00000000_00000000_00000000_00000000_00000000_00000000_11111111) >> 0);
                        break;
                    }
                case 9:
                    {
                        WriteBits(buffer, bitOffset, value, (byte)(numBits - 8));
                        WriteBits(buffer, bitOffset + numBits - 8, value >> (numBits - 8), 8);
                        break;
                    }
                default:
                    throw new Exception($"Somehow attempted to write {numBytesToWrite} bytes with {nameof(WriteBits)}");
            }

            return bitOffset + numBits;
        }

        long WriteManyBits(List<byte> outputBuffer, long totalBitOffset, byte[] input, int numBits)
        {
            var bitsRemaining = numBits;
            var inputIndex = 0L;

            if (numBits >= 32)
            {
                var numInts = numBits/32;

                for (int i = 0; i < numInts; i++)
                {
                    var value =
                        ((ulong)input[inputIndex + 3] << 24) |
                        ((ulong)input[inputIndex + 2] << 16) |
                        ((ulong)input[inputIndex + 1] << 8) |
                        ((ulong)input[inputIndex + 0] << 0);

                    totalBitOffset = WriteBits(outputBuffer, totalBitOffset, value, 32);

                    bitsRemaining -= 32;
                    inputIndex += 4;
                }
            }

            ulong finalValueToWrite = 0;
            var finalBitsToWrite = (byte)bitsRemaining;

            while(bitsRemaining >= 8)
            {
                bitsRemaining -= 8;

                finalValueToWrite |= ((ulong)input[inputIndex] << bitsRemaining);
                inputIndex++;
            }

            if (bitsRemaining > 0 )
            {
                var mask = (byte)((1UL << bitsRemaining) - 1);
                finalValueToWrite |= ((ulong)input[inputIndex] & mask);
            }

            return WriteBits(outputBuffer, totalBitOffset, finalValueToWrite, finalBitsToWrite);
        }
    }
}
