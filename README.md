# .NET ACME Client
A dot net client for the ACME protocol

# Purpose
This repository is for interacting with an ACME server via .NET

# Overview of Projects

*DotNetAcmeClient.csproj*
A project specifically to have a run time and test the code. This may develop into an interactive client later.

*DotNetAcmeClient.Logic*
This project is where all the interaction with the server takes place

# Resources

*RFC 8555 - Automatic Certificate Management Environment (ACME)*
https://datatracker.ietf.org/doc/html/rfc8555#page-10

*RFC 7515 - JSON Web Signatures (JWS)*
https://datatracker.ietf.org/doc/html/rfc7515#section-3.3

*RFC 7518 - JSON Web Algorithms (JWA)*
https://datatracker.ietf.org/doc/html/rfc7518

*RFC 7638 - JSON Weky Key (JWK)*
https://datatracker.ietf.org/doc/html/rfc7638#section-3

*Let's Encrypt Staging Server*
https://acme-staging-v02.api.letsencrypt.org/directory

# Notes
- The Let's Encrypt site is not very helpful with documentation. The best bet is to read the RFC (ugh)
- Unsure of the Let's Encrypt production server