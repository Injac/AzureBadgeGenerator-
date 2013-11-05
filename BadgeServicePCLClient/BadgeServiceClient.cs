using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BadgeServicePCLClient
{
    public class BadgeServiceClient
    {

        private readonly string serviceAddress;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeServiceClient" /> class.
        /// </summary>
        /// <param name="serviceAddress">The service address.</param>
        public BadgeServiceClient(string serviceAddress)
        {
            this.serviceAddress = serviceAddress;
        }

        /// <summary>
        /// Creates the badge image.
        /// </summary>
        /// <param name="badgeData">The badge data.</param>
        /// <returns></returns>
        public async Task<byte[]> CreateBadgeImage(BadgeGenData badgeData)
        {
            
            HttpClient cl = new HttpClient();

            cl.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var badgeDataSerialized = JsonConvert.SerializeObject(badgeData);

            var url = String.Format("{0}/{1}", this.serviceAddress, "GenerateBadge");
            var content = new StringContent(badgeDataSerialized,null,"application/json");

            var serverContent = await cl.PostAsync(url, content);

            var data = await serverContent.Content.ReadAsByteArrayAsync();

            if(data.Length == 0)
            {
                return null;
            }
            else
            {
                return data;
            }

            return null;
        }

    }
}
