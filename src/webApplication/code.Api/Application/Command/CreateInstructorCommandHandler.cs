using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using code.Domain.Entities;
using code.Domain.Event;
using code.Domain.Repositories;

namespace code.Api.Application.Command
{
    [DataContract]
    public class CreateInstructorCommand : IRequest<bool>
    {
        [DataMember]
        [Required]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        public DateTime HireDate { get; set; }

        public CreateInstructorCommand()
        {
            
        }

        public CreateInstructorCommand(string lastName, string firstName, DateTime hireDate)
        {
            this.LastName = lastName;
            this.FirstName = firstName;
            this.HireDate = hireDate;
        }
    }
    public class CreateInstructorCommandHandler : IRequestHandler<CreateInstructorCommand, bool>
    {

        private readonly IInstructorRepository _repo;
        public CreateInstructorCommandHandler(IInstructorRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(CreateInstructorCommand request, CancellationToken cancellationToken)
        {
            var addNew = Instructor.CreateNew(request.LastName, request.FirstName, request.HireDate);
            await _repo.AddAsync(addNew);
            return await _repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}