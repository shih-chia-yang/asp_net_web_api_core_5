using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Threading;
using System.Threading.Tasks;
using code.Domain.Event;
using code.Domain.Repositories;

namespace code.Api.Application.Command
{
    [DataContract]
    public class DeleteStudentCommand:IRequest<bool>
    {
        [DataMember]
        [Required]
        public int StudentId { get; set; }
    }
    public class DeleteStudentCommandHandler : IRequestHandler<DeleteStudentCommand, bool>
    {
        private readonly IStudentRepository _repo;
        public DeleteStudentCommandHandler(IStudentRepository repo)
        {
            _repo = repo;
        }
        public async Task<bool> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var deleteItem = await _repo.FindAsync(request.StudentId);
            if(deleteItem==null)
                return false;
            _repo.Delete(deleteItem);
            return await _repo.UnitOfWork.SaveEntitiesAsync();
        }
    }
}