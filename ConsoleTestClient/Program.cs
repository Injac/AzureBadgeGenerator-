using BadgeServicePCLClient;
using Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.Run( async ()=>
            {
                BadgeServiceClient cl = new BadgeServiceClient("http://[yourhost]:800/badges");

                BadgeGenData data = new BadgeGenData();

                data.BadgeName = "test";

                data.BadgeData.Add(new BadgeTextContent() { ElementName = "text3003", Value = "ConsoleTest" });
                data.BadgeData.Add(new BadgeTextContent() { ElementName = "text3007", Value = "ConsoleTest" });
                data.BadgeData.Add(new BadgeTextContent() { ElementName = "text2999", Value = "ConsoleTest" });

                var imageData = await cl.CreateBadgeImage(data);

                if (imageData != null)
                {
                    if (imageData.Length > 0)
                    {


                        try
                        {

                                 MemoryStream ms = new MemoryStream(imageData);
                                                           

                                 Image img = Image.FromStream(ms);
                                

                            
                               

                                img.Save(@"C:\Users\Ilija\Documents\test.png");
                            
                        }
                        catch (Exception ex)
                        {

                            Console.WriteLine(ex.Message);

                        }
                    }
                }//last if
            }).Wait();

            Console.WriteLine("done");
            Console.Read();
        }
    }
}
