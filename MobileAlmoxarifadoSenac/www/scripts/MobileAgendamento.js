'Equipamentos para a vDesktop'
$(document).ready(function () {
    IdEquipamento();
});

function IdEquipamento(){
    var Id = Cookies.get('idEquipamento');
    var api = ApiEquipamento();
    api.Consultar(Id, function(coxinha) {

    $('#equipDescricao').text(coxinha.Descricao);
    $('#NomeEquip').text(coxinha.Nome);
  
    let imageElementSource = coxinha.SrcImagem
    if (imageElementSource == null || imageElementSource == "")
        imageElementSource = "images/defaultequip.png"
    else
        imageElementSource = "data:image/png;base64, " + coxinha.SrcImagem;

    $('.img-mobile').css('background-image', 'url(\'' + imageElementSource + '\')');
    console.log(coxinha.Descricao);
    }, function(error){

    })
}