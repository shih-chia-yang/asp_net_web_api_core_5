using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using code.Domain.Event;
using code.Domain.Repositories;

namespace code.Api.Application.Command
{
    [DataContract]
    public class DeleteInstructorCommand:IRequest<bool>
    {
        [DataMember]
        [Required]
        public int InstructorId { get; set; }

        public DeleteInstructorCommand()
        {
            
        }

        public DeleteInstructorCommand(int id)
        {
            InstructorId = id;
        }
    }

    public class DeleteInstructorCommandHandler : IRequestHandler<DeleteInstructorCommand, bool>
    {
        private readonly IInstructorRepository _repo;
        public DeleteInstructorCommandHandler(IInstructorRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteInstructorCommand request, CancellationToken cancellationToken)
        {
            var deleteItem = await _repo.FindAsync(request.InstructorId);
            if(deleteItem==null)
                return false;
            _repo.Delete(deleteItem);
            return await _repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}