namespace LifeCenter.Services;

using AutoMapper;
using BCrypt.Net;
using LifeCenter.Entities;
using LifeCenter.Helpers;
using LifeCenter.Models.Personal;
using LifeCenter.Repositories;

public interface IPersonalService
{
    Task<IEnumerable<Personal>> GetAll();
    Task<Personal> GetById(int id);
    Task Create(CreateRequest model);
    Task Update(int id, UpdateRequest model);
    Task Delete(int id);
}

public class PersonalService : IPersonalService
{
    private IPersonalRepository _personalRepository;
    private readonly IMapper _mapper;

    public PersonalService(
        IPersonalRepository personalRepository,
        IMapper mapper)
    {
        _personalRepository = personalRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<Personal>> GetAll()
    {
        return await _personalRepository.GetAll();
    }

    public async Task<Personal> GetById(int id)
    {
        var personal = await _personalRepository.GetById(id);

        if (personal == null)
            throw new KeyNotFoundException("Personal not found");

        return personal;
    }

    public async Task Create(CreateRequest model)
    {
        // validate
        if (await _personalRepository.GetByName(model.Name!) != null)
            throw new AppException("Personal with the Name '" + model.Name + "' already exists");

        // map model to new personal object
        var personal = _mapper.Map<Personal>(model);

        // hash password
        //personal.PasswordHash = BCrypt.HashPassword(model.Password);

        // save personal
        await _personalRepository.Create(personal);
    }

    public async Task Update(int id, UpdateRequest model)
    {
        var personal = await _personalRepository.GetById(id);

        if (personal == null)
            throw new KeyNotFoundException("Personal not found");

        // validate
        var nameChanged = !string.IsNullOrEmpty(model.Name) && personal.Name != model.Name;
        if (nameChanged && await _personalRepository.GetByName(model.Name!) != null)
            throw new AppException("Personal with the name '" + model.Name + "' already exists");

        // hash password if it was entered
        //if (!string.IsNullOrEmpty(model.Password))
            //personal.PasswordHash = BCrypt.HashPassword(model.Password);

        // copy model props to personal
        _mapper.Map(model, personal);

        // save personal
        await _personalRepository.Update(personal);
    }

    public async Task Delete(int id)
    {
        await _personalRepository.Delete(id);
    }
}