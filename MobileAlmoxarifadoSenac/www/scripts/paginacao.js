$(function () {
    getPagination(1);
})

function getPagination(pageNumber) {
    const api = ApiEquipamento();
    let equipQuantity;
    let quantPages;
    const category = $('.selected-section').text()
    const filter = (screen.width <= 576) ? $('#searchbar').val() : $('.searchInput').val();
    api.Paginacao(category, filter, function (dados) {
        equipQuantity = (dados != 0) ? dados : 1;
        quantPages = Math.ceil(equipQuantity / 12);
        const link = new Object;

        if ((pageNumber + 2) <= quantPages) {
            link.main = pageNumber;
            link.second = pageNumber + 1;
            link.third = link.second + 1;
            fillPagination(link);
            $('.main').addClass('only');
        } else {
            if (quantPages < 3) {
                switch (quantPages) {
                    case 1:
                        link.main = 1;
                        fillPagination(link);
                        break;

                    case 2:
                        link.main = 1;
                        link.second = 2;
                        fillPagination(link);
                        if (pageNumber == 1)
                            $('.main').addClass('only');
                        else
                            $('.second').addClass('only');
                        break;

                    default:
                        break;
                }
            } else {
                if ((pageNumber + 1) <= quantPages) {
                    link.main = pageNumber - 1;
                    link.second = pageNumber;
                    link.third = pageNumber + 1;
                    fillPagination(link)
                    $('.second').addClass('only')
                } else if (pageNumber = quantPages) {
                    link.main = pageNumber - 2;
                    link.second = pageNumber - 1;
                    link.third = pageNumber;
                    fillPagination(link);
                    $('.third').addClass('only');
                }
            }
        }

        // Para validar se deve existir o back ou next
        if (pageNumber == 1)
            $('.back').addClass('d-none');
        if (pageNumber == quantPages)
            $('.next').addClass('d-none');

    }, function (dados) {
        window.alert('Ocorreu um erro.')
    })
}

function fillPagination(link) {
    const containerPagination = '<div class="col d-flex justify-content-center">' +
        '<ul class="p-0 m-0 mt-2 d-flex" style="list-style-type: none;">' +
        '<li class="page-links back"><</li>' +
        '<li class="page-links main">' + link.main + '</li>' +
        '<li class="page-links second ">' + link.second + '</li>' +
        '<li class="page-links third">' + link.third + '</li>' +
        '<li class="page-links next">></li>' +
        '</ul>' +
        '</div>';

    $('.pagination-js').html(containerPagination);

    if (link.second == null)
        $('.second').addClass('d-none');

    if (link.third == null)
        $('.third').addClass('d-none');

    clickPageLinksEvent();
}

function clickPageLinksEvent() {
    let updatedPage;

    $('.back').click(function () {
        const only = $(this).parent().find('.only').text();
        updatedPage = parseInt(only) - 1;
        callUpdatePagination(updatedPage);
    })

    $('.next').click(function () {
        const only = $(this).parent().find('.only').text();
        updatedPage = parseInt(only) + 1;
        callUpdatePagination(updatedPage);
    })

    $('.main, .second, .third').click(function () {
        updatedPage = parseInt($(this).text());
        callUpdatePagination(updatedPage);
    })


}
function callUpdatePagination(updatedPage) {
    getPagination(updatedPage);
    const category = $('.selected-section').text()
    const filter = (screen.width <= 576) ? $('#searchbar').val() : $('.searchInput').val();
    UpdateContent(filter, category, updatedPage);
}
