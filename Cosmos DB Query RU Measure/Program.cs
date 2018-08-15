using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string docId = Guid.NewGuid().ToString();
            string attId = Guid.NewGuid().ToString();

            var myDoc = new { id = docId, Name = "Max", City = "Aberdeen" }; // this is the document you are trying to save
            
            var client = GetClientAsync().GetAwaiter().GetResult();
            var createUrl = UriFactory.CreateDocumentCollectionUri("mytestdb", "test");
            ResourceResponse<Document> document = client.CreateDocumentAsync(createUrl, myDoc).GetAwaiter().GetResult();

            // Measure the performance (request units) of writes
            // ResourceResponse<Document> response = await client.CreateDocumentAsync(collectionSelfLink, myDocument);
            Console.WriteLine("Insert of document consumed {0} request units", document.RequestCharge);
            // Measure the performance (request units) of queries

            IDocumentQuery<dynamic> queryable = client.CreateDocumentQuery(
                UriFactory.CreateDocumentCollectionUri("mytestdb", "test"), "select top 1 * from c").AsDocumentQuery();

            FeedResponse<dynamic> queryResponse = queryable.ExecuteNextAsync<dynamic>().GetAwaiter().GetResult();
            Console.WriteLine("Query batch consumed {0} request units", queryResponse.RequestCharge);
            Console.Read();
            
        }

        private static DocumentClient documentClient;

        private static async Task<DocumentClient> GetClientAsync()
        {
            if (documentClient == null)
            {
                var endpointUrl = "https://catecosmos.documents.azure.com:443/";
                var primaryKey = "o5RR73dDwfcDetx7Xr91kGs22QOkcgJgfMgyyJ8xKBbes6mooRtXY1vRo0gk5T5poFNAYviI9So53xsKgPiTsQ==";

                documentClient = new DocumentClient(new Uri(endpointUrl), primaryKey);
                await documentClient.OpenAsync();
            }

            return documentClient;
        }
    }
}