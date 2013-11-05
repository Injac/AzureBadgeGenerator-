using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using Model;
using System.Xml;
using System.IO;
using System.Drawing.Imaging;
using BadgeService.Ioc;
using System.Drawing;

namespace BadgeService.Service
{
    /// <summary>
    /// Generates a png image from a svg
    /// with textparameters to be assigned
    /// to SVG parameters.
    /// </summary>
   
    public class BadgeGenerator : IBadgeGenerator
    {
        private IBadgeStorageService badgeStoreageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeGenerator" /> class.
        /// </summary>
        /// <param name="service">The service.</param>
        /// <param name="badgeName">Name of the badge.</param>
        /// <param name="content">The content.</param>
        public BadgeGenerator(IBadgeStorageService service)
        {
            if (service == null)
            {
                throw new ArgumentNullException("storage service cannot be null", "service");
            }
           
            this.badgeStoreageService = service;
        }

        /// <summary>
        /// Generates the badge image
        /// </summary>
        /// <returns>Bytes of an PNG image if successfull, otherwhise null.</returns>
        public async Task<byte[]> GenerateBadgeImage(string badgeName, List<BadgeTextContent> content)
        {
            if (string.IsNullOrEmpty(badgeName))
            {
                throw new ArgumentNullException("badge name cannot be null", "service");
            }

            if (content == null || content.Count == 0)
            {
                throw new ArgumentNullException("content cannot be null or not contain any elements.", "content");
            }

            return await Task<byte[]>.Run(async () =>
            {
                var xmlDoc = new XmlDocument();

                var badge = await this.badgeStoreageService.LoadBadge(badgeName);

                if (String.IsNullOrEmpty(badge))
                {
                    return null;
                }

                

                xmlDoc.LoadXml(badge);
                var svgDoc = SvgDocument.Open(xmlDoc);


                foreach (var textContent in content)
                {

                    svgDoc.GetElementById<SvgText>(textContent.ElementName).Content = textContent.Value;

                }
                
                using (MemoryStream imgStream = new MemoryStream())
                {
                    try
                    {

                        var bitmap = svgDoc.Draw();
                        
                        ImageConverter converter = new ImageConverter();
                        return (byte[])converter.ConvertTo(bitmap, typeof(byte[]));

                       
                    }
                    catch (Exception ex)
                    {

                        return null;
                    }
                }
            });
        }
    }
}