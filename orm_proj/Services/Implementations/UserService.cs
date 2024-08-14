using orm_proj.Models;
using orm_proj.Repositories.Interfaces;
using orm_proj.Services.Interfaces;

namespace orm_proj.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IOrderRepository _orderRepository;

        public UserService(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
        }

        public async Task RegisterUserAsync(UserPostDto newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.UserName) || string.IsNullOrWhiteSpace(newUser.Email) ||
                string.IsNullOrWhiteSpace(newUser.Password))
            {
                throw new InvalidUserInformationException("User information is incomplete.");
            }

            var user = new User
            {
                UserName = newUser.UserName,
                Email = newUser.Email,
                Password = newUser.Password,
                Address = newUser.Address
            };

            await _userRepository.CreateAsync(user);
        }

        public async Task<UserGetDto> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if (user == null || user.Password != password)
            {
                throw new UserAuthenticationException("Invalid email or password.");
            }

            UserGetDto userGet = new UserGetDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address
            };
            return userGet;
        }

        public async Task UpdateUserInfoAsync(int userId, UserPutDto userDto)
        {
            var user = await _userRepository.GetSingleAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new NotFoundException($"User with ID {userId} not found.");
            }

            user.UserName = userDto.UserName ?? user.UserName;
            user.Email = userDto.Email ?? user.Email;
            user.Address = userDto.Address ?? user.Address;

            _userRepository.Update(user);
        }

        public async Task ExportUserOrdersToExcelAsync(int userId, string filePath)
        {
            throw new NotImplementedException("Export to Excel is not implemented.");
        }
    }

}
