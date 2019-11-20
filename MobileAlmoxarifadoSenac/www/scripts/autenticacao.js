var urlSite = 'http://127.0.0.1:5500/'
if(Cookies.get('site_autenticado') !== undefined){
     $.ajaxSetup({
         headers: {'Authorization': 'Bearer '+Cookies.get('site_autenticado')}

     });
}
else if (window.location.href.indexOf('index.html') === -1)
{
    window.location.href = urlSite + 'index.html';
}