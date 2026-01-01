## OTP Verify using Cryptography and without any Database

### Dependencies
This verification depends on the [HMCSHA256]. Computes a Hash-based Message Authentication Code (HMAC) by using the SHA256 hash function. See [Details]

### OTP Verification process

- A hash(server end) is created with the phone number and then sent to the user as a OTP token.
- The user received the OTP via SMS.
- The user sends back the OTP token, OTP and phone number used in the first http request.
- The server verify the request and returns the response verified or not.

### Diagram

![OTP verification process](https://user-images.githubusercontent.com/22918946/220368325-71a3dba0-904d-46d1-a514-92ac4e76afcc.png)

## Run the project

### Step 1: Prerequisites

 * Please make sure you have installed .NET 6 SDK or Above. If not download [here]. 

#### Step 2: Download or clone the repository

```sh
https://github.com/sodipto/.net-core-otp-verify-without-database.git
```

## Version Information

```sh
C# 10.0
.NET 6.0
```

 [HMCSHA256]: <https://learn.microsoft.com/en-us/dotnet/api/system.security.cryptography.hmacsha256?view=net-7.0>
 [here]: <https://dotnet.microsoft.com/en-us/download/dotnet/6.0>
 [Details]:<https://en.wikipedia.org/wiki/HMAC>
