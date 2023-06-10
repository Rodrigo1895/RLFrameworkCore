using RLFrameworkCore.Dto.DTOs.Configuracoes;
using RLFrameworkCore.Dto.DTOs.Interfaces;

namespace RLFrameworkCore.Dto.DTOs
{
    public class RequestAllDto : IRequestAllDto
    {
        public RequestAllDto()
        {
            _paginaTamanho = PaginacaoConfig.PaginaTamanhoPadrao;
            _pagina = 1;
        }

        public string Fields { get; set; }

        private int _pagina;
        public int Pagina
        {
            get => _pagina;
            set
            {
                _pagina = value;
                if (_pagina <= 0)
                    _pagina = 1;
            }
        }

        private int _paginaTamanho;
        public int PaginaTamanho
        {
            get => _paginaTamanho;
            set
            {
                _paginaTamanho = value;
                if (_paginaTamanho <= 0)
                    _paginaTamanho = PaginacaoConfig.PaginaTamanhoPadrao;
                if (_paginaTamanho > PaginacaoConfig.PaginaTamanhoMaximo)
                    _paginaTamanho = PaginacaoConfig.PaginaTamanhoMaximo;
            }
        }
    }
}
