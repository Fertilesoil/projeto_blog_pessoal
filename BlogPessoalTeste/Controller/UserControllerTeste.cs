using blogpessoal.Model;
using BlogPessoalTeste.Factory;
using FluentAssertions;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using Xunit.Extensions.Ordering;

namespace BlogPessoalTeste.Controller
{
    public class UserControllerTeste : IClassFixture<WebAppFactory>
    {
        protected readonly WebAppFactory _factory;
        protected HttpClient _client;
        private readonly dynamic token;

        private string Id { get; set; } = string.Empty;
        public UserControllerTeste(WebAppFactory factory)
        {
            _factory = factory;
            _client = factory.CreateClient();

            token = GetToken();
        }

        private static dynamic GetToken()
        {
            dynamic data = new ExpandoObject();
            data.sub = "root@root.com";
            return data;
        }

        [Fact, Order(1)]

        public async Task DeveCriarUmUsuario()
        {
            var NovoUsuario = new Dictionary<string, string>()
            {
                { "nome","Clarício Balafina"},
                { "usuario","claricio@email.com"},
                { "senha","123456789"},
                { "foto",""}
            };

            var UsuarioJson = JsonConvert.SerializeObject(NovoUsuario);
            var CorpoRequisicao = new StringContent(UsuarioJson, Encoding.UTF8, "application/json");

            var Resposta = await _client.PostAsync("/usuarios/cadastrar", CorpoRequisicao);

            Resposta.EnsureSuccessStatusCode();

            Resposta.StatusCode.Should().Be(HttpStatusCode.Created);
        }

        [Fact, Order(2)]

        public async Task DeveDarErroUsuario()
        {
            var NovoUsuario = new Dictionary<string, string>()
            {
                { "nome","Clarício Balafina"},
                { "usuario","claricioemail.com"},
                { "senha","123456789"},
                { "foto",""}
            };

            var UsuarioJson = JsonConvert.SerializeObject(NovoUsuario);
            var CorpoRequisicao = new StringContent(UsuarioJson, Encoding.UTF8, "application/json");

            var Resposta = await _client.PostAsync("/usuarios/cadastrar", CorpoRequisicao);

            //Resposta.EnsureSuccessStatusCode();

            Resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, Order(3)]
        public async Task DeveBarrarMultiplosUsuarios()
        {
            var NovoUsuario = new Dictionary<string, string>()
            {
                { "nome","Brigite Bira"},
                { "usuario","bira@email.com"},
                { "senha","123456789"},
                { "foto",""}
            };

            var UsuarioJson = JsonConvert.SerializeObject(NovoUsuario);
            var CorpoRequisicao = new StringContent(UsuarioJson, Encoding.UTF8, "application/json");

            await _client.PostAsync("/usuarios/cadastrar", CorpoRequisicao);

            var Resposta = await _client.PostAsync("/usuarios/cadastrar", CorpoRequisicao);

            //Resposta.EnsureSuccessStatusCode();

            Resposta.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact, Order(4)]
        public async Task DeveListarTodosUsuarios()
        {
            _client.SetFakeBearerToken((object)token);

            var Resposta = await _client.GetAsync("/usuarios/all");

            Resposta.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Order(5)]

        public async Task DeveAtualizarUsuario()
        {
            var NovoUsuario = new Dictionary<string, string>()
            {
                { "nome","Banela Balafina"},
                { "usuario","banela@email.com"},
                { "senha","123456789"},
                { "foto",""}
            };

            var UsuarioJson = JsonConvert.SerializeObject(NovoUsuario);
            var CorpoRequisicao = new StringContent(UsuarioJson, Encoding.UTF8, "application/json");

            var Resposta = await _client.PostAsync("/usuarios/cadastrar", CorpoRequisicao);

            var CorpoRespostaPost = await Resposta.Content.ReadFromJsonAsync<User>();

            if (CorpoRespostaPost != null)
            {
                Id = CorpoRespostaPost.Id.ToString();
            }

            var AtualizaUsuario = new Dictionary<string, string>()
            {
                { "id",Id},
                { "nome","Banela Balafina Burió"},
                { "usuario","banelaburió@email.com"},
                { "senha","123456789"},
                { "foto",""}
            };

            var UsuarioJsonAtualizar = JsonConvert.SerializeObject(AtualizaUsuario);
            var CorpoRequisicaoAtualizar = new StringContent(UsuarioJsonAtualizar, Encoding.UTF8, "application/json");

            _client.SetFakeBearerToken((object)token);

            var RespostaPut = await _client.PutAsync("/usuarios/atualizar", CorpoRequisicaoAtualizar);
            RespostaPut.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Order(6)]
        public async Task DeveListarUsuarioPeloId()
        {
            _client.SetFakeBearerToken((object)token);

            var Resposta = await _client.GetAsync("/usuarios/1");
            Resposta.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, Order(7)]
        public async Task DeveAutenticarUmUsuario()
        {
            var NovoUsuario = new Dictionary<string, string>()
            {
                { "usuario","claricio@email.com"},
                { "senha","123456789"},
            };

            var UsuarioJson = JsonConvert.SerializeObject(NovoUsuario);
            var CorpoRequisicao = new StringContent(UsuarioJson, Encoding.UTF8, "application/json");

            _client.SetFakeBearerToken((object)token);
            var Resposta = await _client.PostAsync("/usuarios/logar", CorpoRequisicao);
            Resposta.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
