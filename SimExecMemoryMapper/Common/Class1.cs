using System.Text;

namespace Common
{
    public class SimexecMemoryMapElement
    {
        public const int MAX_VAR_STRING = 32;

        public const int ELE_SIZE = sizeof(int) + MAX_VAR_STRING;
        public string VarName { get; set; } = "";
        public int Address { get; set; }

        public byte[] ToBuffer()
        {
            byte[] buffer = new byte[sizeof(int) + MAX_VAR_STRING];

            byte[] varNameBuff = Encoding.UTF8.GetBytes(VarName);
            Array.Copy(varNameBuff,0, buffer,0, varNameBuff.Length);

            byte[] adressbuff = BitConverter.GetBytes(Address);
            Array.Copy(adressbuff, 0, buffer, MAX_VAR_STRING, adressbuff.Length);

            return buffer;
        }

        public void FromBuffer(byte[] buffer)
        {
            VarName = Encoding.UTF8.GetString(buffer,0, MAX_VAR_STRING).Trim('\0');
            

            Address = BitConverter.ToInt32(buffer, MAX_VAR_STRING);
        }
    }
}
