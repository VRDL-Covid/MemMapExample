using System;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Text;
using Common;
namespace SimExecMemoryMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            SimExecMemoryMapper mapper = new SimExecMemoryMapper();
            mapper.Run();
        }
    }

    public class SimExecMemoryMapper
    {
        private const int NUM_MAPS = 100000;
        string mappedFile = "SimExecMemoryMapp";


        public void Run()
        {
            SimexecMemoryMapElement[] map = new SimexecMemoryMapElement[NUM_MAPS];
            



            for(int i = 0; i < NUM_MAPS; i++)
            {

                map[i] = new SimexecMemoryMapElement { VarName = $"io_var_thing_detail_qdore{i}", Address = 444210+i };
                //map[i] = new SimExecMemoryMapElement { SimexecAddr = 43210 + i };
            }

            int size = SimexecMemoryMapElement.ELE_SIZE * NUM_MAPS + sizeof(int);


            // Create a 10MB memory mapped file
            MemoryMappedFile simexecMappedFile = MemoryMappedFile.CreateNew(mappedFile, size);

            //var thing = simexecMappedFile.CreateViewStream();
            var thing = simexecMappedFile.CreateViewAccessor();

            thing.Write(0, NUM_MAPS);

            for (int i = 0; i < NUM_MAPS; i++)
            {
                int arrayPos = sizeof(int) + (i * SimexecMemoryMapElement.ELE_SIZE);
                byte[] mappedelebuffer = map[i].ToBuffer();
                thing.WriteArray(arrayPos, mappedelebuffer, 0, mappedelebuffer.Length);
            }

            Console.WriteLine("donemap");
            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }
        }
    }


}