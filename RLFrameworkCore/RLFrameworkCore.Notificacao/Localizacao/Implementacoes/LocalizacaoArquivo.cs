using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using RLFrameworkCore.Notificacao.Localizacao.Exceptions;
using RLFrameworkCore.Notificacao.Localizacao.Interfaces;

namespace RLFrameworkCore.Notificacao.Localizacao.Implementacoes
{
    internal class LocalizacaoArquivo : ILocalizacaoArquivo
    {
        public string Nome { get; }
        public Assembly AssemblyArquivo { get; }
        public string Local { get; }
        private IMemoryCache _memoryCache { get; }

        public LocalizacaoArquivo(string nome, Assembly assembly, string local, IMemoryCache memoryCache)
        {
            Nome = nome;
            Local = local;
            AssemblyArquivo = assembly;
            _memoryCache = memoryCache;
        }

        public string GetString(string nome, CultureInfo idioma)
        {
            string campo;

            try
            {
                using (JsonDocument jsonDoc = GetJson(idioma))
                {
                    JsonElement json = jsonDoc.RootElement;
                    json = json.GetProperty("texts");
                    campo = json.GetProperty(nome).GetString();
                    return campo;
                }
            }
            catch (Exception ex)
            {

                if (ex is KeyNotFoundException || ex is ArquivoNaoEncontradoException)
                {
                    campo = $"[{Regex.Replace(Regex.Replace(nome, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2")}]";
                    return campo;
                }

                throw;
            }
        }

        private JsonDocument GetJson(CultureInfo idioma)
        {
            bool isExist = _memoryCache.TryGetValue($"{Local}.{Nome}-{idioma?.Name}.json", out string jsonString);
            if (!isExist)
            {
                jsonString = Inicializa(idioma);
            }

            return JsonDocument.Parse(jsonString);
        }

        private string Inicializa(CultureInfo idioma)
        {
            string jsonNome = $"{Local}.{Nome}-{idioma?.Name}.json";
            string jsonString;
            using (Stream stream = AssemblyArquivo.GetManifestResourceStream(jsonNome))
            {
                if (stream == null)
                    throw new ArquivoNaoEncontradoException($"Arquivo de recurso não encontrado: {jsonNome}");
                using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                {
                    jsonString = reader.ReadToEnd();
                }
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            _memoryCache.Set(jsonNome, jsonString, cacheEntryOptions);

            return jsonString;
        }
    }
}