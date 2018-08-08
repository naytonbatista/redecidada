var core = {
    
    alert: function (titulo, mensagem) {

        var modal =    $('<div id="myModal" class="modal fade" role="dialog">' +
                        '    <div class="modal-dialog">' +
                        '       <div class="modal-content"> ' +
                        '           <div class="modal-header">' +
                        '               <button type="button" class="close" data-dismiss="modal">&times;</button>' +
                        '               <h4 class="modal-title">Modal Header</h4>' +
                        '           </div>' +
                        '           <div class="modal-body">' +
                        '               <p>Some text in the modal.</p>' +
                        '           </div>' +
                        '           <div class="modal-footer">' +
                        '               <button type="button" class="btn btn-default" data-dismiss="modal">Fechar</button>' +
                        '           </div>' +
                        '       </div>' +

                        '    </div>' +
                        '</div>'),

            container = $('body'), 
            
            btn = $('<button type="button" style="display:none" class="btn btn-info btn-lg" data-toggle="modal" data-target="#myModal"></button>');
        
        modal.find('.modal-title').html(titulo);
        modal.find('.modal-body p').html(mensagem);


        container.find('#myModal').remove();
        container.append(modal);

        
        container.append(btn);
        btn.trigger('click');
        

    },

    dropDownCharger: function (dropdownlist, lista)
    {
        var ddl = $(dropdownlist);

        $(lista).each(function () {

            var item = this;

            ddl.append('<option value=' + item.Id + '>' + item.Nome + '</option>');
        });
    }
};