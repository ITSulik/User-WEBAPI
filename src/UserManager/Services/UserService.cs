using UserManager.Models;

namespace UserManager.Services;

public class UserService
{
    private readonly List<User> _users = new();
    private int _nextId = 1;

    public List<User> GetAll() => _users;

    public User? GetById(int id) => _users.FirstOrDefault(u => u.Id == id);

    public void Create(User user)
    {
        user.Id = _nextId++;
        _users.Add(user);
    }

    public bool Update(int id, User updatedUser)
    {
        var existing = GetById(id);
        if (existing is null) return false;

        existing.Name = updatedUser.Name;
        existing.Email = updatedUser.Email;
        return true;
    }

    public bool Delete(int id)
    {
        var user = GetById(id);
        return user is not null && _users.Remove(user);
    }
}
