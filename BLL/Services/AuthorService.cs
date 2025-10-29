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
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        private readonly IValidator<AuthorDTO> _authorValidator;

        public AuthorService(IAuthorRepository repository, IBookRepository bookRepository, IMapper mapper, IValidator<AuthorDTO> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _authorValidator = validator;
            _bookRepository = bookRepository;
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

        public async Task<IEnumerable<AuthorWithCountBooks>> GetAuthorsWithCountBookAsync(CancellationToken cancellationToken)
        {
            var authors = await _repository.GetAllAsync(cancellationToken);
            var books = await _bookRepository.GetAllAsync(cancellationToken);

            var authorDtos = authors.Select(author =>
            {
                var count = books.Count(b => b.AuthorId == author.Id);
                return new AuthorWithCountBooks
                {
                    Id = author.Id,
                    Name = author.Name,
                    DateOfBirth = author.DateOfBirth,
                    BookCount = count
                };
            });

            return authorDtos;
        }


        public async Task<AuthorDTO?> GetAuthorByNameAsync(string name, CancellationToken cancellationToken)
        {
            var author = await _repository
                .GetAllAsync(cancellationToken);

            var match = author
                .FirstOrDefault(a => a.Name.Contains(name, StringComparison.OrdinalIgnoreCase));

            if (match is null) return null;

            return new AuthorDTO
            {
                Id = match.Id,
                Name = match.Name,
                DateOfBirth = match.DateOfBirth,
            };
        }
    }
}
