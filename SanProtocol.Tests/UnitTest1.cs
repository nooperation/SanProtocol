using SanBot.Packets;

namespace SanProtocol.Tests
{
    public class UnitTest1
    {
        private ulong ReadUlong(List<byte> buffer, int? offset = null, int? count = null)
        {
            ulong value = 0;

            for (int i = (offset ?? 0); i < (count ?? buffer.Count); i++)
            {
                value |= (ulong)buffer[i] << (8 * i);
            }

            return value;
        }

        [Fact]
        public void WriteBits()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 1, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_00000001, buffer[0]);
            Assert.Equal(1, offset);

            offset = BitWriter.WriteBits(buffer, offset, 0, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_00000001, buffer[0]);
            Assert.Equal(2, offset);

            offset = BitWriter.WriteBits(buffer, offset, 1, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_00000101, buffer[0]);
            Assert.Equal(3, offset);

            offset = BitWriter.WriteBits(buffer, offset, 1, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_00001101, buffer[0]);
            Assert.Equal(4, offset);

            offset = BitWriter.WriteBits(buffer, offset, 1, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_00011101, buffer[0]);
            Assert.Equal(5, offset);

            offset = BitWriter.WriteBits(buffer, offset, 1, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_00111101, buffer[0]);
            Assert.Equal(6, offset);

            offset = BitWriter.WriteBits(buffer, offset, 0, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_00111101, buffer[0]);
            Assert.Equal(7, offset);

            offset = BitWriter.WriteBits(buffer, offset, 1, 1);
            Assert.Single(buffer);
            Assert.Equal(0b_10111101, buffer[0]);
            Assert.Equal(8, offset);

            offset = BitWriter.WriteBits(buffer, offset, 1, 1);
            Assert.Equal(2, buffer.Count);
            Assert.Equal(0b_10111101, buffer[0]);
            Assert.Equal(0b_00000001, buffer[1]);
            Assert.Equal(9, offset);
        }

        [Fact]
        public void WriteNibbles()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x0D, 4);
            Assert.Single(buffer);
            Assert.Equal(4, offset);
            Assert.Equal(0x0D, buffer[0]);

            offset = BitWriter.WriteBits(buffer, offset, 0x0E, 4);
            Assert.Single(buffer);
            Assert.Equal(8, offset);
            Assert.Equal(0xED, buffer[0]);

            offset = BitWriter.WriteBits(buffer, offset, 0x05, 4);
            Assert.Equal(2, buffer.Count);
            Assert.Equal(12, offset);
            Assert.Equal(0xED, buffer[0]);
            Assert.Equal(0x05, buffer[1]);
        }

        [Fact]
        public void Write1Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0xED, 8);
            Assert.Single(buffer);
            Assert.Equal(8, offset);
            Assert.Equal(0xED, buffer[0]);

            offset = BitWriter.WriteBits(buffer, offset, 0x54, 8);
            Assert.Equal(2, buffer.Count);
            Assert.Equal(16, offset);
            Assert.Equal(0xED, buffer[0]);
            Assert.Equal(0x54, buffer[1]);

            offset = BitWriter.WriteBits(buffer, offset, 0x05, 8);
            Assert.Equal(3, buffer.Count);
            Assert.Equal(24, offset);
            Assert.Equal(0xED, buffer[0]);
            Assert.Equal(0x54, buffer[1]);
            Assert.Equal(0x05, buffer[2]);
        }


        [Fact]
        public void Write1Byte_offset3()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_101, 3);

            offset = BitWriter.WriteBits(buffer, offset, 0b_11011011, 8);
            Assert.Equal(2, buffer.Count);
            Assert.Equal(11, offset);
            Assert.Equal(0b_11011011_101UL, ReadUlong(buffer));

            offset = BitWriter.WriteBits(buffer, offset, 0b_11101010, 8);
            Assert.Equal(3, buffer.Count);
            Assert.Equal(19, offset);
            Assert.Equal(0b_11101010_11011011_101UL, ReadUlong(buffer));
        }

        [Fact]
        public void Write1Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_11011011, 8);
            Assert.Equal(3, buffer.Count);
            Assert.Equal(19, offset);
            Assert.Equal(0b_11011011_10101010101UL, ReadUlong(buffer));

            offset = BitWriter.WriteBits(buffer, offset, 0b_11101010, 8);
            Assert.Equal(4, buffer.Count);
            Assert.Equal(27, offset);
            Assert.Equal(0b_11101010_11011011_10101010101UL, ReadUlong(buffer));
        }

        [Fact]
        public void Write2Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x1234, 16);
            Assert.Equal(2, buffer.Count);
            Assert.Equal(16, offset);
            Assert.Equal(0x34, buffer[0]);
            Assert.Equal(0x12, buffer[1]);

            offset = BitWriter.WriteBits(buffer, offset, 0x5678, 16);
            Assert.Equal(4, buffer.Count);
            Assert.Equal(32, offset);
            Assert.Equal(0x34, buffer[0]);
            Assert.Equal(0x12, buffer[1]);
            Assert.Equal(0x78, buffer[2]);
            Assert.Equal(0x56, buffer[3]);
        }


        [Fact]
        public void Write2Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_1110101011011011, 16);
            Assert.Equal(4, buffer.Count);
            Assert.Equal(16 + 11, offset);
            Assert.Equal(0b_1110101011011011_10101010101UL, ReadUlong(buffer));

            offset = BitWriter.WriteBits(buffer, offset, 0b_1111111111111111, 16);
            Assert.Equal(6, buffer.Count);
            Assert.Equal(16 + 11 + 16, offset);
            Assert.Equal(0b_1111111111111111_1110101011011011_10101010101UL, ReadUlong(buffer));
        }

        [Fact]
        public void Write3Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x123456, 24);
            Assert.Equal(3, buffer.Count);
            Assert.Equal(24, offset);
            Assert.Equal(0x56, buffer[0]);
            Assert.Equal(0x34, buffer[1]);
            Assert.Equal(0x12, buffer[2]);

            offset = BitWriter.WriteBits(buffer, offset, 0x789ABC, 24);
            Assert.Equal(6, buffer.Count);
            Assert.Equal(48, offset);
            Assert.Equal(0x56, buffer[0]);
            Assert.Equal(0x34, buffer[1]);
            Assert.Equal(0x12, buffer[2]);
            Assert.Equal(0xBC, buffer[3]);
            Assert.Equal(0x9A, buffer[4]);
            Assert.Equal(0x78, buffer[5]);
        }

        [Fact]
        public void Write3Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_111111011000110110011101, 24);
            Assert.Equal(5, buffer.Count);
            Assert.Equal(24 + 11, offset);
            Assert.Equal(0b_111111011000110110011101_10101010101UL, ReadUlong(buffer));
        }

        [Fact]
        public void Write4Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x12345678, 32);
            Assert.Equal(4, buffer.Count);
            Assert.Equal(32, offset);
            Assert.Equal(0x78, buffer[0]);
            Assert.Equal(0x56, buffer[1]);
            Assert.Equal(0x34, buffer[2]);
            Assert.Equal(0x12, buffer[3]);

            offset = BitWriter.WriteBits(buffer, offset, 0x9ABCDEF0, 32);
            Assert.Equal(8, buffer.Count);
            Assert.Equal(64, offset);
            Assert.Equal(0x78, buffer[0]);
            Assert.Equal(0x56, buffer[1]);
            Assert.Equal(0x34, buffer[2]);
            Assert.Equal(0x12, buffer[3]);
            Assert.Equal(0xF0, buffer[4]);
            Assert.Equal(0xDE, buffer[5]);
            Assert.Equal(0xBC, buffer[6]);
            Assert.Equal(0x9A, buffer[7]);
        }

        [Fact]
        public void Write4Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_11001100111111011000110110011101, 32);
            Assert.Equal(6, buffer.Count);
            Assert.Equal(32 + 11, offset);
            Assert.Equal(0b_11001100111111011000110110011101_10101010101UL, ReadUlong(buffer));
        }

        [Fact]
        public void Write5Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x123456789A, 40);
            Assert.Equal(5, buffer.Count);
            Assert.Equal(40, offset);
            Assert.Equal(0x9A, buffer[0]);
            Assert.Equal(0x78, buffer[1]);
            Assert.Equal(0x56, buffer[2]);
            Assert.Equal(0x34, buffer[3]);
            Assert.Equal(0x12, buffer[4]);
        }

        [Fact]
        public void Write5Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_1110001111001100111111011000110110011101, 5*8);
            Assert.Equal(7, buffer.Count);
            Assert.Equal(5*8 + 11, offset);
            Assert.Equal(0b_1110001111001100111111011000110110011101_10101010101UL, ReadUlong(buffer));
        }

        [Fact]
        public void Write6Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x123456789ABC, 48);
            Assert.Equal(6, buffer.Count);
            Assert.Equal(48, offset);
            Assert.Equal(0xBC, buffer[0]);
            Assert.Equal(0x9A, buffer[1]);
            Assert.Equal(0x78, buffer[2]);
            Assert.Equal(0x56, buffer[3]);
            Assert.Equal(0x34, buffer[4]);
            Assert.Equal(0x12, buffer[5]);
        }

        [Fact]
        public void Write6Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_110110011110001111001100111111011000110110011101, 6 * 8);
            Assert.Equal(8, buffer.Count);
            Assert.Equal(6 * 8 + 11, offset);
            Assert.Equal(0b_110110011110001111001100111111011000110110011101_10101010101UL, ReadUlong(buffer));
        }

        [Fact]
        public void Write7Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x123456789ABCDE, 56);
            Assert.Equal(7, buffer.Count);
            Assert.Equal(56, offset);
            Assert.Equal(0xDE, buffer[0]);
            Assert.Equal(0xBC, buffer[1]);
            Assert.Equal(0x9A, buffer[2]);
            Assert.Equal(0x78, buffer[3]);
            Assert.Equal(0x56, buffer[4]);
            Assert.Equal(0x34, buffer[5]);
            Assert.Equal(0x12, buffer[6]);
        }

        [Fact]
        public void Write7Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_10110010110110011110001111001100111111011000110110011101, 7 * 8);
            Assert.Equal(9, buffer.Count);
            Assert.Equal(7 * 8 + 11, offset);
            Assert.Equal(0b_10010110110011110001111001100111111011000110110011101_10101010101UL, ReadUlong(buffer, 0, 8));
            Assert.Equal(0b_101UL, ReadUlong(buffer, 8));
        }

        [Fact]
        public void Write8Byte()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x123456789ABCDEF0, 64);
            Assert.Equal(8, buffer.Count);
            Assert.Equal(64, offset);
            Assert.Equal(0xF0, buffer[0]);
            Assert.Equal(0xDE, buffer[1]);
            Assert.Equal(0xBC, buffer[2]);
            Assert.Equal(0x9A, buffer[3]);
            Assert.Equal(0x78, buffer[4]);
            Assert.Equal(0x56, buffer[5]);
            Assert.Equal(0x34, buffer[6]);
            Assert.Equal(0x12, buffer[7]);
        }

        [Fact]
        public void Write8Byte_offset11()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0b_10101010101, 11);

            offset = BitWriter.WriteBits(buffer, offset, 0b_1010001110110010110110011110001111001100111111011000110110011101, 64);
            Assert.Equal(10, buffer.Count);
            Assert.Equal(64 + 11, offset);
            Assert.Equal(0b_10010110110011110001111001100111111011000110110011101_10101010101UL, ReadUlong(buffer, 0, 8));
            Assert.Equal(0b_10100011101UL, ReadUlong(buffer, 8));
        }

        [Fact]
        public void Write8Byte_offset1()
        {
            var buffer = new List<byte>();
            var offset = 0L;

            offset = BitWriter.WriteBits(buffer, offset, 0x123456789ABCDEF0, 64);
            Assert.Equal(8, buffer.Count);
            Assert.Equal(64, offset);
            Assert.Equal(0xF0, buffer[0]);
            Assert.Equal(0xDE, buffer[1]);
            Assert.Equal(0xBC, buffer[2]);
            Assert.Equal(0x9A, buffer[3]);
            Assert.Equal(0x78, buffer[4]);
            Assert.Equal(0x56, buffer[5]);
            Assert.Equal(0x34, buffer[6]);
            Assert.Equal(0x12, buffer[7]);
        }
    }
}
