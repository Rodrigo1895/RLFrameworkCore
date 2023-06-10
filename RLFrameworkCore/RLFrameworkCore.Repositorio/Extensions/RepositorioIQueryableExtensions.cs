using Microsoft.EntityFrameworkCore;
using RLFrameworkCore.Dto.DTOs;
using RLFrameworkCore.Dto.DTOs.Interfaces;

namespace System.Linq
{
    public static class RepositorioIQueryableExtensions
    {
        public static async Task<IListDto<TDto>> ToListDtoAsync<TEntidade, TDto>(this IQueryable<TEntidade> query, IRequestAllDto dto, CancellationToken cancellationToken = default) where TEntidade : class
        {
            try
            {
                var itens = await PaginacaoAsync(query, dto.Pagina, dto.PaginaTamanho, cancellationToken);

                var retorno =  new ListDto<TDto>()
                {
                    TemProximo = itens.TemProximo,
                    PaginaAtual = itens.PaginaAtual,
                    PaginaTamanho = itens.PaginaTamanho,
                    TotalPaginas = itens.TotalPaginas,
                    TotalItens = itens.TotalItens,
                    Itens = itens.Itens.MapTo<IList<TDto>>()
                };

                return retorno;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Erro ao listar {typeof(TEntidade).Name} para {typeof(TDto).Name} - [RepositorioIQueryableExtensions.ToListDtoAsync]", ex);
            }
        }
        
        public static async Task<IListDto<T>> ToListDtoAsync<T>(this IQueryable<T> query, IRequestAllDto dto, CancellationToken cancellationToken = default)
        {
            try
            {
                return await PaginacaoAsync(query, dto.Pagina, dto.PaginaTamanho, cancellationToken);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException($"Erro ao listar {typeof(T).Name}  - [RepositorioIQueryableExtensions.ToListDtoAsync]", ex);
            }
        }

        private static async Task<IListDto<TEntidade>> PaginacaoAsync<TEntidade>(
            IQueryable<TEntidade> query,
            int pagina,
            int paginaTamanho,
            CancellationToken cancellationToken = default)
        {
            var paginacao = new ListDto<TEntidade>();

            pagina = (pagina < 0) ? 1 : pagina;

            paginacao.PaginaAtual = pagina;

            var totalItens = await query.CountAsync(cancellationToken);

            var inicio = (pagina - 1) * paginaTamanho;
            paginacao.Itens = await query
                       .Skip(inicio)
                       .Take(paginaTamanho)
                       .ToListAsync(cancellationToken);

            paginacao.TotalItens = totalItens;
            paginacao.TotalPaginas = (int)Math.Ceiling(paginacao.TotalItens / (double)paginaTamanho);
            paginacao.TemProximo = paginacao.TotalPaginas > paginacao.PaginaAtual;
            paginacao.PaginaTamanho = paginacao.Itens.Count;

            return paginacao;
        }
    }
}