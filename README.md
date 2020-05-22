# Cosmos University

This sample project demonstrates how to use Cosmos DB (SQL Api) with EF Core 3.1

The project takes advantage of the TPH (Table per hierarchy) convention to store all types into a single Cosmos container.

```

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

```

The partition key for all entities is the `/College` property.

This is to demonstrate how optimise for reads from a single logical partition.

The idea is that we would be expecting to receive many queries that are concerned with the staff and students of a single college, so this partition caters for that scenario.

# Requirements

Azure subscription with a [Cosmos DB account](https://docs.microsoft.com/en-us/azure/cosmos-db/create-cosmosdb-resources-portal)

# Secret Management

To avoid leaking secrets in source this project uses the dotnet [secrets manager](https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-3.1&tabs=windows)

You will need to configure locally your Cosmos https endpoint and its secret

The `dotnet` cli can help with this

`dotnet user-secrets set "CosmosSettings:ServiceEndpoint" "https://your_endpoint_here"`

`dotnet user-secrets set "CosmosSettings:Secret" "12345"`
