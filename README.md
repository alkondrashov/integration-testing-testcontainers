# Cars

Cars project is a simple REST API project to expose one endpoint. Cars.Tests project contains an integration test for the REST API.

## Environment variables
```
export CONNECTION_STRING="Host=localhost;Port=3306;Database=carsdb;Uid=root;Pwd=password;SslMode=None"
```

### Run

```
>> curl --request POST 'http://localhost:5000/api/cars' --header 'Content-Type: application/json' --data '{\"name\": \"bmw\", \"available\" : true}'

{"id":"1",name":"bmw","available":false}
```

# Run integration tests

```
>> dotnet test

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.
Passed!  - Failed:     0, Passed:     1, Skipped:     0, Total:     1, Duration: < 1 ms - Cars.Tests.dll (net6.0)
```