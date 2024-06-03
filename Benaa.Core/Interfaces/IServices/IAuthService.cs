using Benaa.Core.Entities.DTOs;
using Benaa.Core.Entities.General;
using ErrorOr;

namespace Benaa.Core.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<ErrorOr<LoginResponseDto>> Login(LoginRequestDto applictionUser);
        Task<ErrorOr<User>> RegisterStudent(StudentRegisterDto newStudent);
		Task<ErrorOr<User>> RegisterAdmin(AdminRegesterDTO New);
		Task<ErrorOr<User>> RegisterTeacher(TeacherRegisterDto newTeacher);
    }
}
