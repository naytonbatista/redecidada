$(function () {

    var container = $('body');
    var btnLogar = container.find('#btnLogar');
    
    btnLogar.bind('click', logar);

    function logar() {

        var login = $('input[name=login]').val(), 

            senha = $('input[name=senha]').val(),
            
            lblValidacao = $('#validacao').html(validacao),

            bloqueiaTela = $('.bloqueia_tela');

            validacao = '';


        bloqueiaTela.show();
        lblValidacao.html('');

        if (!login) {
            validacao = 'Preencha o campo Login.';
        }

        if (!senha) {
            validacao = validacao? validacao + ' <br /> Preencha o campo Senha.' : validacao;
        }

        if (validacao) {

            lblValidacao.html(validacao);
            bloqueiaTela.hide();
            return;
        }
        var request = {
            login,
            senha
        };
        
        $.post('inicio/logar', request, function (response) {
            if (response.url) {

                window.location.href = response.url;
                return;
            }
            bloqueiaTela.hide();

            lblValidacao.html(response.message);

        });
        
    };

});