using System;

using Common;
using System.IO.MemoryMappedFiles;

namespace SimExecMemoryMapper
{
    internal class Program
    {
        static void Main(string[] args)
        {
            EternalModel model = new EternalModel();
            model.Run();
        }
    }
    public class EternalModel()
    {
        public void Run()
        {
            Console.WriteLine("Hello World!");


            string mappedFile = "SimExecMemoryMapp";






            // Create a 10MB memory mapped file
            MemoryMappedFile simexecMappedFile = MemoryMappedFile.OpenExisting(mappedFile);

            //var thing = simexecMappedFile.CreateViewStream();
            var thing = simexecMappedFile.CreateViewAccessor();

            thing.Read(0, out int num_elements);


            SimexecMemoryMapElement[] map = new SimexecMemoryMapElement[num_elements];


            for (int i = 0; i < num_elements; i++)
            {
                int arrayPos = sizeof(int) + (i * SimexecMemoryMapElement.ELE_SIZE);

                byte[] buffer = new byte[SimexecMemoryMapElement.ELE_SIZE];

                thing.ReadArray(arrayPos, buffer, 0, SimexecMemoryMapElement.ELE_SIZE);
               
                map[i] = new SimexecMemoryMapElement();
                map[i].FromBuffer(buffer);
            }

            Console.WriteLine("donemap");


            while (true)
            {
                System.Threading.Thread.Sleep(1000);
            }

        }

    }
}
