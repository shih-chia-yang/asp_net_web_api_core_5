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
    public class UpdateStudentCommand:IRequest<bool>
    {
        [DataMember]
        [Required]
        public int Id{ get; set; }
        [DataMember]
        [Required]
        public string LastName { get; set; }

        [DataMember]
        [Required]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        public DateTime EnrollmentDate { get; set; }
    }
    
    public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, bool>
    {
        public readonly IStudentRepository _repo;
        public UpdateStudentCommandHandler(IStudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var updateItem = await _repo.FindAsync(request.Id);
            updateItem.EnrollmentDate=request.EnrollmentDate;
            updateItem.FirstName = request.FirstName;
            updateItem.LastName = request.LastName;
            _repo.Update(updateItem);
            return await _repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}