# dotnet-demos
A demo project illustrating the basics of uploading files to a cloud bucket. 
I used <a href="https://uploadcare.com/">upload care</a> as the storage bucket.

### Uploading files
There are 2 ways to upload. 
1. directly from the client side using a public key
2. generating a signature key (that consists of a secret key with the HMAC/SHA256 code applied and an expiry time) and sending this key to the client. This key will then be used in the form-data fields for authorizations. Check <a href="https://uploadcare.com/docs/security/secure-uploads/">here</a>

I used method 2 which is more secure for the website I'm intending to use on.
The signature key is to be passed like the following:




References:
- https://uploadcare.com/docs/security/secure-uploads/

### Deleting files
Again, there are 2 ways:
1. By using the secret key directly and sending the uuid of the file in DELETE request
2. By generating a signature along with several other headers.

References:
- https://uploadcare.com/api-refs/rest-api/v0.5.0/#operation/deleteFileStorage 
- https://uploadcare.com/api-refs/rest-api/v0.5.0/#section/Authentication 
