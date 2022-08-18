using Grpc.Core;

namespace GrpcServer.Services
{
    public class PersonService : Person.PersonBase
    {
        private readonly ILogger<PersonService> _logger;

        public PersonService(ILogger<PersonService> logger)
        {
            _logger = logger;
        }

        public override Task<PersonModel> GetPersonInfo(PersonLookupModel request, ServerCallContext context)
        {
            PersonModel model = new PersonModel()
            {
                FirstName = $"Name {request.UserId}",
                Age = request.UserId,
                Email = $"Email {request.UserId}",
                LastName = $"LastName {request.UserId}",
            };

            return Task.FromResult(model);
        }

        public override async Task GetNewPerson(NewPersonRequest request, IServerStreamWriter<PersonModel> responseStream, ServerCallContext context)
        {
            List<PersonModel> persons = new List<PersonModel>
            {
                new PersonModel()
                {
                    FirstName = "danns1",
                    Age = 1,
                    Email = "danns1@email.com",
                    LastName = "danns1",
                },
                new PersonModel()
                {
                    FirstName = "danns2",
                    Age = 21,
                    Email = "danns2@email.com",
                    LastName = "danns2",
                },
                new PersonModel()
                {
                    FirstName = "danns3",
                    Age = 31,
                    Email = "danns3@email.com",
                    LastName = "danns3",
                }
            };
            
            foreach(var person in persons)
            {
                await Task.Delay(2000);
                await responseStream.WriteAsync(person);
            }
        }
    }
}
