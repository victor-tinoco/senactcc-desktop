$(function() {
   

    // Mobile
    var contentMobileMenu = '<a class="col-12" href="carteira.html">Carteira de Agendamentos</a>' +
                        '<a class="col-12" href="index.html">Sair</a>';
    $('#menu-mobile').html(contentMobileMenu);

    $('.menu-mobile-icon').click(function(){
        const menu = $(".menu-drop-mobile");
        if (menu.is(':visible'))
            menu.css('display', 'none');
        else
            menu.css('display', 'block');
    })
    
    // circleProfile.blur(function(){
    //     if (menuDropContainer.hasClass('d-none'))
    //         menuDropContainer.removeClass('d-none');
    // })
})