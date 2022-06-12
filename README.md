<!-- ABOUT THE PROJECT -->
## About The Project

This project was created as a part of recruitment process for software company: https://www.ideo.pl/
The goal was to create an application allowing one to browse and manage Tree-like structure,
similar to that of filesystem catalogues. Implemented functionalities:

- [x] Browsing the tree
    - [x] Collapsing and expanding individual nodes
    - [x] Collapsing and expanding entire structure
- [x] Adding new nodes
- [x] Editing existing nodes
    - [x] Changing name of a node
    - [x] Moving node within the structure
- [x] Deleting nodes
- [x] Sorting nodes (different options)
- [x] Data validation
    - [x] Loop prevention
    - [x] Non-text data prevention
    - [x] Maximum length limit
- [x] Example data auto-seed


### Built With

* [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet)
* [PostgreSQL](https://www.postgresql.org/)


<!-- GETTING STARTED -->
## Getting Started

### Prerequisites

* nuget
  ```sh
  dotnet add package Npgsql --version 6.0.4
  ```
  
* PostgreSQL
  ```sh
    Database name: treeStruct
    Username: treeUser
    Password: TotallySavePasswordToPutOnRepository
    URL: localhost:5432
  ```
  You can also tweak these settings in `appsettings.json` file.

### Installation


1. Clone the repo
   ```sh
   git clone https://github.com/MatiF100/TreeStruct
   ```

2. (optional) Configure `appsettings.json` accordingly to your setup   

3. Apply configuration to the database
    ```sh
      dotnet ef database update
    ```
4. Compile the program accordingly to your setup