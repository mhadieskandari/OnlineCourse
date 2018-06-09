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
        private readonly string _bbbServerId;
        private readonly string _bbbServerIpAddress;
        public PublicConfig(IConfiguration config)
        {
            var configuration = config;
            //todo serialaze config from json to object 
            _paginationPZ = Convert.ToInt32(configuration.GetSection("PublicConfig").GetSection("PaginationPageSize").Value);
            _bbbModeratorPassword= configuration.GetSection("BbbConfig").GetSection("ModeratorPassword").Value;
            _bbbServerId = configuration.GetSection("BbbConfig").GetSection("ServerId").Value;
            _bbbServerIpAddress = configuration.GetSection("BbbConfig").GetSection("ServerIpAddress").Value;
        }        


        public int GetPageSize()
        {
            return _paginationPZ;
        }

        public string BbbGetModeratorPassword()
        {
            return _bbbModeratorPassword;
        }

        public string BbbGetServerId()
        {
            return _bbbServerId;
        }
        public string BbbGetServerIpAddress()
        {
            return _bbbServerIpAddress;
        }
    }
}
