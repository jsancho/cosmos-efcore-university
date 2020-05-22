# Cosmos University

This sample project that demonstrates how to use Cosmos DB (SQL Api) with EF Core 3.1

The project takes advantage of the TPH (Table per hierarchy) convention to store all types into a single Cosmos container.


                              +----------------+
                              |                |
                              |    Person      |
                              |                |
                              +------+--+------+
                                     ^  ^
                                     |  |
                        +------------+  +--------+
                        |                        |
                 +------+-----+           +------+------+
                 |            |           |             |
                 |   Staff    |           |   Student   |
                 |            |           |             |
                 +----+--+----+           +-------------+
                      ^  ^
         +------------+  +---------+
         |                         |
+--------+---------+        +------+------+
|                  |        |             |
|  Administrative  |        |  Lecturer   |
|                  |        |             |
+------------------+        +-------------+


There partition key for all entities is the `/College` property.

This is to demonstrate how optimise for reads from a single logical partition.
The idea is that there would be queries being performed to retrieve staff and students for a given college.

# Requirements

Azure subscription with a [Cosmos DB account](https://docs.microsoft.com/en-us/azure/cosmos-db/create-cosmosdb-resources-portal)

# Secret Management

To avoid leaking secrets the endpoint this project uses the dotnet [secrets manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows)

You will need to configure locally your Cosmos https endpoint and its secret

`dotnet user-secrets set "CosmosSettings:ServiceEndpoint" "https://your_endpoint_here"`
`dotnet user-secrets set "CosmosSettings:Secret" "12345"`
