using System.Collections.Generic;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace fnShopping
{
    public static class DatabaseTriggered
    {
        [FunctionName("DatabaseTriggered")]
        public static void Run([CosmosDBTrigger(
            databaseName: "TablesDB",
            collectionName: "PrintList",
            ConnectionStringSetting = "DefaultEndpointsProtocol=https;AccountName=dbserverlesscosmos;AccountKey=66sGoaHZlOVR4YNqvipbOurp55fINYHlm0r4RtqmGPeDISJXJflmOaYSbPwxNPrM2ELwPE9kowNoFVR7sFUWDA==;TableEndpoint=https://dbserverlesscosmos.table.cosmosdb.azure.com:443/;",
            LeaseCollectionName = "leases")]IReadOnlyList<Document> documents, TraceWriter log)
        {
            if (documents != null && documents.Count > 0)
            {
                log.Verbose("Documents modified " + documents.Count);
                log.Verbose("First document Id " + documents[0].Id);
            }
        }
    }
}
