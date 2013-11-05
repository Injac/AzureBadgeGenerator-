using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using System.Data.SqlClient;
using Microsoft.WindowsAzure;
using Newtonsoft.Json;
using Model;
using BadgeService.Ioc;

namespace BadgeService.Storage
{
    /// <summary>
    /// Use bare SQL to manage badges
    /// </summary>
 
    public class BadgeSqlStorageDriver : IBadgeStorageService
    {
        private readonly string sqlConnectionString;

        /// <summary>
        /// Initializes a new instance of the <see cref="BadgeSqlStorageDriver" /> class.
        /// </summary>
        public BadgeSqlStorageDriver()
        {
            this.sqlConnectionString = CloudConfigurationManager.GetSetting("SqlConnection");

            if (String.IsNullOrEmpty(this.sqlConnectionString))
            {
                ///TODO:LOGGER
                throw new ArgumentException("Cloud setting could not be found.", "SqlConnectionString");
            }
        }

        /// <summary>
        /// Saves the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns></returns>
        public async Task SaveBadge(Model.Badge badge)
        {
            if (badge == null)
            {
                throw new ArgumentNullException("badge cannot be null.", "badge");
            }

            using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
            {
                try
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand();

                    var badgeSerialized = await JsonConvert.SerializeObjectAsync(badge);
                    
                    cmd.CommandText = "Insert into badges (BadgeName,Badge) values (@BadgeName,@Badge)";
                    cmd.Parameters.Add(new SqlParameter("@BadgeName", badge.BadgeName.ToLower()));
                    cmd.Parameters.Add(new SqlParameter("@Badge", badgeSerialized));

                    cmd.Connection = con;

                    await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    ///TODO:LOGGER
                    throw;
                }
            }
        }

        /// <summary>
        /// Loads the badge.
        /// </summary>
        /// <param name="badgeName">Name of the badge.</param>
        /// <returns></returns>
        public async Task<string> LoadBadge(string badgeName)
        {
            if (String.IsNullOrEmpty(badgeName))
            {
                throw new ArgumentNullException("badge name cannot be null.", "badgeName");
            }

            using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
            {
                try
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "Select badge from badges where BadgeName = @badgeName";
                    cmd.Parameters.Add(new SqlParameter("@badgeName", badgeName));
                    cmd.Connection = con;
                    var reader = await cmd.ExecuteReaderAsync();

                    if (await reader.ReadAsync())
                    {
                        var badge = reader.GetString(0);

                        if (string.IsNullOrEmpty(badge))
                        {
                            return null;
                        }

                        
                        return badge;
                    }
                    else
                    {
                        //Nothing here.
                        return null;
                    }
                }
                catch (Exception ex)
                {
                    ///TODO:LOGGER
                    throw;
                }
            }
        }

        /// <summary>
        /// Lists all badges.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> ListAllBadges()
        {
            using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
            {
                try
                {
                    List<string> badgeNames = new List<string>();
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "Select badgename from badges";
                    cmd.Connection = con;
                    var reader = await cmd.ExecuteReaderAsync();

                    while (await reader.ReadAsync())
                    {
                        badgeNames.Add(reader.GetString(0));
                    }

                    if (badgeNames.Count == 0)
                    {
                        return null;
                    }

                    return badgeNames;
                }
                catch (Exception ex)
                {
                    ///TODO:LOGGER
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes the badge.
        /// </summary>
        /// <param name="badgeName">Name of the badge.</param>
        /// <returns></returns>
        public async Task<bool> DeleteBadge(string badgeName)
        {
            if (string.IsNullOrEmpty(badgeName))
            {
                throw new ArgumentNullException("badge name cannot be null.", "badgeName");
            }

            using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
            {
                try
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "Delete from badges where BadgeName =@BadgeName";
                    cmd.Parameters.Add(new SqlParameter("@BadgeName", badgeName.ToLower()));
                  
                    cmd.Connection = con;

                    var count = await cmd.ExecuteNonQueryAsync();

                    if (count == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    ///TODO:LOGGER
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates the badge.
        /// </summary>
        /// <param name="badge">The badge.</param>
        /// <returns></returns>
        public async Task<bool> UpdateBadge(Badge badge)
        {
            if (badge == null)
            {
                throw new ArgumentNullException("badge cannot be null.", "badge");
            }

            using (SqlConnection con = new SqlConnection(this.sqlConnectionString))
            {
                try
                {
                    await con.OpenAsync();

                    SqlCommand cmd = new SqlCommand();

                    var badgeSerialized = await JsonConvert.SerializeObjectAsync(badge);

                    cmd.CommandText = "Upadte badges set Badge = @Badge where BadgeName = @BadgeName";
                    cmd.Parameters.Add(new SqlParameter("@BadgeName", badge.BadgeName.ToLower()));
                    cmd.Parameters.Add(new SqlParameter("@Badge", badgeSerialized));

                    cmd.Connection = con;

                    await cmd.ExecuteNonQueryAsync();

                    return true;
                }
                catch (Exception ex)
                {
                    ///TODO:LOGGER
                    return false;
                }
            }
        }
    }
}