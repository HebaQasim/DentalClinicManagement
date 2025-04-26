using AutoMapper;
using DentalClinicManagement.DomainLayer.Interfaces.IAuth;
using DentalClinicManagement.DomainLayer.Interfaces.IRepository;
using MediatR;

namespace DentalClinicManagement.ApplicationLayer.Common.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepository;

        public LoginCommandHandler(
         IAuthRepository authRepository,
         IJwtTokenGenerator jwtTokenGenerator,
         IMapper mapper)
        {
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            _authRepository = authRepository;
        }

        public async Task<LoginResponse> Handle(
            LoginCommand request, CancellationToken cancellationToken = default)
        {

            var authResult = await _authRepository.AuthenticateAsync(request.Email, request.Password, cancellationToken)

                             ?? throw new UnauthorizedAccessException("Provided Credentials are not valid");


            //var (user, role) = authResult;
            var user = authResult.User;
           // var role = authResult.Role;

            // Token generation based on role
            var token = _jwtTokenGenerator.Generate(user);


            return _mapper.Map<LoginResponse>(token);
        }
    }

}
