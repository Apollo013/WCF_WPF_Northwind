# WCF_WPF_Northwind
A WCF app providing a simple demonstration on how to connect to a database to perform CRUD operations. Contains a WPF Client.

---

Developed using VS2015 Community. Uses the Product table from the Northwind database (not included)

---

####Project Layout

| Name | Type | Description|
| ---- | ------ | -------- |
| DistNorthwindWPF | WPF | Client app used to consume our service |
| LINQNorthwind.Models | Class Library | Contains DTO & BDO models |
| LinqNorthwindDAL | Class Library | Handles communication with the DB |
| LINQNorthwindLogic | Class Library | Handles communication between Service and DAL |
| LINQNorthwindService | Class Library | The WCF Service |

---

####Resources
| Title | Author | Publisher |
| --------- | --------- | ------- |
| WCF 4.5 Multi-Layer Services Development with Entity Framework | Mike Liu | PACKT |
| [Windows Communication Foundation](https://msdn.microsoft.com/en-us/library/dd456779(v=vs.110).aspx)| | MSDN |
