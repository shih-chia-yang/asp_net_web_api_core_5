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
    public class CreateStudentCommand: IRequest<bool>
    {
        [DataMember]
        [Required]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        public DateTime EnrollmentDate { get; set; }

        public CreateStudentCommand(string lastName,string firstName,DateTime enrollmentDate)
        {
            LastName = lastName;
            FirstName = firstName;
            EnrollmentDate = enrollmentDate;
        }
    }
    
    public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, bool>
    {
        private readonly IStudentRepository _repo;

        public CreateStudentCommandHandler(IStudentRepository repo)
        {
            _repo = repo;
        }
        public Task<bool> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
        {
            var addNew = Student.CreateNew(request.LastName, request.FirstName, request.EnrollmentDate);
            _repo.Add(addNew);
            return  _repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}