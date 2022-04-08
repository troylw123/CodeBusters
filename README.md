# CodeBusters

The Code Busters are a small team of attractive software developers who fix broken code for a fee. This Web API service will allow users to submit tickets to the Code Busters, who will then assess each ticket on an individual basis.

The **Code Busters API** is reliant upon the Ticket Entity. Once a user submits a ticket, the team of code busters will send back an assessment with estimated time to complete and cost of the fix, and a response containing the solution to the problems of the code.

This API contains 6 data tables, each with the 4 main CRUD operations, plus additional endpoints for added functionality. See the API Documention link in our *External Resources* section below for detailed information.

***We started a console application which currently includes the User table only.***

## Languages and Technologies Utilized
- C#
- .NET Core
- Visual Studio Code
- Azure Data Studio
- Postman
- N-Tier Architecture
- Microsoft Entity Framework
- Swagger

## How to Run the Project

Start by cloning our GitHub repository by entering the following code in your terminal:

`git clone https://github.com/troylw123/CodeBusters`

With your local server running, create the database with the following terminal command:

`dotnet ef database update -p CodeBusters.Data -s CodeBusters.WebAPI`

After successfully creating the database, run the project:

`dotnet run -p CodeBusters.WebAPI`

Finally, with both the local server and the API running, visit https://localhost:5001/swagger to begin using the CodeBusters API!

<hr>

### External Resources
- [Project Planning](https://docs.google.com/document/d/1_K83dHZoMieXm9c5TDeMUXnocI3-ocmpHuf5QcbbIY0/edit#)
- [Project Tickets](https://trello.com/b/QTFoHqiV/code-busters-final-project)
- [API Documentation](https://docs.google.com/document/d/1PUk3TnPHgGqLHHj9jfA_rBgED6gWcRLZCVb2C31eefI/edit?usp=sharing)

### Credits
- Troy Weaver       |   [Github](https://github.com/troylw123)   |   [Linkedin](https://www.linkedin.com/in/-troyweaver-/)
- Maria Harrell     |   [Github](https://github.com/mariaharrell13)   |   [Linkedin](https://www.linkedin.com/in/maria-harrell/)
- Hunter Browning   |   [Github](https://github.com/hunterjacobi)   |   [Linkedin](https://www.linkedin.com/in/hunter-browning-5b1215227/)   |
- Robin Hancock     |   [GitHub](https://github.com/Kahoona542)     |      
