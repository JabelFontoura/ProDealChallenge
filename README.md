# ProDealChallenge

## How to run
### Requirements
* **[.NET SDK 6.0](https://dotnet.microsoft.com/download/dotnet/6.0)**
### Optionals
* **[Postgres 14.3](https://www.enterprisedb.com/postgresql-tutorial-resources-training?uuid=db55e32d-e9f0-4d7c-9aef-b17d01210704&campaignId=7012J000001NhszQAC)**
* **[Visual Studio 22](https://c2rsetup.officeapps.live.com/c2r/downloadVS.aspx?sku=community&channel=Release&version=VS2022&source=VSLandingPage&includeRecommended=true&cid=2030)**

- Option 1
  - Download the repository and execute the following steps in the root folder.
  - Execute `Execute.bat` to restore, and start `WebApi`.
  - The client application should be available at **[https://localhost:7026/swagger](https://localhost:7026/swagger/index.html)**.
  - All interecations can be done using the swagger page.

- Option 2
  - Download the repository and execute the following steps in the root folder.
  - Open `ProDealChallenge.sln` in Visual Studio and build the solution.
  - Select `ProDealChallenge.WebApi` as the startup project and click run;
  - The client application should be available at **[https://localhost:7026/swagger](https://localhost:7026/swagger/index.html)**.
  - All interecations can be done using the swagger page.

## Database
- There is no need to setup a dabase instance for this application cause it's configured to run in AWS RDS.
- However, if you want to use your local instance you need replace to your configuration in **[appsettings.json](ProDealChallenge/ProDealChallenge.WebApi/appsettings.json)**.


## Challenge Details

### BackendEngineerTest A
#### Introduction
In this test you need to create a web API using the **[JSON:API](https://jsonapi.org/)** standard. It can be
developed on any language, framework and database as long as there is an explanation of
how to import the data and how to run the application. Extra points for candidates that are
able to implement in Elixir, Phoenix and PostgreSQL.

#### What you need to do
#### Import seed data
Implement code to import the CSV data provided below:


```
CSV
id,parent_id,item_name,priority
1,nil,heading 1,3
2,nil,heading 2,1
3,1,folder 1 1,4
4,1,folder 1 2,2
5,2,folder 2 1,2
6,2,folder 2 2,3
7,2,folder 2 3,5
8,6,subfolder 2 2 1,2
9,7,subfolder 2 3 1,1
10,7,subfolder 2 3 2,5
```

#### API Implementation
1. Expose the imported data via a single HTTP endpoint that will return all the rows. Add
an additional attribute named `path_name` that should follow the format: for a folder
with `id` 10 it should be `heading 2/folder 2 3/subfolder 2 3 2`.
2. Allow filtering items by `item_name`.
3. Allow sorting items by `priority`.
4. Allow paginating the response.
5. Cover the code with tests.
