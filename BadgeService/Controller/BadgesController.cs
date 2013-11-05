using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Services;
using Model;
using Newtonsoft.Json;
using System.Net;

namespace BadgeService.Controller
{
    /// <summary>
    /// The Web API Controller to 
    /// perform the badge operations.
    /// </summary>
    /// 
    public class BadgesController : ApiController
    {
        readonly IBadgeStorageService storageService;

        readonly IBadgeGenerator generator;

        public BadgesController(IBadgeStorageService service, IBadgeGenerator gen)
        {
            this.storageService = service;
            this.generator = gen;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddBadge(Badge badge)
        {
            await storageService.SaveBadge(badge);

            return Request.CreateResponse("Success");
        }

        [HttpPost]
        public async Task<HttpResponseMessage> UpdateBadge(Badge badge)
        {
            var success = await storageService.UpdateBadge(badge);

            return Request.CreateResponse<bool>(success);
        }

        [HttpPost]
        public async Task<HttpResponseMessage> DeleteBadge([FromBody]
                                                           string badgeName)
        {
            var success = await storageService.DeleteBadge(badgeName);

            return Request.CreateResponse<bool>(success);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllBadgeNames()
        {
            var badges = await storageService.ListAllBadges();

            return Request.CreateResponse <List<string>>(badges);
        }
    
        [HttpPost]
        public async Task<HttpResponseMessage> GenerateBadge(BadgeGenData data)
        {
          

           var imageBytes = await generator.GenerateBadgeImage(data.BadgeName, data.BadgeData);

            if(imageBytes == null)
            {
                return null;
            }

            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Content = new ByteArrayContent(imageBytes);
            response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");

            return response;

            
        }
    }
}