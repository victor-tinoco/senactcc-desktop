
function showDetails(x) {
    x.classList.remove('hidn') // Remove a classe hidn para que o toggleDetails saiba que a div de detalhes foi mostrada
    x.setAttribute('name', 'arrow-dropup') // Muda o icone da seta
    x.setAttribute('name', 'arrow-dropup') // Muda o icone da seta

    var divDetails =
        x.parentNode
            .parentNode
            .querySelector('.equip-details')
    // Viaja pela DOM até encontrar a div dos detalhes

    divDetails.style.display = "block"
    // Altera o display de "none" para "block"
}

function hideDetails(x) {
    x.classList.add('hidn');
    x.setAttribute('name', 'arrow-dropdown')

    var divDetails =
        x.parentNode
            .parentNode
            .querySelector('.equip-details')

    divDetails.style.display = "none"
}

function toggleDetails(x) {
    // Condicional para identificar se a div está aparecendo ou não
    if (x.classList.contains('hidn')) {
        showDetails(x) // Caso não, a função showDetails será chamada, para mostrar os detalhes
    }
    else {
        hideDetails(x) // Caso sim, a função hideDetails será chamada, para esconder os detalhes
    }
}