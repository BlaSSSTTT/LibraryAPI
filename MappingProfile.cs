using AutoMapper;
using LibrariAPI.ViewModels;
using Models;

namespace LibrariAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Мапінг Book -> BookViewModel
            CreateMap<Book, BookViewModel>();

            // Мапінг Loan -> LoanViewModel
            CreateMap<Loan, LoanViewModel>();

            // Мапінг Reader -> ReaderViewModel
            CreateMap<Reader, ReaderViewModel>();
        }
    }
}
