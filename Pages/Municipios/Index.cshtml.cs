using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Torneo.App.Persistencia;
using Torneo.App.Dominio;

namespace Torneo.App.Frontend.Pages.Municipios
{
    public class IndexModel : PageModel
    {
        private readonly IRepositorioMunicipio _repoMunicipio;
        public IEnumerable<Municipio> municipios { get; set; }
        public int gActual { get; set; }
        public string bActual { get; set; }
        public bool ErrorEliminar { get; set; }

        public IndexModel(IRepositorioMunicipio repoMunicipio)
        {
            _repoMunicipio = repoMunicipio;
        }


        public IActionResult OnPostDelete(int id)
        {
            try
            {
                _repoMunicipio.DeleteMunicipio(id);
                municipios = _repoMunicipio.GetAllMunicipios();
                return Page();
            }
            catch (Exception ex)
            {
                municipios = _repoMunicipio.GetAllMunicipios();
                ErrorEliminar = true;
                return Page();
            }
        }
        public void OnGet(int? g, string b)
        {
            municipios = _repoMunicipio.GetAllMunicipios();
            ErrorEliminar = false;

            if (String.IsNullOrEmpty(b))
            {
                bActual = "";
                municipios = _repoMunicipio.GetAllMunicipios();
            }
            else
            {
                bActual = b;
                municipios = _repoMunicipio.SearchMunicipios(b);
            }
        }
    }
}