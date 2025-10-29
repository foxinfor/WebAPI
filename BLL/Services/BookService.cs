using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL.Interfaces;
using DAL.Models;
using FluentValidation;

namespace BLL.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _repository;
        private readonly IMapper _mapper;

        private readonly IValidator<BookDTO> _bookValidator;

        public BookService(IBookRepository repository,  IMapper mapper, IValidator<BookDTO> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _bookValidator = validator;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync(CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }

        public async Task<BookDTO> GetByIdAsync(int id,CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(id,cancellationToken);
            return _mapper.Map<BookDTO>(book);
        }

        public async Task<BookDTO> AddAsync(BookDTO dto, CancellationToken cancellationToken)
        {
            var validationResult = await _bookValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var book = _mapper.Map<Book>(dto);
            //book.Id = Guid.NewGuid();

            var result = await _repository.CreateAsync(book, cancellationToken);
            return _mapper.Map<BookDTO>(result);
        }


        public async Task<BookDTO> UpdateAsync(BookDTO dto, CancellationToken cancellationToken)
        {
            var validationResult = await _bookValidator.ValidateAsync(dto, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var book = _mapper.Map<Book>(dto);
            book.Id = dto.Id;

            var result = await _repository.UpdateAsync(book, cancellationToken);
            return _mapper.Map<BookDTO>(result);
        }


        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var book = await _repository.GetByIdAsync(id,cancellationToken);

            await _repository.DeleteAsync(book,cancellationToken);
        }

        public async Task<IEnumerable<BookDTO>> GetBooksAfterYearAsync(int year, CancellationToken cancellationToken)
        {
            var books = await _repository.GetAllAsync(cancellationToken);

            var filtered = books
                .Where(b => b.PublishedYear > year);

            return _mapper.Map<IEnumerable<BookDTO>>(filtered);
        }
    }
}
