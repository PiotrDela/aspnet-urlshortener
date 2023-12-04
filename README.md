# aspnet-urlshortener
An example of simple URL shortener API written in ASP .NET Core 8, utilizing CQRS with MediatR package.
- Included unit tests for Base36Encoding class and integration tests for ApiController
- In memory cache is used as storage
- Id sequence is stored in memory as well
- Solution doesn't check for duplicates