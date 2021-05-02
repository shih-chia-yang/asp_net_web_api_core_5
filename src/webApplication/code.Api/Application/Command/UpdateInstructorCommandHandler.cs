using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using code.Domain.Event;
using code.Domain.Repositories;

namespace code.Api.Application.Command
{
    [DataContract]
    public class UpdateInstructorCommand : IRequest<bool>
    {
        [DataMember]
        [Required]
        public int Id { get; set; }
        [DataMember]
        [Required]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        public string FirstName { get; set; }
        [DataMember]
        [Required]
        public DateTime HireDate { get; set; }

        public UpdateInstructorCommand()
        {
            
        }
        public UpdateInstructorCommand(int id,string lastName, string firstName, DateTime hireDate)
        {
            Id = id;
            this.LastName = lastName;
            this.FirstName = firstName;
            this.HireDate = hireDate;

        }
    }

    public class UpdateInstructorCommandHandler : IRequestHandler<UpdateInstructorCommand, bool>
    {
        private readonly IInstructorRepository _repo;
        public UpdateInstructorCommandHandler(IInstructorRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(UpdateInstructorCommand request, CancellationToken cancellationToken)
        {
            var updateItem = await _repo.FindAsync(request.Id);
            if (updateItem ==null)
                return false;
            updateItem.LastName = request.LastName;
            updateItem.FirstName = request.FirstName;
            updateItem.HireDate = request.HireDate;
            _repo.Update(updateItem);
            return await _repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}