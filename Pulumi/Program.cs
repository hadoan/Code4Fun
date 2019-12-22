﻿using System.Collections.Generic;
using System.Threading.Tasks;

//https://medium.com/@hadoan/hello-azure-infrastructure-as-code-using-c-4467d53369d0
using Pulumi;
using Pulumi.Azure.Core;
using Pulumi.Azure.Storage;

class Program
{
    static Task<int> Main()
    {
        return Deployment.RunAsync(() => {

            // Create an Azure Resource Group
            var resourceGroup = new ResourceGroup("resourceGroup");

            // Create an Azure Storage Account
            var storageAccount = new Account("storage", new AccountArgs
            {
                ResourceGroupName = resourceGroup.Name,
                AccountReplicationType = "LRS",
                AccountTier = "Standard",
            });

            // Export the connection string for the storage account
            return new Dictionary<string, object>
            {
                { "connectionString", storageAccount.PrimaryConnectionString },
            };
        });
    }
}
