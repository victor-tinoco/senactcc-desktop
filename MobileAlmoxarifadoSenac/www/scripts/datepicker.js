$(function () {
	$(".datejs").datepicker({
		monthNames: ["Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho", "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"],
		dayNames: ["Domingo", "Segunda", "Terça", "Quarta", "Quinta", "Sexta", "Sabado"],
		dayNamesMin: ["D", "S", "T", "Q", "Q", "S", "S"],
		dateFormat: "dd/mm/yy",
	});

	if ($('.today') !== undefined)
		$('.today').val(new Date().getDate() + "/" + (new Date().getMonth() + 1) + "/" + new Date().getFullYear())
	
	if ($('.tomorrow') !== undefined)
		$('.tomorrow').val((new Date().getDate() + 1) + "/" + (new Date().getMonth() + 1) + "/" + new Date().getFullYear())

	if($('.thisMonthFirstDay') !== undefined)
		$('.thisMonthFirstDay').val(1 + "/" + (new Date().getMonth() + 1) + "/" + new Date().getFullYear())

	if($('.thisMonthLastDay') !== undefined){
		var today = new Date();
		var month = today.getMonth();
		var year = today.getFullYear();
		var lastDate = new Date(year, month + 1, 0)
		$('.thisMonthLastDay').val(lastDate.getDate() + "/" + (new Date().getMonth() + 1) + "/" + new Date().getFullYear())
	}
});