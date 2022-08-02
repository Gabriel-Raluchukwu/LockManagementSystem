# Lock Management System Documentation

## Setup

### Database
> Docker is required to run a Postgres locally.

` docker run --name postgressql -e POSTGRES_USER=root -e POSTGRES_PASSWORD=rootpassword -p 2022:5432 -d postgres `

Database Name : lockmanagementsystem

|Database setting               |Value                        |
|-------------------------------|-----------------------------|
|Database name                  |` lockmanagementsystem `     |
|Host                           |` localhost`                 |
|Username                       |` root `					  |
|Password                       |` rootpassword `	 		  |
|Port                           |` 2022 `					  |

### Migrations
Migrations are configured to run automatically when project is built.
To apply migration manually, cd to infrastructure folder and then run:
` dotnet ef database update --context LockManagementWriteContext --startup-project ../LockManagementSystem.Api `

To add migration, run:
` dotnet ef migrations add MigrationName --context LockManagementWriteContext --startup-project ../LockManagementSystem.Api `

## Default Settings
User email: default.user@clay.com
Password: Login@1234



## Flow chart

``` mermaid
graph LR
A[Office] -- Has ----> B[Employees]
A -- Has ----> C[Roles]
A -- Has ----> D[Locks for doors]
C ----> E[Employee Roles]
C ----> F[Lock Roles]
```

An **Office** (branch/building) is the base unit.
Every Employee, Lock and Role belong to a particular office.
> Every employee signed up will have a default role of 'employee'.
> Additional roles can be added to an employee using the employee Id and the role Id.

