$('#modalequip').on('show.bs.modal', function (event) {
	var button = $(event.relatedTarget)
	$('#idEquip').val(button.data('id'));
	var idEquip = $('#idEquip').val()
	var modal = $(this)
	var api = ApiEquipamento();

	api.Consultar(idEquip, function (equip) {
		modal.find('#equipName').text(equip.Nome)
		modal.find('#equipDescricao').text(equip.Descricao)

		let imageElementSource = equip.SrcImagem
		if (imageElementSource == null || imageElementSource == "")
			imageElementSource = "IMG/defaultequip.png"
		else
			imageElementSource = "data:image/png;base64, " + equip.SrcImagem;
		modal.find('.img-modal').css('background-image', 'url(\'' + imageElementSource + '\')');

	}, function (equip) {
		window.alert("Ocorreu um erro.")
	})

	// Limpar campos do último modal aberto
	ClearCheckbox();
	ClearSelect();
	$('.today').val(new Date().getDate() + "/" + (new Date().getMonth() + 1) + "/" + new Date().getFullYear());
})

$('input[type="checkbox"]').change(function () {
	FillSelect();
});

$('#datepicker').change(function () {
	FillSelect();
});

function FillSelect(){
	const api = ApiAgendamento();

	var dia = ConsultDate();

	var id = $('#idEquip').val()

	var hora = ConsultTime()

	// Para não quebrar a hora de entrega temos essa regra de negócio, onde o usuario não pode não pode 
	// alugar num mesmo agendamento somente os horários manhã e noite.
	if (hora != false && dia != false) {
		api.ConsultarDisponibilidade(dia, hora.Retirada, hora.Devolucao, id, function(data) {
			const selectQuant = $('.select-quant');
			ClearSelect();
			quantidadeDisponivel = data;
			var html = '';
			for (let i = 0; i <= quantidadeDisponivel; i++) {
				var text = '<option>' + i + '</option>';
				html += text;
			}
			selectQuant.html(html);
		},function(){
			ClearSelect();
			console.log('Foi inserido uma data ou períodos nulos ou indefinidos.') 
		})		
	} else {
		ClearSelect();
	}
}

function ClearCheckbox(){
	$('#check-manha').prop('checked', false);
	$('#check-tarde').prop('checked', false);
	$('#check-noite').prop('checked', false);
}

function ClearSelect(){
	var text = '<option selected>0</option>';
	$('.select-quant').html(text)
}

$('.confirm-btn').click(function(){
	if (ValidateModalFields()){
		var dados = new Object;
		var hora = ConsultTime();
		dados.DataAgendamento = new Date(new Date().getFullYear(), new Date().getMonth(), new Date().getDate());
		dados.Dia = $('.datepicker-modal').datepicker('getDate');
		dados.DataHoraRetirada = hora.Retirada;
		dados.Quantidade = $('.select-quant option:selected').val()
		dados.DataHoraDevolucao = hora.Devolucao;
		dados.IdEquipamento = $('#idEquip').val();

		var api = ApiAgendamento();
		api.Incluir(dados, function(dados){
			
		}, function(dados){
			$('.confirm-btn').attr('disabled', 'disabled');
			$('.confirm-btn').text('Carregando...')
		}, function(dados){
			$('.confirm-btn').removeAttr('disabled', 'disabled');
			$('.confirm-btn').text('Confirmar')
			$('#modalequip').modal('hide');
			setTimeout(() => { window.alert('Agendamento efetuado com sucesso.') }, 2000); 
		}, function(dados){
			window.alert('Ocorreu um erro.')
		})
	};
})


// Caso der certo retorna os períodos selecionados, se não, retorna falso.
function ConsultTime(){
	var cbManha = $('#check-manha').is(':checked');
	var cbTarde = $('#check-tarde').is(':checked');
	var cbNoite = $('#check-noite').is(':checked');

	if (!cbManha && !cbNoite && !cbTarde) {
		ClearSelect();
		return false;
	}

	if (cbManha && cbNoite && !cbTarde) {
		window.alert('Para esses horários faça um agendamento de cada vez*')
		ClearCheckbox();
		ClearSelect();
		return false;
	}

	var horaRetirada;
	var horaDevolucao;
	if (cbManha)
		horaRetirada = '07:00:00';
	else
		if (cbTarde)
			horaRetirada = '13:00:00';
		else
			horaRetirada = (cbNoite) ? '19:00:00' : null;
	if (cbNoite)
		horaDevolucao = '22:30:00'
	else
		if (cbTarde)
			horaDevolucao = '18:00:00'
		else
			horaDevolucao = (cbManha) ? '12:00:00' : null;

	var time = new Object;
	time.Retirada = horaRetirada;
	time.Devolucao = horaDevolucao;

	return time;
}

// Caso der certo retorna a data selecionada, se não, retorna falso.
function ConsultDate(){
	var today = new Date();
	today.setHours(0,0,0,0);
	var diaInput = $('.datepicker-modal').datepicker('getDate');

	if ((diaInput < today) || ($('.datepicker-modal') === ''))
		return false;

	var diaInput = $('.datepicker-modal').datepicker('getDate');
	diaInput = diaInput.getFullYear() + '-' + (diaInput.getMonth() + 1) + '-' + diaInput.getDate();	

	return diaInput;
}

function ValidateModalFields(){
	var erro = '';

	if (ConsultTime() == false)
		erro += '- Selecione um período*\n'
	
	if ($('.select-quant option:selected').val() == 0)
		erro += '- Selecione uma quantidade*\n'

	if (ConsultDate() == false)
		erro += '- Selecione uma data igual ou maior que o dia de hoje\n'


	if (erro === '') {
		erro += '- Selecione uma data'
		return true;
	} else {
		window.alert('Erro:\n' + erro);
		return false;
	}
}