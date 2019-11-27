$(function () {
	updateCards();
})

function updateCards(){
	const filtro =  $('#searchBar-mobile').val();
	let filtroinicio =  $('#start-dp-mobile').datepicker('getDate'); //campo de pesquisa mobile;
	let filtrofim =  $('#end-dp-mobile').datepicker('getDate') ; //campo de pesquisa mobile;

	filtroinicio = (new Date(filtroinicio).getMonth() + 1) + '-' + new Date(filtroinicio).getDate() + '-' + new Date(filtroinicio).getFullYear();
	filtrofim = (new Date(filtrofim).getMonth() + 1) + '-' + new Date(filtrofim).getDate() + '-' + new Date(filtrofim).getFullYear();

	const api = ApiAgendamento();
	api.Listar(filtro, filtroinicio, filtrofim, function (response) {
		let html = '';
		response.forEach(x => {
			x.DataAgendamento = new Date(x.DataAgendamento).getDate() + '/' + new Date(x.DataAgendamento).getMonth() + '/' + new Date(x.DataAgendamento).getFullYear();
			x.Dia = new Date(x.Dia).getDate() + '/' + new Date(x.Dia).getMonth() + '/' + new Date(x.Dia).getFullYear();
			const card = '<div class="card-item p-3 mb-3">' +
				'<div class="header-card d-flex justify-content-between align-items-center">' +
				'<h2>' + x.Equipamento.Nome + '</h2>' +
				'<i class="fas fa-chevron-down expand"></i>' +
				'</div>' +
				'<div class="body-card d-none">' +
				'<hr>' +
				'<div class="d-flex flex-column mt-3">' +
				'<h3>' + x.Equipamento.NomeCategoria + '</h3>' +
				'<div class="dataInfo mb-2">Câmeras</div>' +
				'<h3>Quantidade</h3>' +
				'<div class="dataInfo mb-2">' + x.Quantidade + '</div>' +
				'<h3>Data que Agendou</h3>' +
				'<div class="dataInfo mb-2">' + x.DataAgendamento + '</div>' +
				'<h3>Dia do Agendamento</h3>' +
				'<div class="dataInfo mb-2">' + x.Dia + '</div>' +
				'<h3>Hora da Retirada</h3>' +
				'<div class="dataInfo mb-2">' + x.DataHoraRetirada + '</div>' +
				'<h3>Hora da Devolução</h3>' +
				'<div class="dataInfo mb-2">' + x.DataHoraDevolucao + '</div>' +
				'<h3>Status</h3>' +
				'<div class="dataInfo mb-2">' + x.StatusDevolucao + '</div>' +
				'</div>' +
				'</div>' +
				'</div>'
			html += card;
		});
		$('.agendamentos-cards').html(html);
		expandCardEvent();
	}, function (error) {
		window.alert('Ocorreu um erro com a aplicação, entre em contato com o suporte.')
	})
}

$('#start-dp, #end-dp, #start-dp-mobile, #end-dp-mobile').change(function(){
	updateCards();
})

$('#searchBar, #searchBar-mobile').keyup(function(){
	updateCards();
})

function expandCardEvent() {
	const headerCard = $('.header-card');

	headerCard.click(function (e) {
		const bodyCard = $(this).parent().find('.body-card');
		const expandIcon = $(this).find('.expand');

		if (bodyCard.hasClass('d-none')) {
			bodyCard.removeClass('d-none');
			expandIcon.removeClass('fa-chevron-down');
			expandIcon.addClass('fa-chevron-up');
		}
		else {
			bodyCard.addClass('d-none');
			expandIcon.removeClass('fa-chevron-up');
			expandIcon.addClass('fa-chevron-down');
		}
	})
}

