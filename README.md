# dotnet-demos
A demo showing the usage of a custom jwt middleware

There are 3 endpoints here:
1. `GET /token`
2. `GET /token/verify/{token}`
3. `GET /token/private`

#### `GET /token`
Generates a jwt token containing 2 claim identities (id and email) and an expiry date of 30mins

#### `GET /token/verify/{token}`
Copy paste the token you received earlier and paste it in the url. This will verify and return the ID contained in the JWT token

#### `GET /token/private`
Paste the token in the authorization header as AUTH BEARER and trigger the endpoint. If the token is valid, and if the user ID contained in the token is valid, then this endpoint can be accessed
