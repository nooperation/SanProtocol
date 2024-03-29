﻿namespace SanProtocol
{
    public class BitReader
    {
        public byte[] Buffer { get; }
        public long BitOffset { get; private set; }

        public BitReader(byte[] buffer)
        {
            Buffer = buffer;
            BitOffset = 0;
        }

        public BitReader(BinaryReader br, long totalBits)
        {
            var bytesToRead = (int)(totalBits / 8);
            if ((bytesToRead * 8) < totalBits)
            {
                bytesToRead++;
            }

            // ReadBits tends to go way out of bounds. Just give ourselves enough room to play with.
            Buffer = new byte[bytesToRead + 8];
            br.Read(Buffer, 0, bytesToRead);
            BitOffset = 0;
        }

        public BitReader(BinaryReader br)
        {
            var bytesToRead = br.BaseStream.Length - br.BaseStream.Position;

            // ReadBits tends to go way out of bounds. Just give ourselves enough room to play with.
            Buffer = new byte[bytesToRead + 8];
            br.Read(Buffer, 0, (int)bytesToRead);
            BitOffset = 0;
        }

        public ulong ReadUnsigned(byte numBits)
        {
            var mask = 0xFFFFFFFFFFFFFFFFul >> (64 - numBits);
            BitOffset = ReadBits(Buffer, BitOffset, out var output, numBits);

            return output & mask;
        }

        public float ReadFloat(byte bits, float modifier = 1.0f)
        {
            var floats = ReadFloats(1, bits, modifier);
            return floats[0];
        }

        private byte[] _cleanBytes = new byte[256];
        public List<float> ReadFloats(int numFloats, int bitsPerFloat, float modifier = 1.0f)
        {
            if (numFloats < 0)
            {
                throw new ArgumentException(nameof(numFloats));
            }

            var bufferSize = 1 + (numFloats * bitsPerFloat / 8);
            if (bufferSize > _cleanBytes.Length)
            {
                _cleanBytes = new byte[bufferSize];
            }

            BitOffset = ReadManyBits(Buffer, BitOffset, _cleanBytes, 0, numFloats * bitsPerFloat);

            var mask = 0xFFFFFFFFFFFFFFFFul >> (64 - (bitsPerFloat - 1));
            var valueModifier = 1.0f / mask * modifier;

            var results = new List<float>(numFloats);
            var bitOffset = 0L;
            for (var i = 0; i < numFloats; i++)
            {
                bitOffset = BitReader.ReadBits(_cleanBytes, bitOffset, out var floatData, bitsPerFloat);

                var value = ((long)floatData - (long)mask) * valueModifier;

                results.Add(value);
            }

            return results;
        }

        public Quaternion ReadQuaternion(int numFloats, int bitsPerFloat)
        {
            BitOffset = ReadBits(Buffer, BitOffset, out var unknownA, 2);
            BitOffset = ReadBits(Buffer, BitOffset, out var unknownB, 1);
            BitOffset = ReadBits(Buffer, BitOffset, out var modifierFlag, 1);

            float modifier;
            if (modifierFlag == 1)
            {
                modifier = 0.1f;
            }
            else
            {
                modifier = 0.70710802f;
            }

            var mask = 0xFFFFFFFFFFFFFFFFul >> (64 - (bitsPerFloat - 1));
            var valueModifier = 1.0f / mask * modifier;

            var results = new List<float>(numFloats);
            for (var i = 0; i < numFloats; i++)
            {
                BitOffset = ReadBits(Buffer, BitOffset, out var floatData, bitsPerFloat);

                var value = ((long)floatData - (long)mask) * valueModifier;

                results.Add(value);
            }

            return new Quaternion()
            {
                UnknownA = (byte)unknownA,
                UnknownB = unknownB != 0,
                ModifierFlag = modifierFlag != 0,
                Values = results
            };
        }

        public static long ReadManyBits(byte[] input, long totalBitOffset, byte[] output, int outputIndex, int totalBits)
        {
            var bitsRemaining = totalBits;

            if (totalBits >= 32)
            {
                bitsRemaining = totalBits % 32;
                var intsRemaining = totalBits / 32;

                for (var i = 0u; i < intsRemaining; i++)
                {
                    ReadBits(input, totalBitOffset, out var result, 32);

                    output[outputIndex + 3] = (byte)((result & 0b11111111_00000000_00000000_00000000) >> 24);
                    output[outputIndex + 2] = (byte)((result & 0b00000000_11111111_00000000_00000000) >> 16);
                    output[outputIndex + 1] = (byte)((result & 0b00000000_00000000_11111111_00000000) >> 8);
                    output[outputIndex + 0] = (byte)((result & 0b00000000_00000000_00000000_11111111) >> 0);

                    totalBitOffset += 32;
                    outputIndex += 4;
                }
            }

            ReadBits(input, totalBitOffset, out var outRemaining, (byte)bitsRemaining);

            while (bitsRemaining >= 8)
            {
                bitsRemaining -= 8;
                totalBitOffset += 8;

                output[outputIndex] = (byte)(outRemaining >> bitsRemaining);
                ++outputIndex;
            }

            if (bitsRemaining > 0)
            {
                var mask = (1ul << bitsRemaining) - 1;

                output[outputIndex] = (byte)(outRemaining & mask);
            }

            return bitsRemaining + totalBitOffset;
        }

        public static long ReadBits(byte[] input, long bitOffset, out ulong output, int numBits)
        {
            if (numBits > 64)
            {
                throw new Exception($"{nameof(ReadBits)} can only read up to 64 bits");
            }
            if (numBits < 0)
            {
                throw new ArgumentException(nameof(numBits));
            }
            if (numBits == 0)
            {
                output = 0;
                return bitOffset;
            }

            var numExistingBits = (int)(bitOffset % 8);
            var index = bitOffset / 8;

            var readMask = 0xFFFFFFFFFFFFFFFFul >> (64 - numBits);

            var numBytesToRead = (numExistingBits + numBits + 7) / 8;
            switch (numBytesToRead)
            {
                case 1:
                    {
                        var val =
                            (ulong)input[index + 0];

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 2:
                    {
                        var val =
                            ((ulong)input[index + 1] << 8) |
                            ((ulong)input[index + 0] << 0);

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 3:
                    {
                        var val =
                            ((ulong)input[index + 2] << 16) |
                            ((ulong)input[index + 1] << 8) |
                            ((ulong)input[index + 0] << 0);

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 4:
                    {
                        var val =
                            ((ulong)input[index + 3] << 24) |
                            ((ulong)input[index + 2] << 16) |
                            ((ulong)input[index + 1] << 8) |
                            ((ulong)input[index + 0] << 0);

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 5:
                    {
                        var val =
                            ((ulong)input[index + 4] << 32) |
                            ((ulong)input[index + 3] << 24) |
                            ((ulong)input[index + 2] << 16) |
                            ((ulong)input[index + 1] << 8) |
                            ((ulong)input[index + 0] << 0);

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 6:
                    {
                        // TODO: This *really* looks incorrect, but this seems to be how sansar does it. I'm not sure if we ever execute it though
                        var val =
                            ((ulong)input[index + 1] << 40) |  // /
                            ((ulong)input[index + 0] << 32) |  // \ *(ushort *)&index[0] << 32

                            ((ulong)input[index + 3] << 24) |  // /
                            ((ulong)input[index + 2] << 16) |  // |
                            ((ulong)input[index + 1] << 8) |   // |
                            ((ulong)input[index + 0] << 0);    // \ *(uint *)&index[0]

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 7:
                    {
                        // TODO: This *really* looks incorrect, but this seems to be how sansar does it. I'm not sure if we ever execute it though
                        var val =
                            ((ulong)input[index + 6] << 48) |   // | input[6] << 16 << 32
                            ((ulong)input[index + 1] << 40) |   // /
                            ((ulong)input[index + 0] << 32) |   // \ *(ushort *)&input[0] << 32

                            ((ulong)input[index + 3] << 24) |   // /
                            ((ulong)input[index + 2] << 16) |   // |
                            ((ulong)input[index + 1] << 8) |    // |
                            ((ulong)input[index + 0] << 0);     // \ *(uint *)&input[0]

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 8:
                    {
                        var val =
                            ((ulong)input[index + 7] << 56) |
                            ((ulong)input[index + 6] << 48) |
                            ((ulong)input[index + 5] << 40) |
                            ((ulong)input[index + 4] << 32) |
                            ((ulong)input[index + 3] << 24) |
                            ((ulong)input[index + 2] << 16) |
                            ((ulong)input[index + 1] << 8) |
                            ((ulong)input[index + 0] << 0);

                        output = readMask & (val >> numExistingBits);
                        break;
                    }
                case 9:
                    {
                        ReadBits(input, bitOffset, out var outHigh, numBits - 8);
                        ReadBits(input, bitOffset + numBits - 8, out var outLow, 8);

                        output = (outHigh & 0xFF) | (outLow << (numBits - 8));
                        break;
                    }
                default:
                    throw new Exception($"Somehow attempted to read {numBytesToRead} bytes with {nameof(ReadBits)}");
            }

            return bitOffset + numBits;
        }
    }
}
