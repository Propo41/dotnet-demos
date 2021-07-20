# dotnet-demos
a demo project illustrating the basics of authentication with mongoDB using .net as a web api

Resources:

- https://medium.com/nerd-for-tech/net-jwt-authentication-with-mongodb-9bca4a33d3f0 
- https://www.c-sharpcorner.com/article/authentication-and-authorization-in-asp-net-core-web-api-with-json-web-tokens/

### Description

- Uses JWT tokens to establish an authorization
- When user logs in, a jwt token is generated
- this token needs to be passed from the client side at all times to access private APIs.


### Packages used:
To install a package using the dotnet CLI:  `dotnet add package <package-name>`
- `MongoDB.Driver`
- `Microsoft.AspNetCore.Authentication`
- `Microsoft.AspNetCore.Authentication.JwtBearer`
- `System.IdentityModel.Tokens.Jwt`

### To run:
- clone the repo
- open a terminal and type: `dotnet watch run` or `dotnet run`
- open postman and create a user first using the **POST** method at the endpoint: `http://localhost5000/api/user/create-user`
- now, go to `http://localhost5000/api/user/authenticate`, and enter the email and password of the user you just created and send a **POST** request
- You will receive a token as a response. Copy the token
- Now, go to `http://localhost5000/api/user/` and create a **GET** request. Now select the Authorization tab and choose *Bearer Token* and paste the token
- Send the request and you will be authenticated. The token is set to expire in 1 minute.

![image](https://user-images.githubusercontent.com/46298019/126398651-e205277b-0a34-4690-a46c-e13d3497fe23.png)

### NOTE
- there are 2 models: Pizza and User. 
- The Pizza model has no authorization but the User model requires authorization
