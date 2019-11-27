
'Equipamentos para a vDesktop'
$(document).ready(function () {
    UpdateContent('', '');
    ListCategories();
});

function ListCategories() {
    api = ApiCategoria();
    api.Listar(function (categorias) {
        var text = "";
        categorias.forEach(categoria => {
            var html = '<div class="section d-block">' + categoria.Nome + '</div>';
            text += html
        })
        $('.categories-list').append(text);
        $('.drop-filter').append(text);

        $('.section').click(function (){
            $('.section').each(function (){ 
                $(this).removeClass('selected-section')
            })
            var section = $(this);
            section.addClass('selected-section');

            const filter = $('.searchInput').val();
            const category = section.text();
            callUpdatePagination(1);
            UpdateContent(filter, category); 
        })
    }, function () { window.alert('Ocorreu um erro.') })
}

$('.section-clear').click(function (){
    UpdateContent('', '');
    $('#searchbar, .searchInput').val('');
    $('.section').each(function() {
        $(this).removeClass('selected-section')
    })
    callUpdatePagination(1);
})

$('.searchInput').keyup(function () {
    callUpdatePagination(1);
})

$('#searchbar').keyup(function () {
    callUpdatePagination(1);
})

// função que atualiza a lista de equipamentos 
function UpdateContent(filter, category, page) {
    let startEquip, endEquip;
    if (page == null) {
        if ($('.only').length > 0) {
            page = (screen.width <= 576) ? parseInt($('.d-md-none').find('.only').text()) : parseInt($('.d-md-block').find('.only').text());
        } else {
            page = 1;
        }
    }

    endEquip = page * 12;
    startEquip = endEquip - 12;

    filter = (filter != null || filter != undefined) ? filter : '';
    category = (category != null || category != undefined) ? category : '';
    
    const api = ApiEquipamento();
    api.Listar(filter, category, startEquip, endEquip, LoadEquipSuccess, LoadEquipError);
}

function LoadEquipSuccess(data) {
    var text = "";
    data.forEach(equip => {
        let imageElementSource = equip.SrcImagem
        if (imageElementSource == null || imageElementSource == "")
            imageElementSource = "images/defaultequip.png"
        else
            imageElementSource = "data:image/png;base64, " + equip.SrcImagem;

        var html = '<div class="col-lg-3 col-md-4 col-sm-4 col-6 mb-4"> <div class="equip card-link pt-2 pl-2 pr-2 " data-id="' + equip.Id + '" data-toggle="modal" data-target="#modalequip">' +
            '<div style="background-image: url(\'' + imageElementSource + '\')" class="imgEquip"></div>' +
            '<div class=" equip-title d-flex align-items-center">' + equip.Nome + '</div>' +
            '</div> </div>';
        text += html;
    });

    $('#equips, #equips-mobile').html(text);
    $('#equips-mobile .equip').removeAttr('data-toggle')
    $('#equips-mobile .equip').removeAttr('data-target')
    $('#equips-mobile .equip').on('click', function (e) { 
        window.location.replace("mobileAgendamento.html");
        const id = $(e.currentTarget).data('id');
        Cookies.set('idEquipamento', id, {expires: 1/24})
    })
}

function LoadEquipError(data) {
    window.alert("Ocorreu um erro.")
}



// Search container 
$(document).ready(function () {

    $(".searchInput").click(function () {
        $(".searchInput").addClass("expanded");
        $("#search-icon").hide();
        $("#back-icon").show();
    })

    $(".searchInput").blur(function () {
        $(".searchInput").removeClass("expanded");
        $("#back-icon").hide();
        $("#search-icon").show();
    })

    // Não está funcionando
    $('.search-icon-container').click(function () {
        $('.searchInput').val('')
    })
});
