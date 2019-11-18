﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DICOMcloud.DataAccess.Database;
using Microsoft.Azure;

namespace PACS.Cloud
{
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        public string ConnectionString => CloudConfigurationManager.GetSetting("app:PacsDataArchieve");
    }
}