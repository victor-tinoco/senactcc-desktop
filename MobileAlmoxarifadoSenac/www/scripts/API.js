const urlBaseApiEquipamento = "http://10.136.52.25/apipatrimoniosenac/api/equipamento/";
const urlBaseApiAgendamento = "http://10.136.52.25/apipatrimoniosenac/api/agendamento/";
const urlBaseApiCategoria = "http://10.136.52.25/apipatrimoniosenac/api/categoria/";
const urlBaseApiLogin = "http://10.136.52.25/apipatrimoniosenac/api/Login/";

function ApiCategoria() {
    var api = new Object;

    api.Listar = function (acaoSucesso, acaoErro) {
        $.ajax({
            url: urlBaseApiCategoria,
            contentType: 'application/json',
            method: "GET",
            success: function (data) { acaoSucesso(data); },
            error: function (data) { acaoErro(data); }
        })
    }

    return api;
}

function ApiEquipamento() {
    var api = new Object;

    
    api.Listar = function (filtro, categoria, iniciopag, fimpag, acaoSucesso, acaoErro) {
        $.ajax({
            url: urlBaseApiEquipamento + "?filtro=" + filtro + "&categoria=" + categoria + "&iniciopag=" + iniciopag + "&fimpag=" + fimpag,
            contentType: 'application/json',
            method: "GET",
            success: function (data) { acaoSucesso(data); },
            error: function (data) { acaoErro(data); }
        })
    }

    api.Consultar = function (Id, acaoSucesso, acaoErro) {
        $.ajax({
            url: urlBaseApiEquipamento + Id,
            method: "GET",
            success: function (data) { acaoSucesso(data); },
            error: function (data) { acaoErro(data); }
        })
    }

    api.Paginacao = function(filtro, categoria, acaoSucesso, acaoErro) {
        $.ajax({
            url: urlBaseApiEquipamento + "?filtro=" + filtro + "&categoria=" + categoria,
            method: "GET",
            success: function (data) { acaoSucesso(data); },
            error: function (data) { acaoErro(data); }
        })        
    }

    return api;
}

function ApiAgendamento() {
    var api = new Object;

    api.ConsultarDisponibilidade = function (dia, horaRetirada, horaDevolucao, id, acaoSucesso, acaoErro) {
        $.ajax({
            url: urlBaseApiAgendamento + "?dia=" + dia + "&horaretirada=" + horaRetirada + "&horadevolucao=" + horaDevolucao + "&id=" + id ,
            method: "GET",
            success: function (data) { acaoSucesso(data); },
            error: function (data) { acaoErro(data); }
        })
    }

    api.Incluir = function (dados, acaoSucesso, acaoCarregando, acaoCarregado, acaoErro) {
        $.ajax({
            url: urlBaseApiAgendamento,
            method: "POST",
            contentType: 'application/json', 
            data: JSON.stringify(dados),
            success: function (data) { acaoSucesso(data); },
            beforeSend: function(dados){ acaoCarregando(dados) },
            complete:function(dados){ acaoCarregado(dados) },
            error: function (data) { acaoErro(data); }
        })
    }
    api.ConsultarIdUser = function(filtro,filtroinicio,filtrofim){
        $.ajax({
            url: urlBaseApiAgendamento + "?filtro="+filtro+"&filtroinicio="+filtroinicio +"&filtrofim="+filtrofim,
            method: "GET",
            success: function (data) { acaoSucesso(data); },
            error: function (data) { acaoErro(data); }
        })
    }

    return api;
}

function ApiLogin() {
    var api = new Object;

    api.FazerLogin = function(dados, acaoSucesso, acaoCarregando, acaoCarregado, acaoErro) {
        $.ajax({
            url: urlBaseApiLogin ,
            method: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(dados),
            success: function(dados){ acaoSucesso(dados); },
            error: function(dados){ acaoErro(dados) },
            beforeSend: function(dados){ acaoCarregando(dados) },
            complete:function(dados){ acaoCarregado(dados) }
        })
    }

    return api;
}