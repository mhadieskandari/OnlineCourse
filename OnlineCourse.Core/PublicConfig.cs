using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnlineCourse.Core
{
    public class PublicConfig
    {
        private readonly int _paginationPZ;
        private readonly string _bbbModeratorPassword;
        public PublicConfig(IConfiguration config)
        {
            var configuration = config;
            //todo serialaze config from json to object 
            _paginationPZ = Convert.ToInt32(configuration.GetSection("PublicConfig").GetSection("PaginationPageSize").Value);
            _bbbModeratorPassword= configuration.GetSection("BbbConfig").GetSection("ModeratorPassword").Value;
        }        


        public int GetPageSize()
        {
            return _paginationPZ;
        }

        public string BbbGetModeratorPassword()
        {
            return _bbbModeratorPassword;
        }
    }
}
