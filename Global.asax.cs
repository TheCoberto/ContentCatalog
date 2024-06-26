﻿using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using static CastafraySoundCatalog.Globals;

namespace CastafraySoundCatalog
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var secretClient = new SecretClient(new Uri("https://contentconnectionstring.vault.azure.net/"), new DefaultAzureCredential());
            var connectionString = secretClient.GetSecret("ConnectionString");
            var azureStorageAccountName = secretClient.GetSecret("StorageAccountName");
            var azureStorageAccountKey = secretClient.GetSecret("StorageAccountKey");

            ConnectionString = connectionString.Value.Value;
            AzureStorageAccountName = azureStorageAccountName.Value.Value;
            AzureStorageAccountKey = azureStorageAccountKey.Value.Value;
        }
    }
}