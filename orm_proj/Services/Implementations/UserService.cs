using orm_proj.Models;
using orm_proj.Repositories.Implementations;
using orm_proj.Repositories.Interfaces;
using orm_proj.Services.Interfaces;
using System.Security.Authentication;
using System.Text.RegularExpressions;

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
                Address = newUser.Address,
                IsAdmin = false,
            };

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<UserGetDto> LoginUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);

            if (user == null || user.Password != password)
            {
                throw new UserAuthenticationException("Invalid email or password.");
            }

            UserGetDto _currentUser = new UserGetDto()
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
                IsAdmin = user.IsAdmin,
            };
            return _currentUser;
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


        public async Task RegisterAdminAsync(UserPostDto newAdmin)
        {

            if (string.IsNullOrWhiteSpace(newAdmin.UserName) || string.IsNullOrWhiteSpace(newAdmin.Email) ||
                string.IsNullOrWhiteSpace(newAdmin.Password))
            {
                throw new InvalidUserInformationException("User information is incomplete.");
            }



            var user = new User
            {
                UserName = newAdmin.UserName,
                Email = newAdmin.Email,
                Password = newAdmin.Password,
                Address = newAdmin.Address,
                IsAdmin = true,
            };

            await _userRepository.CreateAsync(user);
            await _userRepository.SaveChangesAsync();
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }
        public async Task<List<UserGetDto>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();

            List<UserGetDto> result = new List<UserGetDto>();

            users.ForEach(user =>
            {
                UserGetDto userGet = new UserGetDto()
                {
                   Id = user.Id,
                   UserName = user.UserName,
                   Email = user.Email,
                   Address  = user.Address,
                   IsAdmin = user.IsAdmin,
                };

                result.Add(userGet);
            });

            return result;
        }


        //public bool CheckPassword(string password)
        //{
        //    if (string.IsNullOrWhiteSpace(password))
        //        throw new InvalidCredentialsException("Password cannot be empty.");

        //    bool hasUpper = false;
        //    bool hasLower = false;
        //    bool hasDigit = false;

        //    foreach (char c in password)
        //    {
        //        if (char.IsUpper(c))
        //            hasUpper = true;
        //        else if (char.IsLower(c))
        //            hasLower = true;
        //        else if (char.IsDigit(c))
        //            hasDigit = true;
        //    }


        //    if (!hasUpper || !hasLower || !hasDigit || password.Length < 8)
        //        throw new InvalidCredentialsException("Password should contain at least 8 characters, including upper and lower case letters, and at least one digit.");

        //    return true;

        //}


        //public bool CheckUsername(string username)
        //{
        //    if (string.IsNullOrWhiteSpace(username))
        //        throw new InvalidCredentialsException("Fullname cannot be empty.");

        //    if (!char.IsUpper(username[0]))
        //        throw new InvalidCredentialsException("First letter should be an uppercase letter.");

        //    return true;
        //}

        //public bool CheckEmail(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //        throw new InvalidCredentialsException("Email cannot be null.");

        //    string pattern = @"^(([^<>()[\]\\.,;:\s@\""]+(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$";
        //    Regex regex = new Regex(pattern);
        //    if (!regex.IsMatch(email))
        //    {
        //        throw new InvalidCredentialsException($"The email address '{email}' is not in a valid format.");
        //    }
        //    return true;
        //}
    }
}

