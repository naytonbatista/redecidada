$(function () {
    
    var container = $('body');

    var checkAtividade = container.find('input[type=checkbox]');
    
    checkAtividade.bind('click', checkClick);

    $('.data_nascimento').mask('00/00/0000');

    container.find('.ddlEstados').change(estadoChange);

    function checkClick()
    {
        var divAtividades = $(this).closest('fieldset').find('.atividades');
        divAtividades.html('');
        container.find('input[type=checkbox]:checked').each(function (i, item) {
            
            var atividadeId = JSON.parse($(item).closest('.checkbox-inline').find('.hdnAtividade').val()).Id;

            divAtividades.append('<input type="hidden" name="Atividades[' + i + ']" value="' + atividadeId + '"/>')

        });

    }

    function estadoChange()
    {
        var estadoId = $(this).val();

        if (!estadoId) {
            return;
        }

        $.get('obtermunicipios', { estadoId: estadoId }, function (response) {

            core.dropDownCharger(container.find('.ddlMunicipios'), response.lista)

        });
    }
});