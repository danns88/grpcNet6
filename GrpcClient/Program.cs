
// See https://aka.ms/new-console-template for more information
using Grpc.Core;
using Grpc.Net.Client;
using GrpcServer;


//var input = new HelloRequest { Name = "wahlao eh" };
//var channel = GrpcChannel.ForAddress("https://localhost:7202/");
//var client = new Greeter.GreeterClient(channel);
//var reply = await client.SayHelloAsync(input);
//Console.WriteLine(reply.Message);

var channel = GrpcChannel.ForAddress("https://localhost:7202/");
var client = new Person.PersonClient(channel);

var personRequest = new PersonLookupModel { UserId = 11 };

var person = await client.GetPersonInfoAsync(personRequest);
Console.WriteLine($"{person.FirstName}{Environment.NewLine}{person.LastName}{Environment.NewLine}{person.Age}{Environment.NewLine}{person.Email}");

Console.WriteLine();
Console.WriteLine("Get Newly Added Person with stream");

using (var call = client.GetNewPerson(new NewPersonRequest()))
{
    while (await call.ResponseStream.MoveNext())
    {
        var currentPerson = call.ResponseStream.Current;
        Console.WriteLine($"{currentPerson.FirstName}{Environment.NewLine}{currentPerson.LastName}{Environment.NewLine}{currentPerson.Age}{Environment.NewLine}{currentPerson.Email}");
        Console.WriteLine();
    }
}

Console.ReadLine();