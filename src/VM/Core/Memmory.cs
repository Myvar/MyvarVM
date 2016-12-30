namespace MyvarVM.Core
{
    public class Memmory
    {
        public byte[] Ram { get; set; }

        public Memmory(Config cfg)
        {
            Ram = new byte[cfg.RamSize];
        }

        public void LoadIntoMemmory(ref byte[] buffer, int offset)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                Ram[offset + i] = buffer[i];
            }
        }
    }
}