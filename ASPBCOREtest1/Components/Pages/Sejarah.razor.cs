using ASPBCOREtest1.Models;
using ASPBCOREtest1.Service;
using Microsoft.AspNetCore.Components;


namespace ASPBCOREtest1.Components.Pages
{
    public partial class Sejarah
    {
        [Inject] private ProdukService service { get; set; }

        private List<Produk> ListAllSejarah = new();

        protected override async Task OnInitializedAsync()
        {
            ListAllSejarah = await service.GetSejarah();
        }

    }
}
