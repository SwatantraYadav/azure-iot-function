using Common.Data;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fnShopping
{
    class TableStorage
    {
        public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString, TraceWriter log)
        {
            CloudStorageAccount storageAccount;
            try
            {
                storageAccount = CloudStorageAccount.Parse(storageConnectionString);
            }
            catch (FormatException)
            {
                log.Info("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
                throw;
            }
            catch (ArgumentException)
            {
                log.Info("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
                Console.ReadLine();
                throw;
            }

            return storageAccount;
        }


        /// <summary>
        /// Create a table for the sample application to process messages in. 
        /// </summary>
        /// <returns>A CloudTable object</returns>
        public static async Task<CloudTable> CreateTableAsync(string tableName, TraceWriter log)
        {
            // Retrieve storage account information from connection string.
            CloudStorageAccount storageAccount = CreateStorageAccountFromConnectionString(Constants.DatabaseConnectionString, log);

            // Create a table client for interacting with the table service
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            log.Info("Create a Table for the demo");

            // Create a table client for interacting with the table service 
            CloudTable table = tableClient.GetTableReference(tableName);
            try
            {
                if (await table.CreateIfNotExistsAsync())
                {
                    log.Info("Created Table named: {0}", tableName);
                }
                else
                {
                    log.Info("Table {0} already exists", tableName);
                }
            }
            catch (StorageException)
            {
                log.Info("If you are running with the default configuration please make sure you have started the storage emulator. Press the Windows key and type Azure Storage to select and run it from the list of applications - then restart the sample.");
                Console.ReadLine();
                throw;
            }
            
            return table;
        }

        public static async Task<PrintEntity> InsertPrintAsync(CloudTable table, PrintBaseServer photo, string email, TraceWriter log)
        {
            var rowKey = email.Split("@".ToCharArray())[0].Replace(".", "");
            log.Info($"InsertPrintAsync {rowKey}");
            // Create an instance of a customer entity. See the Model\CustomerEntity.cs for a description of the entity.
            PrintEntity printEntity = new PrintEntity("PartitionKey", rowKey)
            {
                Email = email,
                Photo = photo.Photo,
                Cost =  5, //photo.PrintType.Cost,
                Quantity = photo.Quantity
            };

            log.Info($"PrintEntity {printEntity}");

            try
            {
                // Create the InsertOrReplace table operation
                TableOperation insertOrMergeOperation = TableOperation.InsertOrMerge(printEntity);

                // Execute the operation.
                TableResult result = await table.ExecuteAsync(insertOrMergeOperation);
                return result.Result as PrintEntity;

            }
            catch (StorageException e)
            {
                log.Info(e.Message);
                throw;
            }

        }
    }
}
