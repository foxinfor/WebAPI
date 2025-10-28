using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using FluentValidation;

namespace BLL.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        private readonly IValidator<AuthorDTO> _authorValidator;

        public AuthorService(IAuthorRepository repository, IMapper mapper, IValidator<AuthorDTO> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _authorValidator = validator;
        }


        public async Task<IEnumerable<AuthorDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var authors = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<AuthorDTO>>(authors);
        }

        public async Task<AuthorDTO?> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var author = await _repository.GetByIdAsync(id, cancellationToken) ??
                     throw new KeyNotFoundException("Author not found");

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<AuthorDTO> AddAsync(AuthorDTO dto, CancellationToken cancellationToken)
        {
            var validationResult = await _authorValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var author = _mapper.Map<Author>(dto);
            //author.Id = Guid.NewGuid();

            var result = await _repository.CreateAsync(author, cancellationToken);
            return _mapper.Map<AuthorDTO>(result);
        }


        public async Task<AuthorDTO> UpdateAsync(AuthorDTO dto, CancellationToken cancellationToken)
        {
            var validationResult = await _authorValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var author = _mapper.Map<Author>(dto);
            author.Id = dto.Id;

            var result = await _repository.UpdateAsync(author, cancellationToken);
            return _mapper.Map<AuthorDTO>(result);
        }


        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var author = await _repository.GetByIdAsync(id, cancellationToken);
            await _repository.DeleteAsync(author,cancellationToken);
        }
    }
}
