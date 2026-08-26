using ErrorOr;
using SmartTodo.Domain.Common.Interfaces;

namespace SmartTodo.Domain.Users;

public class User : Entity
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public Guid? AdminId { get; private set; }
    public Guid? NormalUserId { get; private set; }

    private readonly string _passwordHash = null!;

    public User(
        string firstName,
        string lastName,
        string email,
        string passwordHash,
        Guid? adminId = null,
        Guid? normalUserId = null,
        Guid? id = null)
            : base(id ?? Guid.NewGuid())
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        AdminId = adminId;
        NormalUserId = normalUserId;
        _passwordHash = passwordHash;
    }

    public bool IsCorrectPasswordHash(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.IsCorrectPassword(password, _passwordHash);
    }

    public ErrorOr<Guid> CreateAdminProfile()
    {
        if (AdminId is not null)
        {
            return Error.Conflict(description: "User already has an admin profile");
        }

        AdminId = Guid.NewGuid();

        return AdminId.Value;
    }

    public ErrorOr<Guid> CreateNormalProfile()
    {
        if (NormalUserId is not null)
        {
            return Error.Conflict(description: "User already has a Normal profile");
        }

        NormalUserId = Guid.NewGuid();

        return NormalUserId.Value;
    }

    public List<ProfileType> GetProfileTypes()
    {
        List<ProfileType> profileTypes = new();

        if (AdminId is not null)
        {
            profileTypes.Add(ProfileType.Admin);
        }

        if (NormalUserId is not null)
        {
            profileTypes.Add(ProfileType.NormalUser);
        }


        return profileTypes;
    }

    private User() { }
}