# OTP Verify using Cryptography and without any Database

### OTP Verification process

- A hash(server end) is created with the phone number and then sent to the user as a OTP token.
- The user received the OTP via SMS.
- The user sends back the OTP token, OTP and phone number used in the first http request.
- The server verify the request and returns the response verified or not.

### Diagram

![OTP verification process](https://user-images.githubusercontent.com/22918946/220368325-71a3dba0-904d-46d1-a514-92ac4e76afcc.png)
